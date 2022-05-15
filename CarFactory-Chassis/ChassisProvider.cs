using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactory_Chasis
{
    public class ChassisProvider : IChassisProvider
    {
        private readonly ISteelSubcontractor _steelSubcontractor;
        private readonly IGetChassisRecipeQuery _chassisRecipeQuery;
        private Object lockObject = new();

        public ChassisProvider(ISteelSubcontractor steelSubcontractor, IGetChassisRecipeQuery chassisRecipeQuery)
        {
            _steelSubcontractor = steelSubcontractor;
            _chassisRecipeQuery = chassisRecipeQuery;
        }
        public Chassis GetChassis(Manufacturer manufacturer, int numberOfDoors)
        {
            var chassisRecipe = _chassisRecipeQuery.Get(manufacturer);

            var chassisParts = new List<ChassisPart>();
            chassisParts.Add(new ChassisBack(chassisRecipe.BackId));
            chassisParts.Add(new ChassisCabin(chassisRecipe.CabinId));
            chassisParts.Add(new ChassisFront(chassisRecipe.FrontId));

            CheckChassisParts(chassisParts);

            // milliseconds
            // Timing for 10 caes is : 10231 with parallel interlocked
            // Timing for 10 cars is : 7398 with parallel lock
            // Timing for 10 cars is : 25043 no parallel

            // Timing for 75 cars is : 31391 with parallel lock only asking for needed
            // Timing for 75 cars is : 32968 with parallel lock asking when not enough
            // Timing for 75 cars is : 38542 with parallel interlocked
            // Timing for 75 cars is : 45479 with parallel lock
            // Timing for 75 cars is : 203559 no parallel

            lock (lockObject)
            {
                if(SteelInventory < chassisRecipe.Cost)
                {
                    var missingSteel = chassisRecipe.Cost - SteelInventory;
                    SteelInventory += _steelSubcontractor.OrderSteel(missingSteel).Select(d => d.Amount).Sum();
                }
                CheckForMaterials(chassisRecipe.Cost);
                SteelInventory -= chassisRecipe.Cost;
            }

            var chassisWelder = new ChassisWelder();

            chassisWelder.StartWeld(chassisParts[0]);
            chassisWelder.ContinueWeld(chassisParts[1], numberOfDoors);
            chassisWelder.FinishWeld(chassisParts[2]);
 
            return chassisWelder.GetChassis();
        }

        public int SteelInventory { get; private set; }

        private void CheckForMaterials(int cost)
        {
                if (SteelInventory < cost)
            {
                throw new Exception("Not enough chassis material");
            }
        }

        private void CheckChassisParts(List<ChassisPart> parts)
        {
            if (parts == null)
            {
                throw new Exception("No chassis parts");
            }

            if (parts.Count > 3)
            {
                throw new Exception("Too many chassis parts");
            }

            if (parts.Count < 3)
            {
                throw new Exception("Chassis parts missing");
            }
        }
    }
}
