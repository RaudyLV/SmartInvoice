namespace SmartInvoice.Application.Interfaces
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }
        TimeSpan? CacheDuration { get; }
    }
}