using System;
using System.Collections.Generic;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Storage;

namespace CarFactory_Wheels
{
    public class WheelProvider : IWheelProvider
    {
        private readonly IGetRubberQuery _getRubberQuery;

        public WheelProvider(IGetRubberQuery getRubberQuery)
        {
            _getRubberQuery = getRubberQuery;
        }
        0
        public IEnumerable<Wheel> GetWheels()
        {
            var parts = _getRubberQuery.Get();
            return CreateWheels(4, ref parts);
        }

        private Wheel[] CreateWheels(int numberOfWheels, ref IEnumerable<Part> parts)
        {
            var wheelArr = new Wheel[numberOfWheels];
            for (int i = 0; i < numberOfWheels; i++)
            {
                wheelArr[i] = CreateWheel(ref parts);
            }

            return wheelArr;
        }

        private Wheel CreateWheel(ref IEnumerable<Part> allRubber)
        {
            var rubber = allRubber.Take(50);
            // decrease used rubber from cache and database
            
            if (rubber.Any(x => x.PartType != PartType.Rubber))
            {
                throw new Exception("parts must be rubber");
            }
            
            return new Wheel() { Manufacturer = rubber.First().Manufacturer };
        }
    }
}