using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Repository
{
    public class AttendantRepository
    {
        private DataContext _context;

        public AttendantRepository(DataContext context)
        {
            this._context = context;
        }
        public int Add(Attendant model)
        {
            if(model != null)
            {
                var ret = _context.Attendants.Add(model);
                _context.SaveChanges();
                return (int)ret.State;
            }
            return 0;
        }
        public int Update(Attendant model)
        {
            var ret = _context.Attendants.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
            return (int)ret.State;
        }
        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM Attendants");
            _context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE NAME = 'Attendants'");
        }
        public List<Attendant> Get()
        {
            return _context.Attendants.ToList();
        }
        public List<Attendant> GetByDepartmentID(int departmentID)
        {
            var result = _context.Attendants.Where(x => x.DepartmentId == departmentID);
            return result.ToList();
        }
    }
}
