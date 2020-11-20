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
        public int InsertDepartment(Department model)
        {
            var ret = _departmentRepository.Add(model);
            return ret;
        }
        public List<Department> GetDepartments()
        {
            return _departmentRepository.Get();
        }
        public List<Department> GetDepartmentsById(int departmentId)
        {
            return _departmentRepository.GetByID(departmentId);
        }
    }
}
