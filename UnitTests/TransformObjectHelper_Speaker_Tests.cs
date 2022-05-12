using AutoFixture;
using CarFactory.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarFactory.Controllers.CarController;

namespace UnitTests
{
    [TestClass]
    public class TransformObjectHelper_Speaker_Tests
    {
        private static readonly Fixture Fixture = new();

        [TestMethod]
        public void TransformObjectHelper_CreatesTheRightAmountOfSpeakerInstances()
        {
            var car = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 1).Create();
            car.Specification.FrontWindowSpeakers = new SpeakerSpecificationInputModel {
                NumberOfSubWoofers = 1,
                Speakers = new SpeakerInputModel { NumberOfSpeakers = 5, SpeakerType = CarFactory.Enum.InputSpeakerType.Standard } };
            car.Specification.DoorSpeakers = new SpeakerSpecificationInputModel
            {
                NumberOfSubWoofers = 2,
                Speakers = new SpeakerInputModel { NumberOfSpeakers = 0, SpeakerType = CarFactory.Enum.InputSpeakerType.Normal }
            };
            car.Specification.NumberOfDoors = 3;

            var result = TransformObjectHelper.TransformToDomainObjects(new BuildCarInputModel
            {
                Cars = new List<BuildCarInputModelItem> { car }
            }).ToList();

            var fwsp_standard = result[0].FrontWindowSpeakers.Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.Standard).Count();
            var fwsp_sub = result[0].FrontWindowSpeakers.Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.SubWoofer).Count();
            var dsp_normal = result[0].DoorSpeakers.Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.Normal).Count();
            var dsp_sub = result[0].DoorSpeakers.Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.SubWoofer).Count();

            Assert.AreEqual(fwsp_standard, car.Specification.FrontWindowSpeakers.Speakers.NumberOfSpeakers);
            Assert.AreEqual(fwsp_sub, car.Specification.FrontWindowSpeakers.NumberOfSubWoofers);
            Assert.AreEqual(dsp_normal, car.Specification.DoorSpeakers.Speakers.NumberOfSpeakers);
            Assert.AreEqual(dsp_sub, car.Specification.DoorSpeakers.NumberOfSubWoofers);
        }

        [TestMethod]
        public void TransformObjectHelper_CanHandleNull_FrondWindowSpeakers()
        {
            var car = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 1).Create();
            car.Specification.FrontWindowSpeakers = null;
            car.Specification.DoorSpeakers = new SpeakerSpecificationInputModel
            {
                NumberOfSubWoofers = 2,
                Speakers = new SpeakerInputModel { NumberOfSpeakers = 4, SpeakerType = CarFactory.Enum.InputSpeakerType.Normal }
            };
            car.Specification.NumberOfDoors = 3;

            var result = TransformObjectHelper.TransformToDomainObjects(new BuildCarInputModel
            {
                Cars = new List<BuildCarInputModelItem> { car }
            }).ToList();

            var fwsp = result[0].FrontWindowSpeakers.Count();
            var dsp_normal = result[0].DoorSpeakers.Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.Normal).Count();
            var dsp_sub = result[0].DoorSpeakers.Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.SubWoofer).Count();

            Assert.AreEqual(fwsp, 0);
            Assert.AreEqual(dsp_normal, car.Specification.DoorSpeakers.Speakers.NumberOfSpeakers);
            Assert.AreEqual(dsp_sub, car.Specification.DoorSpeakers.NumberOfSubWoofers);
        }

        [TestMethod]
        public void TransformObjectHelper_CanHandleNull_DoorSpeakers()
        {
            var car = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 1).Create();
            car.Specification.FrontWindowSpeakers = new SpeakerSpecificationInputModel
            {
                NumberOfSubWoofers = 1,
                Speakers = new SpeakerInputModel { NumberOfSpeakers = 5, SpeakerType = CarFactory.Enum.InputSpeakerType.Standard }
            };
            car.Specification.DoorSpeakers = null;
            car.Specification.NumberOfDoors = 3;

            var result = TransformObjectHelper.TransformToDomainObjects(new BuildCarInputModel
            {
                Cars = new List<BuildCarInputModelItem> { car }
            }).ToList();

            var fwsp_standard = result[0].FrontWindowSpeakers.Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.Standard).Count();
            var fwsp_sub = result[0].FrontWindowSpeakers.Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.SubWoofer).Count();
            var dsp = result[0].DoorSpeakers.Count();

            Assert.AreEqual(fwsp_standard, car.Specification.FrontWindowSpeakers.Speakers.NumberOfSpeakers);
            Assert.AreEqual(fwsp_sub, car.Specification.FrontWindowSpeakers.NumberOfSubWoofers);
            Assert.AreEqual(dsp, 0);
        }

        [TestMethod]
        public void TransformObjectHelper_CanHandleNull_Speakers()
        {
            var car = Fixture.Build<BuildCarInputModelItem>().With(x => x.Amount, 1).Create();
            car.Specification.FrontWindowSpeakers = null;
            car.Specification.DoorSpeakers = null;
            car.Specification.NumberOfDoors = 3;

            var result = TransformObjectHelper.TransformToDomainObjects(new BuildCarInputModel
            {
                Cars = new List<BuildCarInputModelItem> { car }
            }).ToList();

            var fwsp = result[0].FrontWindowSpeakers.Count();
            var dsp = result[0].DoorSpeakers.Count();

            Assert.AreEqual(fwsp, 0);
            Assert.AreEqual(dsp, 0);
        }
    }
}
