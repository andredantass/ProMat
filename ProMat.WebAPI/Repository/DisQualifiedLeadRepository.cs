using Microsoft.EntityFrameworkCore;
using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Repository
{
    public class DisQualifiedLeadRepository
    {
        private DataContext _context;

        public DisQualifiedLeadRepository(DataContext context)
        {
            this._context = context;
        }

        public int Add(DisqualifiedLead model)
        {
            var ret = _context.DisqualifiedLeads.Add(model);
            _context.SaveChanges();
            return (int)ret.State;
        }
        public DisqualifiedLead GetLastDisQualifiedLead()
        {
            DisqualifiedLead ret = _context.DisqualifiedLeads.OrderByDescending(x => x.DisqualifiedLeadId).FirstOrDefault();
            return ret;
        }
        public IList<DisqualifiedLead> GetAll()
        {
            return _context.DisqualifiedLeads.ToList();
        }
        public DisqualifiedLead GetLastDisQualifiedLeadInsertedAttendant(int departmentID)
        {
            DisqualifiedLead ret = _context.DisqualifiedLeads
                                            .Where(y => y.DepartmentID == departmentID)
                                            .OrderByDescending(x => x.DisqualifiedLeadId).FirstOrDefault();
            return ret;
        }
        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM DisqualifiedLeads");
            _context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE NAME = 'DisqualifiedLeads'");
        }
    }
}
