using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public class AttendantServices
    {
        AttendantRepository _attendantRepository;
        public AttendantServices()
        {
            var context = new DataContext();
            _attendantRepository = new AttendantRepository(context);
        }
        public int InsertAttendant(Attendant model)
        {
            var ret = _attendantRepository.Add(model);
            return ret;
        }
        public int SincronizeAttendantWithBitrix()
        {
            Bitrix24Services _serviceBitrix = new Bitrix24Services();
            WebhookServices _serviceWebhook = new WebhookServices();
            DepartmentServices _serviceDepartment = new DepartmentServices();

            try
            {
                IList<Webhook> lstWebhook = _serviceWebhook.GetWebhooks();
                DeleteAllAttendants();

                foreach (Webhook webhook in lstWebhook)
                {
                    IList<Department> lstDepartment = _serviceBitrix.GetDepartments(webhook.WebhookPath);

                    foreach (Department departItem in lstDepartment)
                    {
                        IList<Attendant> lstAttendant = _serviceBitrix.GetEmployeeDepartments(webhook.WebhookPath,
                                                                                        departItem.DepartmentId);

                        foreach (Attendant attendants in lstAttendant)
                        {
                            if (GetAttendantsByID(attendants.AttendantId).Count == 0)
                                _attendantRepository.Add(attendants);
                        }
                    }

                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public void DeleteAllAttendants()
        {
            _attendantRepository.DeleteAll();
        }
        public List<Attendant> GetAttendants()
        {
            return _attendantRepository.Get();
        }
        public List<Attendant> GetAttendantsByID(int attendantID)
        {
            return _attendantRepository.GetByID(attendantID);
        }
        public List<Attendant> GetAttendantsByCompanyId(int departmentID)
        {
            return _attendantRepository.GetByDepartmentID(departmentID);
        }
    }
}
