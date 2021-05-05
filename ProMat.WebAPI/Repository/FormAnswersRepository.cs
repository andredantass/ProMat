using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Repository
{
    public class FormAnswersRepository
    {
        private DataContext _context;
        public FormAnswersRepository(DataContext context)
        {
            this._context = context;
        }
        public int Add(FormAnswer model)
        {
            if (model != null)
            {
                var ret = _context.FormAnswers.Add(model);
                _context.SaveChanges();
                return (int)ret.State;
            }
            return 0;
        }
    }
}
