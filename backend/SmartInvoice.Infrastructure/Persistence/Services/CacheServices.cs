using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SmartInvoice.Application.Interfaces;
using StackExchange.Redis;

namespace SmartInvoice.Infrastructure.Persistence.Services
{
    public class CacheServices : ICacheServices
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;
        public CacheServices(IDistributedCache cache, IConnectionMultiplexer redis)
        {
            _cache = cache;
            _redis = redis;
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            var cached = await _cache.GetStringAsync(key, cancellationToken);

            if (!string.IsNullOrEmpty(cached))
                return default!;

            return JsonSerializer.Deserialize<T>(cached!)!;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(30)
            };

            var serilaized = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, serilaized, options, cancellationToken);
        }
        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }

       public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
        {
            var endpoints = _redis.GetEndPoints();

            foreach (var endpoint in endpoints)
            {
                var server = _redis.GetServer(endpoint);

                var cursor = 0L;
                do
                {
                    var scanResult = server.Keys(cursor: cursor, pattern: $"{prefix}*", pageSize: 500);
                    
                    foreach (var key in scanResult)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            return;

                        await _cache.RemoveAsync(key.ToString(), cancellationToken);
                    }

                    if (!scanResult.Any())
                        break;

                } while (true);
            }
        }
    }
}