using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Model
{
    public class FormAnswer
    {
        [Key]
        public int Id { get; set; }
        public int LeadId { get; set; }
        public string Situation { get; set; }
        public string DateBorn { get; set; }
        public string DateWillBorn { get; set; }
        public string WorkRegistered { get; set; }
        public string DateJobEnd { get; set; }
        public string SegJobReceive { get; set; }
        public string Contributed { get; set; }
        public bool Qualified { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
