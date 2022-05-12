using System.Collections.Generic;
using CarFactory_Domain;
using static CarFactory_Factory.CarSpecification;

namespace CarFactory_Interior.Interfaces
{
    public interface ISpeakerBuilder
    {
        List<CarFactory_Domain.BaseSpeaker> BuildSpeakers(IEnumerable<SpeakerSpecification> specification);
    }
}