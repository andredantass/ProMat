using Microsoft.EntityFrameworkCore;
using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Repository
{
    public class LeadRepository
    {
        private DataContext _context;

        public LeadRepository(DataContext context)
        {
            this._context = context;
        }
        public int Add(Lead model)
        {
            if (model != null)
            {
                var ret = _context.Lead.Add(model);
                _context.SaveChanges();
                return (int)ret.State;
            }
            return 0;
        }
        public int VerifyLead(Lead model)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                int leadId;
                command.CommandText = "SELECT count(distinct id), id FROM Lead WHERE name = " + $"'{model.Name.Trim().ToUpper()}'";
                _context.Database.OpenConnection();
                try
                {
                    using (DbDataReader dataReader = command.ExecuteReader())
                    {
                        dataReader.Read();
                        if(Convert.ToInt32(dataReader.GetValue(0)) == 0)
                        {
                            leadId = 0;
                        }
                        else
                        {
                            leadId = Convert.ToInt32(dataReader.GetValue(1));
                        }
                    }
                    return leadId;
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }
        }
        public List<int> QualifiedCount()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                List<int> lead = new List<int>();
                var qualifiedScript = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Repository\Scripts\qualifiedCount.sql");
                var disqualifiedScript = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Repository\Scripts\disqualifiedCount.sql");
                _context.Database.OpenConnection();
                try
                {
                    command.CommandText = disqualifiedScript;
                    using (DbDataReader dataReader = command.ExecuteReader())
                    {
                        dataReader.Read();
                        lead.Add(Convert.ToInt32(dataReader.GetValue(0)));
                        dataReader.Close();
                    }
                    command.CommandText = qualifiedScript;
                    using (DbDataReader dataReader = command.ExecuteReader())
                    {
                        dataReader.Read();
                        lead.Add(Convert.ToInt32(dataReader.GetValue(0)));
                        dataReader.Close();
                    }
                    return lead;
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }
        }
        public List<Lead> LeadList()
        {

            var result = _context.Lead.Where(x => x.LeadStatusId != 3).ToList();
            return result;

            //using (var command = _context.Database.GetDbConnection().CreateCommand())
            //{
            //    List<Lead> leads = new List<Lead>();
            //    Lead lead = new Lead();
            //    var sqlScript = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Repository\Scripts\leadList.sql");
            //    _context.Database.OpenConnection();
            //    try
            //    {
            //        command.CommandText = sqlScript;
            //        using (DbDataReader dataReader = command.ExecuteReader())
            //        {
            //            while(dataReader.Read())
            //            {
            //                lead.LeadStatusId = Convert.ToInt32(dataReader.GetValue(1));
            //                lead.Name = dataReader.GetString(2);
            //                lead.Email = dataReader.GetString(3);
            //                lead.Phone = dataReader.GetString(4);
            //                lead.Date = dataReader.GetDateTime(5);
            //                leads.Add(lead);
            //            }
            //        }
            //        return leads;
            //    }
            //    finally
            //    {
            //        _context.Database.CloseConnection();
            //    }
            //}
        }
    }
}
