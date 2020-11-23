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
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        [Route("UpdateDepartmentStructure")]
        public ResponseDTO UpdateDepartmentStructure()
        {
            DepartmentServices departmentService = new DepartmentServices();
            var response = new ResponseDTO();

            try
            {
                var ret = departmentService.SincronizeDepartmentWithBitrix();
               
                if (ret == 1)
                {
                    response.Data = new { sucesso = 1, message = "Departamentos atualizados com sucesso!" };
                }
                else
                {
                    response.Data = new { sucesso = 0, message = "Não foi possível atualizar os departamentos!" };
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