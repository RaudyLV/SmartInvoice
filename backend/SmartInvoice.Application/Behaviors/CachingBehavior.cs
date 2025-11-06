using MediatR;
using Microsoft.Extensions.Logging;
using SmartInvoice.Application.Interfaces;


namespace SmartInvoice.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICacheServices _cacheServices;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

        public CachingBehavior(ILogger<CachingBehavior<TRequest, TResponse>> logger, ICacheServices cacheServices)
        {
            _logger = logger;
            _cacheServices = cacheServices;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ICacheableQuery cacheableQuery)
            {
                return await next();
            }

            var cacheKey = cacheableQuery.CacheKey;

            var cachedResponse = await _cacheServices.GetAsync<TResponse>(cacheKey, cancellationToken);
            if (cachedResponse != null)
            {
                _logger.LogInformation($"Cache hit for {cacheKey}");
                return cachedResponse;
            }

            _logger.LogInformation($"Cache miss for {cacheKey}");

            var response = await next();

            await _cacheServices.SetAsync(cacheKey, response, cacheableQuery.CacheDuration, cancellationToken);

            return response;
        }
    }
}