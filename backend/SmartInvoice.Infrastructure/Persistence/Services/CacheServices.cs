using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using SmartInvoice.Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class CacheServices : ICacheServices
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<CacheServices> _logger;

        public CacheServices(
            IDistributedCache cache, 
            IConnectionMultiplexer redis,
            ILogger<CacheServices> logger)
        {
            _cache = cache;
            _redis = redis;
            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var cached = await _cache.GetStringAsync(key, cancellationToken);
                
                if (string.IsNullOrWhiteSpace(cached))
                {
                    _logger.LogDebug("Cache miss for key: {Key}", key);
                    return default;
                }

                var result = JsonSerializer.Deserialize<T>(cached);
                _logger.LogDebug("Cache hit for key: {Key}", key);
                return result;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing cache for key: {Key}", key);
                // Eliminar cache corrupto
                await _cache.RemoveAsync(key, cancellationToken);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cache for key: {Key}", key);
                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            try
            {
                if (value == null)
                {
                    _logger.LogWarning("Attempted to cache null value for key: {Key}", key);
                    return;
                }

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(30)
                };

                var serialized = JsonSerializer.Serialize(value, new JsonSerializerOptions
                {
                    WriteIndented = false,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });

                await _cache.SetStringAsync(key, serialized, options, cancellationToken);
                _logger.LogDebug("Cache set for key: {Key}, expiration: {Expiration}", key, options.AbsoluteExpirationRelativeToNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting cache for key: {Key}", key);
                // No lanzar excepción, solo loggear
            }
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cache.RemoveAsync(key, cancellationToken);
                _logger.LogDebug("Cache removed for key: {Key}", key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cache for key: {Key}", key);
            }
        }

        public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
        {
            try
            {
                var endpoints = _redis.GetEndPoints();
                var keysRemoved = 0;

                foreach (var endpoint in endpoints)
                {
                    var server = _redis.GetServer(endpoint);
                    var keys = server.Keys(pattern: $"*{prefix}*");
                    
                    foreach (var key in keys)
                    {
                        await _cache.RemoveAsync(key.ToString(), cancellationToken);
                        keysRemoved++;
                    }
                }

                _logger.LogDebug("Removed {Count} cache keys with prefix: {Prefix}", keysRemoved, prefix);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cache by prefix: {Prefix}", prefix);
            }
        }
    }
}