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
    public class LeadController : ControllerBase
    {


        [HttpGet]
        [Route("ReprocessQualifiedLeadsContingency")]
        public ResponseDTO ReprocessQualifiedLeadsContingency()
        {
            QualifiedLeadServices service = new QualifiedLeadServices();
            var response = new ResponseDTO();

            try
            {
                var ret = service.ReprocessQualifiedLeadsContingency();

                if (ret == 1)
                {
                    response.Data = new { sucesso = 1, message = "Todos os leads foram reprocessados com sucesso!" };
                }
                else
                {
                    response.Data = new { sucesso = 0, message = "Não foi possível reprocessars os leads qualificados!" };
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet]
        [Route("ReprocessDisQualifiedLeadsContingency")]
        public ResponseDTO ReprocessDisQualifiedLeadsContingency()
        {
            DisQualifiedLeadServices service = new DisQualifiedLeadServices();
            var response = new ResponseDTO();

            try
            {
                var ret = service.ReprocessDisQualifiedLeadsContingency();

                if (ret == 1)
                {
                    response.Data = new { sucesso = 1, message = "Todos os leads foram reprocessados com sucesso!" };
                }
                else
                {
                    response.Data = new { sucesso = 0, message = "Não foi possível reprocessars os leads qualificados!" };
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet]
        [Route("RemoveQualifiedLeads")]
        public ResponseDTO RemoveQualifiedLeads()
        {
            QualifiedLeadServices service = new QualifiedLeadServices();
            var response = new ResponseDTO();

            try
            {
                var ret = service.DeleteAll();

                if (ret == 1)
                {
                    response.Data = new { sucesso = 1, message = "Todos os leads qualificados foram deletados com sucesso!" };
                }
                else
                {
                    response.Data = new { sucesso = 0, message = "Não foi possível deletar os leads qualificados!" };
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        [HttpGet]
        [Route("GetLeadList")]
        public ResponseDTO GetLeadList()
        {
            LeadServices leadServices = new LeadServices();
            var response = new ResponseDTO();
            List<Lead> leads = leadServices.LeadList();
            try
            {
                response.Data = new { leadList = leads };
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        [HttpPost]
        [Route("ChangeStatusId")]
        public void ChangeStatusId([FromBody] List<int> ChangeList, int StatusId)
        {
            try
            {
                int teste = ChangeList.Count();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("RemoveDisQualifiedLeads")]
        public ResponseDTO RemoveDisQualifiedLeads()
        {
            DisQualifiedLeadServices service = new DisQualifiedLeadServices();
            var response = new ResponseDTO();

            try
            {
                var ret = service.DeleteAll();

                if (ret == 1)
                {
                    response.Data = new { sucesso = 1, message = "Todos os leads desqualificados foram deletados com sucesso!" };
                }
                else
                {
                    response.Data = new { sucesso = 0, message = "Não foi possível deletar os leads desqualificados!" };
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