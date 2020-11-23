using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public class WebhookServices
    {
        WeebhookRepository _weebhookRepository;
        public WebhookServices()
        {
            var context = new DataContext();
            _weebhookRepository = new WeebhookRepository(context);
        }
        public int InsertWebhook(Webhook model)
        {
            var ret = _weebhookRepository.Add(model);
            return ret;
        }
        public List<Webhook> GetWebhooks()
        {
            return _weebhookRepository.Get();
        }
     
    }
}
