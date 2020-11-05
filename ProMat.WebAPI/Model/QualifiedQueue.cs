using System;

namespace ProMat.WebAPI.Model
{
    public class QualifiedQueue
    {
        public int QualifiedQueueId { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string Situation { get; set; }
        public DateTime? DateBorn { get; set; } = new DateTime();
        public string PrevSituation { get; set; }
        public DateTime? DateJobEnd { get; set; } = new DateTime();
        public string SegJobReceive { get; set; }
        

    }
}