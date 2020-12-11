using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public class QualifiedLeadServices
    {
        private QualifiedLeadReporitory _qualifiedLeadRepository;

        public QualifiedLeadServices()
        {
            var context = new DataContext();
            _qualifiedLeadRepository = new QualifiedLeadReporitory(context);
        }

        public int InsertQualifiedLead(QualifiedLead model)
        {
            var ret = _qualifiedLeadRepository.Add(model);
            return ret;
        }
        public QualifiedLead GetLastQualifiedLeadInsertedAttendant(int departmentID)
        {
            QualifiedLead _quaifiedLead = _qualifiedLeadRepository.GetLastQualifiedLeadInsertedAttendant(departmentID);
            return _quaifiedLead;
        }
        public QualifiedLead GetLastQualifiedLeadInserted()
        {
            QualifiedLead _quaifiedLead = _qualifiedLeadRepository.GetLastQualifiedLead();
            return _quaifiedLead;
        }
        public int ReprocessQualifiedLeadsContingency()
        {
            DepartmentServices _serviceDepartment = new DepartmentServices();
            Bitrix24Services objService = new Bitrix24Services();

            try
            {
                foreach (QualifiedLead lead in GetAll())
                {

                    Department itemDepartment = _serviceDepartment.GetDepartmentsById(lead.DepartmentID);
                    string webHookPath = itemDepartment.WebHook.WebhookPath;
                   
                    objService.CreateQualifiedLead(lead, webHookPath, "lead@hotmail.com", "", true);

                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public IList<QualifiedLead> GetAll()
        {
            return _qualifiedLeadRepository.GetAll();
        }
        public int DeleteAll()
        {
            try
            {
                _qualifiedLeadRepository.DeleteAll();
                return 1;
            }
            catch
            {
                return 0;
            }

        }
    }
}
