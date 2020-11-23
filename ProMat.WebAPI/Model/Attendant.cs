using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProMat.WebAPI.Model
{
    public class Root
    {
        [JsonProperty("result")]
        public List<Attendant> lstUser { get; set; }
    }
    public class Attendant
    {
        public int AttendantId { get; set; }
        public int DepartmentId { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}