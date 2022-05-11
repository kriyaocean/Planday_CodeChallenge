using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CarFactory.Enum;
using CarFactory.Helpers;
using CarFactory_Domain;
using CarFactory_Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarFactory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarFactory _carFactory;
        public CarController(ICarFactory carFactory)
        {
            _carFactory = carFactory;
        }

        [ProducesResponseType(typeof(BuildCarOutputModel), StatusCodes.Status200OK)]
        [HttpPost]
        public object Post([FromBody][Required] BuildCarInputModel carsSpecs)
        {

            var wantedCars = TransformObjectHelper.TransformToDomainObjects(carsSpecs);
            //Build cars
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var cars = _carFactory.BuildCars(wantedCars);
            stopwatch.Stop();

            //Create response and return
            return new BuildCarOutputModel {
                Cars = cars,
                RunTime = stopwatch.ElapsedMilliseconds
            };
        }

        public class BuildCarInputModel
        {
            public IEnumerable<BuildCarInputModelItem> Cars { get; set; }
        }

        public class BuildCarInputModelItem
        {
            [Required]
            public int Amount { get; set; }
            [Required]
            public CarSpecificationInputModel Specification { get; set; }
        }

        public class CarPaintSpecificationInputModel
        {
            public PaintType Type { get; set; }
            public string BaseColor { get; set; }
            public string? StripeColor { get; set; }
            public string? DotColor { get; set; }
        }

        public class CarSpecificationInputModel
        {
            public int NumberOfDoors { get; set; }
            public CarPaintSpecificationInputModel Paint { get; set; }
            public Manufacturer Manufacturer { get; set; }
            public SpeakerSpecificationInputModel[] FrontWindowSpeakers { get; set; }
        }

        public class SpeakerSpecificationInputModel
        {
            public bool IsSubwoofer { get; set; }
        }

        public class BuildCarOutputModel{
            public long RunTime { get; set; }
            public IEnumerable<Car> Cars { get; set; }
        }
    }
}
