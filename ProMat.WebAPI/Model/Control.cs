using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Model
{
    public class Control
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime VisitTime { get; set; }
        public string Ip { get; set; }
        public string Location { get; set; }
    }
}
