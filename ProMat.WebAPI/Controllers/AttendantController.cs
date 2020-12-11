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
    public class AttendantController : ControllerBase
    {
       

        [HttpGet]
        [Route("UpdateAttendantStructure")]
        public ResponseDTO UpdateAttendantStructure()
        {
            AttendantServices attendantService = new AttendantServices();
            var response = new ResponseDTO();

            try
            {
                var ret = attendantService.SincronizeAttendantWithBitrix();

                if (ret == 1)
                {
                    response.Data = new { sucesso = 1, message = "Atendentes atualizados com sucesso!" };
                }
                else
                {
                    response.Data = new { sucesso = 0, message = "N�o foi poss�vel atualizar os departamentos!" };
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
      
       
    }
}