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
        public List<Attendant> GetAttendants()
        {
            return _attendantRepository.Get();
        }
        public List<Attendant> GetAttendantsByCompanyId(int departmentID)
        {
            return _attendantRepository.GetByDepartmentID(departmentID);
        }
    }
}
