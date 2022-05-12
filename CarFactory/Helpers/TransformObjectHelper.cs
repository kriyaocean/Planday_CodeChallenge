using CarFactory.Enum;
using CarFactory_Domain;
using CarFactory_Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using static CarFactory.Controllers.CarController;
using static CarFactory_Factory.CarSpecification;

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
                    var dashboardSpeakers = GetSpeakerSpecificationItems(spec.Specification.FrontWindowSpeakers);
                    var doorSpeakers = GetSpeakerSpecificationItems(spec.Specification.DoorSpeakers);
                    var wantedCar = new CarSpecification(paint, spec.Specification.Manufacturer, spec.Specification.NumberOfDoors, doorSpeakers, dashboardSpeakers);
                    wantedCars.Add(wantedCar);
                }
            }
            return wantedCars;
        }

        private static IEnumerable<SpeakerSpecification> GetSpeakerSpecificationItems(SpeakerSpecificationInputModel inputSpeakers)
        {
            if(inputSpeakers == null)
                return new List<SpeakerSpecification>();

            var speakerList = new List<SpeakerSpecification>();
            for (int i = 0; i < inputSpeakers.NumberOfSubWoofers; i++)
            {
                speakerList.Add(new SpeakerSpecification { SpeakerType = SpeakerSpecType.SubWoofer });
            }
            for (int i = 0; i < inputSpeakers.Speakers.NumberOfSpeakers; i++)
            {
                speakerList.Add(new SpeakerSpecification { SpeakerType = inputSpeakers.Speakers.SpeakerType.Convert() });
            }

            return speakerList;
        }
    }
}
