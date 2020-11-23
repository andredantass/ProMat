using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProMat.WebAPI.Model
{
   
    public class Webhook
    {
        [Key]
        public int WebhookID { get; set; }
        public string WebhookPath { get; set; }
       
    }
}