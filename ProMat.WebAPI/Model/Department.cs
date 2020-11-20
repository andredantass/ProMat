using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProMat.WebAPI.Model
{
   
    public class Webhook
    {
        public int DepartmentId { get; set; }

        public string WebhookPath { get; set; }
       
    }
}