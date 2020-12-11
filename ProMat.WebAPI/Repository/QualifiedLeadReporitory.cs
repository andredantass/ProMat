using Microsoft.EntityFrameworkCore;
using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Repository
{
    public class QualifiedLeadReporitory
    {
        private DataContext _context;

        public QualifiedLeadReporitory(DataContext context)
        {
            this._context = context;
        }

        public int Add(QualifiedLead model)
        {
            var ret = _context.QualifiedLeads.Add(model);
            _context.SaveChanges();
            return (int)ret.State;
        }
        public QualifiedLead GetLastQualifiedLead()
        {
            QualifiedLead ret = _context.QualifiedLeads.OrderByDescending(x => x.QualifiedLeadId).FirstOrDefault();
            return ret;
        }
        public IList<QualifiedLead> GetAll()
        {
            return _context.QualifiedLeads.ToList();
        }
        public QualifiedLead GetLastQualifiedLeadInsertedAttendant(int departmentID)
        {
            QualifiedLead ret = _context.QualifiedLeads
                                            .Where(y => y.DepartmentID == departmentID)
                                            .OrderByDescending(x => x.QualifiedLeadId).FirstOrDefault();
            return ret;
        }
        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM QualifiedLeads");
            _context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE NAME = 'QualifiedLeads'");
        }
    }
}
