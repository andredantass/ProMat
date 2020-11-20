﻿using ProMat.WebAPI.Data;
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
        public QualifiedLead GetLastQualifiedLeadInsertedAttendant(int departmentID)
        {
            QualifiedLead ret = _context.QualifiedLeads
                                            .Where(y => y.DepartmentID == departmentID)
                                            .OrderByDescending(x => x.QualifiedLeadId).FirstOrDefault();
            return ret;
        }
    }
}
