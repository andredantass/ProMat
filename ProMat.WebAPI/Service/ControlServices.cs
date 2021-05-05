using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public class ControlServices
    {
        private ControlRepository _formControlRepository;
        private LeadRepository _leadRepository;
        public ControlServices()
        {
            var context = new DataContext();
            _formControlRepository = new ControlRepository(context);
            _leadRepository = new LeadRepository(context);
        }

        public int RegisterVisit(string path, string ip, string location)
        {
            Control model = new Control()
            {
                Path = path,
                VisitTime = DateTime.Now.AddHours(4),
                Ip = ip,
                Location = location
            };
            var ret = _formControlRepository.Add(model);
            return ret;
        }
        public int LeadCount()
        {
            var count = _formControlRepository.LeadCount();
            return count;
        }
        public List<int> QualifiedCount()
        {
            List<int> lead = _leadRepository.QualifiedCount();
            
            return lead;
        }
        public int VisitsCount()
        {
            var visits = _formControlRepository.VisitsCount();
            return visits;
        }
    }
}
