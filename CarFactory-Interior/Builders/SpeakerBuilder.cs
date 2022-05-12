using System.Collections.Generic;
using System.Linq;
using CarFactory_Interior.Interfaces;
using static CarFactory_Factory.CarSpecification;

namespace CarFactory_Interior.Builders
{
    public class SpeakerBuilder : ISpeakerBuilder
    {
        public List<CarFactory_Domain.BaseSpeaker> BuildSpeakers(IEnumerable<SpeakerSpecification> specification)
        {
            var speakers = new List<CarFactory_Domain.BaseSpeaker>();

            speakers.AddRange(specification
                .Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.Standard)
                .Select(x => new CarFactory_Domain.StandardSpeaker()));
            speakers.AddRange(specification
                .Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.Normal)
                .Select(x => new CarFactory_Domain.NormalSpeaker()));
            speakers.AddRange(specification
                .Where(x => x.SpeakerType == CarFactory_Factory.SpeakerSpecType.SubWoofer)
                .Select(x => new CarFactory_Domain.SubWoofer()));

            return speakers;
        }
    }
}