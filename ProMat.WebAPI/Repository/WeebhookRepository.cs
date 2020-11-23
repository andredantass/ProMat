using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Repository
{
    public class WeebhookRepository
    {
        private DataContext _context;

        public WeebhookRepository(DataContext context)
        {
            this._context = context;
        }
        public int Add(Webhook model)
        {
            if(model != null)
            {
                var ret = _context.Webhook.Add(model);
                _context.SaveChanges();
                return (int)ret.State;
            }
            return 0;
        }
        public int Update(Webhook model)
        {
            var ret = _context.Webhook.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
            return (int)ret.State;
        }
        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM Webhook");
            _context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE NAME = 'Webhook'");
        }
        public List<Webhook> Get()
        {
            return _context.Webhook.ToList();
        }
        public List<Webhook> GetByID(int webHookId)
        {
            var result = _context.Webhook.Where(x => x.WebhookID == webHookId);
            return result.ToList();
        }
    }
}
