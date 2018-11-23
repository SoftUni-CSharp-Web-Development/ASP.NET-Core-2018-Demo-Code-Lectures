using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace IntroMvc.Controllers
{
    public class CacheDemoController : BaseController
    {
        private readonly IMemoryCache cache;
        private readonly IDistributedCache distributedCache;

        public CacheDemoController(IMemoryCache cache, IDistributedCache distributedCache)
        {
            this.cache = cache;
            this.distributedCache = distributedCache;
        }

        // Use only for client caching: [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
            DateTime cacheEntry;

            if (!this.cache.TryGetValue("NowCache", out cacheEntry)) // Look for cache key.
            {
                cacheEntry = DateTime.UtcNow; // Key not in cache, so get data.

                var cacheEntryOptions = new MemoryCacheEntryOptions() // Set cache options.
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10)); // Keep in cache for this time.

                // Save data in cache.
                this.cache.Set("NowCache", cacheEntry, cacheEntryOptions);
            }

            return this.View(cacheEntry);
        }
    }
}
