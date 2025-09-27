using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Project.Services;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(15);

    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<T?> GetAsync<T>(string key) where T : class
    {
        if (_cache.TryGetValue(key, out var cachedValue))
        {
            if (cachedValue is T directValue)
            {
                return Task.FromResult<T?>(directValue);
            }

            if (cachedValue is string jsonValue)
            {
                var deserializedValue = JsonSerializer.Deserialize<T>(jsonValue);
                return Task.FromResult(deserializedValue);
            }
        }

        return Task.FromResult<T?>(null);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? _defaultExpiration,
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };

        _cache.Set(key, value, cacheOptions);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        _cache.Remove(key);
        return Task.CompletedTask;
    }

    public Task RemoveByPatternAsync(string pattern)
    {
        return Task.CompletedTask;
    }
}
