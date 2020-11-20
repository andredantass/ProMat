using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public class DisQualifiedLeadServices
    {
        private DisQualifiedLeadRepository _disQualifiedLeadRepository;

        public DisQualifiedLeadServices()
        {
            var context = new DataContext();
            _disQualifiedLeadRepository = new DisQualifiedLeadRepository(context);
        }

        public int InsertDisQualifiedLead(DisqualifiedLead model)
        {
            var ret = _disQualifiedLeadRepository.Add(model);
            return ret;
        }
        public DisqualifiedLead GetLastDisQualifiedLeadInserted()
        {
            DisqualifiedLead _quaifiedLead = _disQualifiedLeadRepository.GetLastDisQualifiedLead();
            return _quaifiedLead;
        }
        public DisqualifiedLead GetLastDisQualifiedLeadInsertedAttendant(int departmentID)
        {
            DisqualifiedLead _quaifiedLead = _disQualifiedLeadRepository.GetLastDisQualifiedLeadInsertedAttendant(departmentID);
            return _quaifiedLead;
        }
    }
}
