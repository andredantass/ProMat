using System;
using System.ComponentModel.DataAnnotations;

namespace ProMat.WebAPI.Model
{
    public class FullForm
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Situation { get; set; }
        public string DateBorn { get; set; }
        public string DateWillBorn { get; set; }
        public string PrevSituation { get; set; }
        public string DateJobEnd { get; set; }
        public string SegJobReceive { get; set; }
        public string PaidTen { get; set; }
    }
}