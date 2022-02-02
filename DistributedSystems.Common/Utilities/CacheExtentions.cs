using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DistributedSystems.Common.Utilities
{
    public static class CacheExtentions
    {
        public static async Task<T> GetValueAsync<T>(this IDistributedCache cache, string name, CancellationToken cancellationToken = default)
        {
            var objectCache = await cache.GetStringAsync(name, cancellationToken);
            if (objectCache is null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(objectCache);
        }

        public static async Task SetValueAsync(this IDistributedCache cache, string name, object value, double minute, CancellationToken cancellationToken = default)
        {
            var objectCache = await cache.GetStringAsync(name, cancellationToken);
            if (!(objectCache is null))
                await cache.RemoveAsync(name);

            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(minute));

            await cache.SetStringAsync(name, JsonConvert.SerializeObject(value), options, cancellationToken);
        }
    }
}
