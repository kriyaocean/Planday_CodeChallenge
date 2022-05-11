using CarFactory.Controllers;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Factory;
using CarFactory_Paint;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Drawing;
using static CarFactory.Controllers.CarController;

namespace UnitTests
{
    [TestClass]
    public class PainterTests
    {
        [TestMethod]
        public void Painter_PaintJobTest()
        {
            var singleColor = new SingleColorPaintJob(Color.Aqua);
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10),"Test"), new Interior(), new Wheel[4]);
            painter.PaintCar(car, singleColor);
            var job = (SingleColorPaintJob)car.PaintJob;
            job.Color.Should().Be(Color.Aqua);
            job.AreInstructionsUnlocked().Should().BeTrue();
        }
    }
}
