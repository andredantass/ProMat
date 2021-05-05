using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Model
{
    public class Lead
    {
        [Key]
        public int Id { get; set; }
        public int LeadStatusId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
    }
}
