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
    public class ControlRepository
    {
        private DataContext _context;
        public ControlRepository(DataContext context)
        {
            this._context = context;
        }
        public int Add(Control model)
        {
            if (model != null)
            {
                var ret = _context.Visits.Add(model);
                _context.SaveChanges();
                return (int)ret.State;
            }
            return 0;
        }
        public int LeadCount()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                int leadCount;
                var sqlScript = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Repository\Scripts\leadCount.sql");
                command.CommandText = sqlScript;
                _context.Database.OpenConnection();
                try
                {
                    using (DbDataReader dataReader = command.ExecuteReader())
                    {
                        dataReader.Read();
                        leadCount = Convert.ToInt32(dataReader.GetValue(0));
                    }
                    return leadCount;
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }
        }
        public int VisitsCount()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                int visits;
                var sqlScript = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Repository\Scripts\visitsCount.sql");
                command.CommandText = sqlScript;
                _context.Database.OpenConnection();
                try
                {
                    using (DbDataReader dataReader = command.ExecuteReader())
                    {
                        dataReader.Read();
                        visits = Convert.ToInt32(dataReader.GetValue(0));
                    }
                    return visits;
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }
        }
    }
}
