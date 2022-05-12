namespace CarFactory_Domain
{
    public abstract class BaseSpeaker
    {
        public abstract string SpeakerTypeName { get; }
    }

    public class StandardSpeaker : BaseSpeaker
    {
        public override string SpeakerTypeName => "Standard speaker"; 
    }

    public class NormalSpeaker : BaseSpeaker
    {
        public override string SpeakerTypeName => "Normal speaker";
    }

    public class SubWoofer : BaseSpeaker
    {
        public override string SpeakerTypeName => "SubWoofer";
    }
}