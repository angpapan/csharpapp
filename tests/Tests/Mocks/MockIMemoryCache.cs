using Microsoft.Extensions.Caching.Memory;

namespace Tests.Mocks;

public class MockIMemoryCache : IMemoryCache
{
    private readonly IMemoryCache _memoryCache;

    public MockIMemoryCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public bool TryGetValue<TItem>(object key, out TItem value)
    {
        return _memoryCache.TryGetValue(key, out value);
    }

    public void Set<TItem>(object key, TItem value, DateTimeOffset absoluteExpiration)
    {
        _memoryCache.Set(key, value, absoluteExpiration);
    }

    public bool TryGetValue(object key, out object? value)
    {
        throw new NotImplementedException();
    }

    public ICacheEntry CreateEntry(object key)
    {
        throw new NotImplementedException();
    }

    public void Remove(object key)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
