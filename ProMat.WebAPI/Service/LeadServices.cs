using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public class LeadServices
    {
        private LeadRepository _leadRepository;

        public LeadServices()
        {
            var context = new DataContext();
            _leadRepository = new LeadRepository(context);
        }

        public int InsertLead(Lead model)
        {
            var ret = _leadRepository.Add(model);
            return ret;
        }
        public int VerifyLead(Lead model)
        {
            int leadId = _leadRepository.VerifyLead(model);
            return leadId;
        }
        public List<List<string>> LeadList()
        {
            List<List<string>> leads = _leadRepository.
        }

    }
}
