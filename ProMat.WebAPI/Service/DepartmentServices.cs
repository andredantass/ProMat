using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public class DepartmentServices
    {
        DepartmentRepository _departmentRepository;
        public DepartmentServices()
        {
            var context = new DataContext();
            _departmentRepository = new DepartmentRepository(context);
        }
        public int SincronizeDepartmentWithBitrix()
        {
            Bitrix24Services _serviceBitrix = new Bitrix24Services();
            WebhookServices _serviceWebhook = new WebhookServices();

            try
            {
                IList<Webhook> lstWebhook = _serviceWebhook.GetWebhooks();
                DeleteAllDepartments();

                foreach (Webhook webhook in lstWebhook)
                {
                    IList<Department> lstDepartment = _serviceBitrix.GetDepartments(webhook.WebhookPath);

                    foreach (Department department in lstDepartment)
                    {
                        if (GetDepartmentsById(department.DepartmentId) == null)
                        {
                            department.WebhookId = webhook.WebhookID;
                            _departmentRepository.Add(department);
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
        public void DeleteAllDepartments()
        {
            _departmentRepository.DeleteAll();
        }
        public List<Department> GetDepartments()
        {
            return _departmentRepository.Get();
        }
        public Department GetDepartmentsById(int departmentId)
        {
            return _departmentRepository.GetByID(departmentId);
        }
    }
}
