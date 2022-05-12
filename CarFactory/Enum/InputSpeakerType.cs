using CarFactory_Factory;
using System;

namespace CarFactory.Enum
{
    public enum InputSpeakerType
    {
        Standard = 1,
        Normal = 2,
    }

    public static class SpeakerTypeExtensions
    {
        public static SpeakerSpecType Convert(this InputSpeakerType type)
        {
            switch (type)
            {
                case InputSpeakerType.Standard:
                    return SpeakerSpecType.Standard;
                case InputSpeakerType.Normal:
                    return SpeakerSpecType.Normal;
                default:
                    throw new ArgumentException("Unsupported speaker type");
            }    
        }
    }
}
