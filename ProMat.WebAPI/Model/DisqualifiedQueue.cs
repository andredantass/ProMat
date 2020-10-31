using System;

namespace ProMat.WebAPI.Model
{
    public class DisqualifiedQueue
    {
        public int DisqualifiedQueueId { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public DateTime Situation { get; set; }
        public string DateBorn { get; set; }
        public string PrevSituation { get; set; }
        public DateTime DateJobEnd { get; set; }
    }
}