using CarFactory_Domain.Engine;
using CarFactory_Engine;
using CarFactory_Storage;
using CarFactory_SubContractor;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass, TestCategory("UnitTests")]
    public class EngineProviderTests
    {
        private static Mock<IGetPistons> _pistonsMock;
        private static Mock<ISteelSubcontractor> _steelMock;
        private static Mock<IGetEngineSpecificationQuery> _engineSpecQueryMock;
        private static Mock<IMemoryCache> _memoryCacheMock;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            _pistonsMock = new Mock<IGetPistons>();
            _steelMock = new Mock<ISteelSubcontractor>();
            _engineSpecQueryMock = new Mock<IGetEngineSpecificationQuery>();
            _memoryCacheMock = new Mock<IMemoryCache>();

            _engineSpecQueryMock.Setup(x => x.GetForManufacturer(It.IsAny<CarFactory_Domain.Manufacturer>()))
                .Returns(new CarFactory_Domain.Engine.EngineSpecifications.EngineSpecification { Name = "Engine1", CylinderCount = 4, PropulsionType = Propulsion.Diesel});
            _steelMock.Setup(x => x.OrderSteel(It.IsAny<int>())).Returns(new List<SteelDelivery> { new SteelDelivery() });
            _pistonsMock.Setup(x => x.Get(It.IsAny<int>())).Returns(6);
        }

        [TestMethod]
        public void GetEngine()
        {
            var engineProvider = new EngineProvider(_pistonsMock.Object, _steelMock.Object, _engineSpecQueryMock.Object, _memoryCacheMock.Object);
            var engine = engineProvider.GetEngine(CarFactory_Domain.Manufacturer.PlanfaRomeo);

            Assert.AreEqual("Engine1", engine.EngineSpecification);
            Assert.AreEqual(6, engine.PistonsCount);
            Assert.AreEqual(40, engine.EngineBlock.Volume);
            Assert.AreEqual(4, engine.EngineBlock.CylinderCount);
            Assert.AreEqual(Propulsion.Diesel, engine.PropulsionType.Value);
            Assert.IsTrue(engine.IsFinished);
            Assert.AreEqual(50 - (4 * EngineBlock.RequiredSteelPerCylinder), engineProvider.SteelInventory);
        }

        [TestMethod]
        public void GetEngine_parallel_IncrementDecrementCorrect()
        {
            var outputInventory = 0;
            for (int i = 0; i < 6; i++)
            {
                var provider = new EngineProvider(_pistonsMock.Object, _steelMock.Object, _engineSpecQueryMock.Object, _memoryCacheMock.Object);

                Parallel.For(0, 75, index =>
                {
                    var engine = provider.GetEngine(CarFactory_Domain.Manufacturer.PlanfaRomeo);

                    outputInventory = provider.SteelInventory;
                });
                Console.WriteLine($"Output: {outputInventory}");
            }
            Assert.IsTrue(outputInventory == 0);
        }
    }
}
