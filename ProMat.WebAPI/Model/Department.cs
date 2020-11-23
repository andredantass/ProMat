using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProMat.WebAPI.Model
{
   
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public Webhook WebHook { get; set; }
        public int WebhookId { get; set; }
    }
}