using CarFactory.Enum;
using CarFactory_Factory;
using System;

namespace CarFactory.Extensions
{
    public static class InputSpeakerTypeExtension
    {
        public static SpeakerSpecType ConvertToSpeakerSpecType(this InputSpeakerType type)
        {
            return type switch
            {
                InputSpeakerType.Standard => SpeakerSpecType.Standard,
                InputSpeakerType.Normal => SpeakerSpecType.Normal,
                _ => throw new ArgumentException("Unsupported speaker type"),
            };
        }
    }
}
