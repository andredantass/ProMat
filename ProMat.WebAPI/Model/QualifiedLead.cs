using System;
using System.ComponentModel.DataAnnotations;

namespace ProMat.WebAPI.Model
{
    public class QualifiedLead
    {
        [Key]
        public int QualifiedLeadId { get; set; }
        public int DepartmentID { get; set; }
        public int AttendantID { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string Situation { get; set; }
        public string DateBorn { get; set; }
        public string PrevSituation { get; set; }
        public string DateJobEnd { get; set; }
        public string SegJobReceive { get; set; }
        public DateTime InsertDate { get; set; }


    }
}