using System;
using System.Collections.Generic;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Storage;
using Microsoft.Extensions.Caching.Memory;

namespace CarFactory_Wheels
{
    public class WheelProvider : IWheelProvider
    {
        private readonly IGetRubberQuery _getRubberQuery;
        private readonly IMemoryCache _cache;

        public WheelProvider(IGetRubberQuery getRubberQuery, IMemoryCache cache)
        {
            _getRubberQuery = getRubberQuery;
            _cache = cache; 
        }

        public IEnumerable<Wheel> GetWheels()
        {
            _cache.TryGetValue(CacheKeys.Materials, out IEnumerable<Part> parts);
            if (parts == null)
            {
                parts = _getRubberQuery.Get();
                _cache.Set(CacheKeys.Materials, parts, DateTime.Now.AddDays(1));
            }

            return new[]
            {
                CreateWheel(ref parts),
                CreateWheel(ref parts),
                CreateWheel(ref parts),
                CreateWheel(ref parts)
            };
        }

        private Wheel CreateWheel(ref IEnumerable<Part> allRubber)
        {
            var rubber = allRubber.Take(50);
            // decrease used rubber from cache and database
            
            if (rubber.Any(x => x.PartType != PartType.Rubber))
            {
                throw new Exception("parts must be rubber");
            }
            
            return new Wheel(){Manufacturer = rubber.First().Manufacturer};
        }
    }
}