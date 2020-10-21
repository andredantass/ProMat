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

namespace ProMat.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "Current Legislators";
        static readonly string SpreadsheetId = "1Uhi0xsPvAVp44usDCFcVqXHQcd7ZE6PY-psFNuFP7fE";
        static readonly string sheet = "Sheet1";
        static SheetsService service;
        private const string ReadRange = "Sheet1!A:C";
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            GoogleCredential credential;
            using (var stream = new FileStream("google-credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }

            // Create Google Sheets API service.
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            ReadEntries();
            return new string[] { "value1,value2, value3" };
        }
        static void ReadEntries()
        {
            var range = $"Sheet1!A:B";
     
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(SpreadsheetId, range);
                var response = request.Execute();
                 IList<IList<object>> values = response.Values;
       Console.WriteLine(values);
           /*  SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(SpreadsheetId, range);

            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    // Print columns A to F, which correspond to indices 0 and 4.
                    Console.WriteLine("{0} | {1} | {2} ", row[0], row[1], row[2]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            } */
        }
    }
}