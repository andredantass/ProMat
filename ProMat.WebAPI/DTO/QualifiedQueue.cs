using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.DTO
{
    public class QualifiedQueue
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Situation { get; set; }
        public string DateBorn { get; set; }
        public string DateWillBorn { get; set; }
        public string PrevSituation { get; set; }
        public string DateJobEnd { get; set; }
        public bool SegJobReceive { get; set; }
        public string PaidTen { get; set; }
    }
}
