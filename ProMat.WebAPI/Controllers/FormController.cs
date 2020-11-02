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
        [Route("ValidateForm")]
        public ResponseDTO ValidateForm([FromBody] QualifiedQueue model)
        {
            var response = new ResponseDTO();
            response.Data = new { sucesso = 1, message = "Formulario validado com sucesso!" };
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