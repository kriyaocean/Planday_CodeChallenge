using CarFactory_Chasis;
using CarFactory_Storage;
using CarFactory_SubContractor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass, TestCategory("UnitTests")]
    public class ChassisProviderTests
    {
        [TestMethod]
        public void GetChassis_4door_hatchback_offroader()
        {
            var subcontractorMock = new Mock<ISteelSubcontractor>();
            subcontractorMock.Setup(x => x.OrderSteel(It.IsAny<int>())).Returns(new List<SteelDelivery> { new SteelDelivery () });
            var recipeMock = new Mock<IGetChassisRecipeQuery>();
            var recipe = new CarFactory_Domain.ChassisRecipe(CarFactory_Domain.Manufacturer.PlanfaRomeo, 1, 10, 1, 15, 2, 11);
            recipeMock.Setup(x => x.Get(It.IsAny<CarFactory_Domain.Manufacturer>())).Returns(recipe);
            var provider = new ChassisProvider(subcontractorMock.Object, recipeMock.Object);

            var result = provider.GetChassis(CarFactory_Domain.Manufacturer.PlanfaRomeo, 5);

            Assert.IsTrue(result.ValidConstruction);
            Assert.AreEqual("Four Door Hatchback Offroader", result.Description);
            Assert.AreEqual(50 - (10 + 15 + 11), provider.SteelInventory);
        }

        [TestMethod]
        public void GetChassis_2door_pickup_familyCar()
        {
            var subcontractorMock = new Mock<ISteelSubcontractor>();
            subcontractorMock.Setup(x => x.OrderSteel(It.IsAny<int>())).Returns(new List<SteelDelivery> { new SteelDelivery() });
            var recipeMock = new Mock<IGetChassisRecipeQuery>();
            var recipe = new CarFactory_Domain.ChassisRecipe(CarFactory_Domain.Manufacturer.AstonPlanday, 2, 14, 0, 20, 1, 12);
            recipeMock.Setup(x => x.Get(It.IsAny<CarFactory_Domain.Manufacturer>())).Returns(recipe);
            var provider = new ChassisProvider(subcontractorMock.Object, recipeMock.Object);

            var result = provider.GetChassis(CarFactory_Domain.Manufacturer.AstonPlanday, 3);

            Assert.IsTrue(result.ValidConstruction);
            Assert.AreEqual("Two Door Pickup Family car", result.Description);
            Assert.AreEqual(50 - (14 + 20 + 12), provider.SteelInventory);
        }

        [TestMethod]
        public void GetChassis_2door_sedan_sportscar()
        {
            var subcontractorMock = new Mock<ISteelSubcontractor>();
            subcontractorMock.Setup(x => x.OrderSteel(It.IsAny<int>())).Returns(new List<SteelDelivery> { new SteelDelivery() });
            var recipeMock = new Mock<IGetChassisRecipeQuery>();
            var recipe = new CarFactory_Domain.ChassisRecipe(CarFactory_Domain.Manufacturer.AstonPlanday, 0, 12, 0, 21, 0, 10);
            recipeMock.Setup(x => x.Get(It.IsAny<CarFactory_Domain.Manufacturer>())).Returns(recipe);
            var provider = new ChassisProvider(subcontractorMock.Object, recipeMock.Object);

            var result = provider.GetChassis(CarFactory_Domain.Manufacturer.AstonPlanday, 3);

            Assert.IsTrue(result.ValidConstruction);
            Assert.AreEqual("Two Door Sedan Sportcar", result.Description);
            Assert.AreEqual(50 - (12 + 21 + 10), provider.SteelInventory);
        }

        [TestMethod]
        public void GetChassis_throws_WhenNotEnoughSteel()
        {
            var subcontractorMock = new Mock<ISteelSubcontractor>();
            subcontractorMock.Setup(x => x.OrderSteel(It.IsAny<int>())).Returns(new List<SteelDelivery> { new SteelDelivery() });
            var recipeMock = new Mock<IGetChassisRecipeQuery>();
            var recipe = new CarFactory_Domain.ChassisRecipe(CarFactory_Domain.Manufacturer.AstonPlanday, 0, 36, 0, 21, 0, 10);
            recipeMock.Setup(x => x.Get(It.IsAny<CarFactory_Domain.Manufacturer>())).Returns(recipe);
            var provider = new ChassisProvider(subcontractorMock.Object, recipeMock.Object);

            // throw specific exception to make it more identifiable
            Assert.ThrowsException<Exception>(() => provider.GetChassis(CarFactory_Domain.Manufacturer.AstonPlanday, 3));
        }

        [TestMethod]
        public void GetChassis_throws_WhenUnknownFront()
        {
            var subcontractorMock = new Mock<ISteelSubcontractor>();
            subcontractorMock.Setup(x => x.OrderSteel(It.IsAny<int>())).Returns(new List<SteelDelivery> { new SteelDelivery() });
            var recipeMock = new Mock<IGetChassisRecipeQuery>();
            var recipe = new CarFactory_Domain.ChassisRecipe(CarFactory_Domain.Manufacturer.AstonPlanday, 7, 8, 0, 21, 0, 10);
            recipeMock.Setup(x => x.Get(It.IsAny<CarFactory_Domain.Manufacturer>())).Returns(recipe);
            var provider = new ChassisProvider(subcontractorMock.Object, recipeMock.Object);

            // throw specific exception to make it more identifiable
            Assert.ThrowsException<Exception>(() => provider.GetChassis(CarFactory_Domain.Manufacturer.AstonPlanday, 3));
        }

        [TestMethod]
        public void GetChassis_throws_WhenUnknownCabin()
        {
            var subcontractorMock = new Mock<ISteelSubcontractor>();
            subcontractorMock.Setup(x => x.OrderSteel(It.IsAny<int>())).Returns(new List<SteelDelivery> { new SteelDelivery() });
            var recipeMock = new Mock<IGetChassisRecipeQuery>();
            var recipe = new CarFactory_Domain.ChassisRecipe(CarFactory_Domain.Manufacturer.AstonPlanday, 1, 8, 7, 21, 0, 10);
            recipeMock.Setup(x => x.Get(It.IsAny<CarFactory_Domain.Manufacturer>())).Returns(recipe);
            var provider = new ChassisProvider(subcontractorMock.Object, recipeMock.Object);

            // throw specific exception to make it more identifiable
            Assert.ThrowsException<Exception>(() => provider.GetChassis(CarFactory_Domain.Manufacturer.AstonPlanday, 3));
        }

        [TestMethod]
        public void GetChassis_throws_WhenUnknownBack()
        {
            var subcontractorMock = new Mock<ISteelSubcontractor>();
            subcontractorMock.Setup(x => x.OrderSteel(It.IsAny<int>())).Returns(new List<SteelDelivery> { new SteelDelivery() });
            var recipeMock = new Mock<IGetChassisRecipeQuery>();
            var recipe = new CarFactory_Domain.ChassisRecipe(CarFactory_Domain.Manufacturer.AstonPlanday, 1, 8, 1, 21, 7, 10);
            recipeMock.Setup(x => x.Get(It.IsAny<CarFactory_Domain.Manufacturer>())).Returns(recipe);
            var provider = new ChassisProvider(subcontractorMock.Object, recipeMock.Object);

            // throw specific exception to make it more identifiable
            Assert.ThrowsException<Exception>(() => provider.GetChassis(CarFactory_Domain.Manufacturer.AstonPlanday, 3));
        }

        [TestMethod]
        public void GetChassis_parallel_IncrementDecrementCorrect()
        {
            var subcontractorMock = new Mock<ISteelSubcontractor>();
            subcontractorMock.Setup(x => x.OrderSteel(It.IsAny<int>())).Returns(new List<SteelDelivery> { new SteelDelivery() });
            var recipeMock = new Mock<IGetChassisRecipeQuery>();
            var frontCost = 4;
            var cabinCost = 11;
            var backCost = 2;
            var recipe = new CarFactory_Domain.ChassisRecipe(CarFactory_Domain.Manufacturer.AstonPlanday, 0, frontCost, 1, cabinCost, 2, backCost);
            recipeMock.Setup(x => x.Get(It.IsAny<CarFactory_Domain.Manufacturer>())).Returns(recipe);

            var outputInventory = 0;
            for(int i = 0; i < 20; i++)
            {                
                var provider = new ChassisProvider(subcontractorMock.Object, recipeMock.Object);
                Parallel.For(0, 75, index =>
                {
                    var result = provider.GetChassis(CarFactory_Domain.Manufacturer.AstonPlanday, 3);

                    outputInventory = provider.SteelInventory;
                });
                Console.WriteLine($"Output: {outputInventory}");
            }
            Assert.IsTrue(outputInventory == 25);
        }
    }
}
