using CarFactory_Domain;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace CarFactory_Storage
{
    public class CachedParts : IGetRubberQuery
    {
        protected IMemoryCache _cache;
        private IGetRubberQuery _query;
        private const int _numberOfCacheDays = 1;

        public CachedParts(IGetRubberQuery getRubberQuery, IMemoryCache memoryCache)
        {
            _query = getRubberQuery;
            _cache = memoryCache;
        }

        public IEnumerable<Part> Get()
        {
            _cache.TryGetValue(CacheKeys.Materials, out IEnumerable<Part> parts);
            if (parts == null)
            {
                parts = _query.Get();
                _cache.Set(CacheKeys.Materials, parts, DateTime.UtcNow.AddDays(_numberOfCacheDays));
            }
            return parts;
        }
    }
}
