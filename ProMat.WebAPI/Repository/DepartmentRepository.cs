using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Repository
{
    public class DepartmentRepository
    {
        private DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            this._context = context;
        }
        public int Add(Department model)
        {
            if(model != null)
            {
                var ret = _context.Departments.Add(model);
                _context.SaveChanges();
                return (int)ret.State;
            }
            return 0;
        }
        public int Update(Department model)
        {
            var ret = _context.Departments.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
            return (int)ret.State;
        }
        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM Departments");
            _context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE NAME = 'Departments'");
        }
        public List<Department> Get()
        {
            return _context.Departments.Include(x => x.WebHook).
                OrderBy(x => x.DepartmentId).ToList();
        }
        public List<Department> GetByID(int departmentID)
        {
            var result = _context.Departments.Where(x => x.DepartmentId == departmentID);
            return result.ToList();
        }
    }
}
