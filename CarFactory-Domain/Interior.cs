using System.Collections.Generic;

namespace CarFactory_Domain
{
    public class Interior
    {
        public IEnumerable<BaseSpeaker> FrontWindowSpeakers { get; set; }
        public IEnumerable<BaseSpeaker> DoorSpeakers { get; set; }
        public IEnumerable<Seat> Seats { get; set; }
        public Dashboard Dashboard { get; set; }
    }
}