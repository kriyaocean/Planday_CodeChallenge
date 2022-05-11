using CarFactory.Enum;
using CarFactory_Domain;
using CarFactory_Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static CarFactory.Controllers.CarController;

namespace CarFactory.Helpers
{
    public static class TransformObjectHelper
    {
        public static IEnumerable<CarSpecification> TransformToDomainObjects(BuildCarInputModel carsSpecs)
        {
            //Check and transform specifications to domain objects
            var wantedCars = new List<CarSpecification>();
            foreach (var spec in carsSpecs.Cars)
            {
                for (var i = 1; i <= spec.Amount; i++)
                {
                    if (spec.Specification.NumberOfDoors % 2 == 0)
                    {
                        throw new ArgumentException("Must give an odd number of doors");
                    }
                    PaintJob? paint = null;
                    var baseColor = Color.FromName(spec.Specification.Paint.BaseColor);
                    switch (spec.Specification.Paint.Type)
                    {
                        case PaintType.Single:
                            paint = new SingleColorPaintJob(baseColor);
                            break;
                        case PaintType.Stripe:
                            paint = new StripedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.StripeColor));
                            break;
                        case PaintType.Dot:
                            paint = new DottedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.DotColor));
                            break;
                        default:
                            throw new ArgumentException(string.Format("Unknown paint type %", spec.Specification.Paint.Type));
                    }
                    var dashboardSpeakers = spec.Specification.FrontWindowSpeakers.Select(s => new CarSpecification.SpeakerSpecification { IsSubwoofer = s.IsSubwoofer });
                    var doorSpeakers = new CarSpecification.SpeakerSpecification[0]; //TODO: Let people install door speakers
                    var wantedCar = new CarSpecification(paint, spec.Specification.Manufacturer, spec.Specification.NumberOfDoors, doorSpeakers, dashboardSpeakers);
                    wantedCars.Add(wantedCar);
                }
            }
            return wantedCars;
        }

    }
}
