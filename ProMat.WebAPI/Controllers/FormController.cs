using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Google.Apis.Services;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.DTO;
using ProMat.WebAPI.Service;

namespace ProMat.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        /// <summary>
        /// Criar ou Atualizar um processo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidateFormBorn")]
        public ResponseDTO ValidateFormBorn([FromBody] QualifiedQueue model)
        {
            FormServices formAnswerService = new FormServices();
            var response = new ResponseDTO();

            try
            {
                var ret = formAnswerService.CheckQualifieBornQuestionForm(model);

                if (ret)
                {
                    response.Data = new { sucesso = 1, message = "Formulario validado com sucesso!" };
                }
                else
                {
                    response.Data = new { sucesso = 0, message = "Não foi possível validar o formulário!" };
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        /// <summary>
        /// Criar ou Atualizar um processo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidateFormNoBorn")]
        public ResponseDTO ValidateFormNoBorn([FromBody] QualifiedQueue model)
        {
            FormServices formAnswerService = new FormServices();
            var response = new ResponseDTO();

            try
            {
                var ret = formAnswerService.CheckQualifieNoBornQuestionForm(model);

                if (ret)
                {
                    response.Data = new { sucesso = 1, message = "Formulario validado com sucesso!" };
                }
                else
                {
                    response.Data = new { sucesso = 0, message = "Não foi possível validar o formulário!" };
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        [HttpGet]
        [Route("Authenticated")]
        public string Authenticated() => String.Format("Autenticado pelo Token");

        [HttpGet]
        [Route("ReadData")]
        public void ReadData()
        {
            Service.GoogleServices googleService = new Service.GoogleServices();
            googleService.ReadGoogleSheets();
        }
      
       
    }
}