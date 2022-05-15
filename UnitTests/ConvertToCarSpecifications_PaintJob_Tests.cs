using AutoFixture;
using CarFactory.Enum;
using CarFactory.Extensions;
using CarFactory_Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using static CarFactory.Controllers.CarController;

namespace UnitTests
{
    [TestClass, TestCategory("UnitTests")]
    public class ConvertToCarSpecifications_PaintJob_Tests
    {
        private static readonly Fixture Fixture = new();

        [TestMethod]
        public void ConvertToCarSpecifications_StripedPaintJob()
        {
            var car = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 1).Create();
            car.Specification.Paint = new CarPaintSpecificationInputModel { BaseColor = "Blue", StripeColor = "Orange", Type = PaintType.Stripe };
            car.Specification.NumberOfDoors = 3;

            var result = new BuildCarInputModel { Cars = new List<BuildCarInputModelItem> { car } }
            .ConvertToCarSpecifications().ToList();

            Assert.IsInstanceOfType(result[0].PaintJob, typeof(StripedPaintJob));
        }

        [TestMethod]
        public void ConvertToCarSpecifications_DottedPaintJob()
        {
            var car = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 1).Create();
            car.Specification.Paint = new CarPaintSpecificationInputModel { BaseColor = "Green", DotColor = "Yellow", Type = PaintType.Dot };
            car.Specification.NumberOfDoors = 3;

            var result = new BuildCarInputModel { Cars = new List<BuildCarInputModelItem> { car } }
            .ConvertToCarSpecifications().ToList();

            Assert.IsInstanceOfType(result[0].PaintJob, typeof(DottedPaintJob));
        }

        [TestMethod]
        public void ConvertToCarSpecifications_SingleColorPaintJob()
        {
            var car = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 1).Create();
            car.Specification.Paint = new CarPaintSpecificationInputModel { BaseColor = "Green", Type = PaintType.Single };
            car.Specification.NumberOfDoors = 3;

            var result = new BuildCarInputModel { Cars = new List<BuildCarInputModelItem> { car } }
            .ConvertToCarSpecifications().ToList();

            Assert.IsInstanceOfType(result[0].PaintJob, typeof(SingleColorPaintJob));
        }

        [TestMethod]
        public void ConvertToCarSpecifications_MultipleSpecifications_CreatesCorrectNumberOfInstances()
        {
            var car1 = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 2).Create();
            car1.Specification.Paint = new CarPaintSpecificationInputModel { BaseColor = "Green", Type = PaintType.Single };
            car1.Specification.NumberOfDoors = 3;

            var car2 = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 7).Create();
            car2.Specification.Paint = new CarPaintSpecificationInputModel { BaseColor = "Green", StripeColor = "Silver", Type = PaintType.Stripe };
            car2.Specification.NumberOfDoors = 5;

            var car3 = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 21).Create();
            car3.Specification.Paint = new CarPaintSpecificationInputModel { BaseColor = "Pink", DotColor = "Purple", Type = PaintType.Dot };
            car3.Specification.NumberOfDoors = 3;

            var result = new BuildCarInputModel { Cars = new List<BuildCarInputModelItem> { car1, car2, car3 } }
            .ConvertToCarSpecifications();

            var totalAmount = car1.Amount + car2.Amount + car3.Amount;

            var singleResult = result.Where(x => x.PaintJob.GetType() == typeof(SingleColorPaintJob)).ToList();
            var stripedResult = result.Where(x => x.PaintJob.GetType() == typeof(StripedPaintJob)).ToList();
            var dottedResult = result.Where(x => x.PaintJob.GetType() == typeof(DottedPaintJob)).ToList();

            Assert.AreEqual(totalAmount, result.ToList().Count);
            Assert.AreEqual(singleResult.Count, car1.Amount);
            Assert.AreEqual(stripedResult.Count, car2.Amount);
            Assert.AreEqual(dottedResult.Count, car3.Amount);
        }

        [TestMethod]
        public void ConvertToCarSpecifications_UnknownPaintType_Throws()
        {
            var car = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 1).Create();
            car.Specification.Paint = new CarPaintSpecificationInputModel { BaseColor = "Green", Type = default };
            car.Specification.NumberOfDoors = 3;

            Assert.ThrowsException<ArgumentException>(
                () => new BuildCarInputModel { Cars = new List<BuildCarInputModelItem> { car } }
            .ConvertToCarSpecifications());
        }

        [TestMethod]
        public void TConvertToCarSpecifications_Throws_OnEvenNumberOfDoors()
        {
            // should probably throw on odd number of doors in stead
            var car = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 1).Create();
            car.Specification.NumberOfDoors = 4;

            Assert.ThrowsException<ArgumentException>(
                () => new BuildCarInputModel { Cars = new List<BuildCarInputModelItem> { car } }
            .ConvertToCarSpecifications());
        }
    }
}
