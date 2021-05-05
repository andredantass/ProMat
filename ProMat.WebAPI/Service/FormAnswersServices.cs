using ProMat.WebAPI.Data;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public class FormAnswersServices
    {
        private FormAnswersRepository _formAnswersRepository;

        public FormAnswersServices()
        {
            var context = new DataContext();
            _formAnswersRepository = new FormAnswersRepository(context);
        }

        public int InsertAnswers(FormAnswer model)
        {
            var ret = _formAnswersRepository.Add(model);
            return ret;
        }
    }
}