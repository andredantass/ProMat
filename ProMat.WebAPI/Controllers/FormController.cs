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
        public ResponseDTO ValidateFormBorn([FromBody] QualifiedLead model)
        {
            FormServices formAnswerService = new FormServices();
            var response = new ResponseDTO();

            try
            {
                var retQualified = formAnswerService.CheckQualifieBornQuestionForm(model);
                YouAre youAreObj = (YouAre)Enum.Parse(typeof(YouAre), model.Situation);
                bool ret = false;

                if (retQualified)
                {
                    if (youAreObj == YouAre.MotherChildLessFiveYears)
                        ret = formAnswerService.InsertQualifiedLeadtoBitrixQueue(model, retQualified, "135");
                }
                else
                {
                    ret = formAnswerService.InsertDisQualifiedLeadtoBitrixQueue(model, retQualified, "NOQUALIFIED");
                }

                //var ret = formAnswerService.InsertLeadToGoogleDoc(model, retQualified);

                if (ret)
                {
                    response.Data = new { sucesso = 1, message = "Formulario validado com sucesso!" };
                }
                else
                {
                    response.Data = new { sucesso = 0, message = "N�o foi poss�vel validar o formul�rio!" };
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
        public ResponseDTO ValidateFormNoBorn([FromBody] QualifiedLead model)
        {
            FormServices formAnswerService = new FormServices();
            var response = new ResponseDTO();

            try
            {
                var retQualified = formAnswerService.CheckQualifieNoBornQuestionForm(model);
                YouAre youAreObj = (YouAre)Enum.Parse(typeof(YouAre), model.Situation);

                if (retQualified)
                {
                    if (youAreObj == YouAre.PregnantChildLessFiveYears)
                        formAnswerService.InsertQualifiedLeadtoBitrixQueue(model, retQualified, "PLUSS");
                    else if (youAreObj == YouAre.PregnantFirstChild || youAreObj == YouAre.PregnantChildMoreFiveYears)
                        formAnswerService.InsertQualifiedLeadtoBitrixQueue(model, retQualified, "DPP");
                }
                else
                {
                    formAnswerService.InsertDisQualifiedLeadtoBitrixQueue(model, retQualified, "NOQUALIFIED");
                }

                var ret = formAnswerService.InsertLeadToGoogleDoc(model, retQualified);

                if (ret)
                {
                    response.Data = new { sucesso = 1, message = "Formulario validado com sucesso!" };
                }
                else
                {
                    response.Data = new { sucesso = 0, message = "N�o foi poss�vel validar o formul�rio!" };
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
            //Service.GoogleServices googleService = new Service.GoogleServices();
            //googleService.ReadGoogleSheets();
        }
      
       
    }
}