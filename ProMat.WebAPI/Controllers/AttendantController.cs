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
        public void UpdateAttendantStructure()
        {
            //Service.GoogleServices googleService = new Service.GoogleServices();
            //googleService.ReadGoogleSheets();
        }
      
       
    }
}