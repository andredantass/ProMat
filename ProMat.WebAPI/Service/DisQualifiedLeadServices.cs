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
        public int ReprocessDisQualifiedLeadsContingency()
        {
            DepartmentServices _serviceDepartment = new DepartmentServices();
            Bitrix24Services objService = new Bitrix24Services();

            try
            {
                foreach (DisqualifiedLead lead in GetAll())
                {

                    Department itemDepartment = _serviceDepartment.GetDepartmentsById(lead.DepartmentID);
                    string webHookPath = itemDepartment.WebHook.WebhookPath;

                    objService.CreateDisQualifiedLead(lead, webHookPath, "lead@hotmail.com", "", true);

                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public IList<DisqualifiedLead> GetAll()
        {
            return _disQualifiedLeadRepository.GetAll();
        }
        public DisqualifiedLead GetLastDisQualifiedLeadInsertedAttendant(int departmentID)
        {
            DisqualifiedLead _quaifiedLead = _disQualifiedLeadRepository.GetLastDisQualifiedLeadInsertedAttendant(departmentID);
            return _quaifiedLead;
        }
        public int DeleteAll()
        {
            try
            {
                _disQualifiedLeadRepository.DeleteAll();
                return 1;
            }
            catch
            {
                return 0;
            }

        }
    }
}
