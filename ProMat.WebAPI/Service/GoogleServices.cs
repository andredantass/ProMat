using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using System.IO;
using Google.Apis.Services;
using ProMat.WebAPI.Model;
using Google.Apis.Sheets.v4.Data;
using ProMat.WebAPI.Utility;

namespace ProMat.WebAPI.Service
{
    public class GoogleServices
    {
        private static readonly GoogleServices _mySingletonGoogleServiceInstance = new GoogleServices();

        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "Current Legislators";
        static readonly string SpreadsheetIdCompany1 = "1Uhi0xsPvAVp44usDCFcVqXHQcd7ZE6PY-psFNuFP7fE";
        static readonly string SpreadsheetIdCompany2 = "1gEVGe_mP-YT7kfAmhQMrS1tPn6nK0zN6QXbwMh06SvI";
        static readonly string QualifiedSheet = "QualifiedQueue";
        static readonly string DisqualifiedSheet = "DisqualifiedQueue";

        private static int currentCompany;

        static SheetsService service;
        private const string ReadRange = "Sheet1!A:C";


        public static GoogleServices GetInstance() => _mySingletonGoogleServiceInstance;
        public GoogleServices()
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
        

        }

        public void InsertLeadEntryToGoogleDocs(QualifiedLead qualifiedUser, bool isQualified)
        {
            //string lastCompany = Util.ReadLastCompany();
            string lastCompany = "";
            if (lastCompany == "1")
            {
                if (isQualified)
                    InsertQualifiedEntryCompany2(qualifiedUser);
                else
                    InsertDisqualifiedEntryCompany2(qualifiedUser);
            }
            else
            {
                if (isQualified)
                    InsertQualifiedEntryCompany1(qualifiedUser);
                else
                    InsertDisqualifiedEntryCompany1(qualifiedUser);
            }
        }
        private void InsertQualifiedEntryCompany1(QualifiedLead qualifiedUser)
        {
            var range = $"{QualifiedSheet}!A:G";
            var valueRange = new ValueRange();

            var objectList = new List<object>() { qualifiedUser.FirstName,
                                                  qualifiedUser.Phone,
                                                  qualifiedUser.Situation,
                                                  Convert.ToDateTime(qualifiedUser.DateBorn).ToShortDateString(),
                                                  qualifiedUser.PrevSituation,
                                                  Convert.ToDateTime(qualifiedUser.DateJobEnd).ToShortDateString(),
                                                  qualifiedUser.SegJobReceive };

            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetIdCompany1, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendResponse = appendRequest.Execute();
            
            //Util.StoreLastCompany("1");
            
        }
        private void InsertDisqualifiedEntryCompany1(QualifiedLead qualifiedUser)
        {
            var range = $"{DisqualifiedSheet}!A:G";
            var valueRange = new ValueRange();

            var objectList = new List<object>() { qualifiedUser.FirstName,
                                                  qualifiedUser.Phone,
                                                  qualifiedUser.Situation,
                                                  Convert.ToDateTime(qualifiedUser.DateBorn).ToShortDateString(),
                                                  qualifiedUser.PrevSituation,
                                                  Convert.ToDateTime(qualifiedUser.DateJobEnd).ToShortDateString(),
                                                  qualifiedUser.SegJobReceive };


            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetIdCompany1, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendResponse = appendRequest.Execute();

            //Util.StoreLastCompany("1");
        }
        private void InsertQualifiedEntryCompany2(QualifiedLead qualifiedUser)
        {
            var range = $"{QualifiedSheet}!A:G";
            var valueRange = new ValueRange();

            var objectList = new List<object>() { qualifiedUser.FirstName,
                                                  qualifiedUser.Phone,
                                                  qualifiedUser.Situation,
                                                  Convert.ToDateTime(qualifiedUser.DateBorn).ToShortDateString(),
                                                  qualifiedUser.PrevSituation,
                                                  Convert.ToDateTime(qualifiedUser.DateJobEnd).ToShortDateString(),
                                                  qualifiedUser.SegJobReceive };


            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetIdCompany2, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendResponse = appendRequest.Execute();

            //Util.StoreLastCompany("2");
        }
        private void InsertDisqualifiedEntryCompany2(QualifiedLead qualifiedUser)
        {
            var range = $"{DisqualifiedSheet}!A:G";
            var valueRange = new ValueRange();

            var objectList = new List<object>() { qualifiedUser.FirstName,
                                                  qualifiedUser.Phone,
                                                  qualifiedUser.Situation,
                                                  Convert.ToDateTime(qualifiedUser.DateBorn).ToShortDateString(),
                                                  qualifiedUser.PrevSituation,
                                                  Convert.ToDateTime(qualifiedUser.DateJobEnd).ToShortDateString(),
                                                  qualifiedUser.SegJobReceive };


            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetIdCompany2, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendResponse = appendRequest.Execute();

            //Util.StoreLastCompany("2");
        }
        private void ReadDisqualifiedEntriesCompany1()
        {
            var range = $"DisqualifiedQueue!A:G";

            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(SpreadsheetIdCompany1, range);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            Console.WriteLine(values);
        }

        public void ReadQualifiedEntriesCompany1()
        {
            var range = $"QualifiedQueue!A:G";

            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(SpreadsheetIdCompany1, range);
            var response = request.Execute();
            var values = response.Values;
            if(values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    Console.WriteLine("{0} | {1} | {2} | {3}", row[0], row[1], row[2], row[3]);
                }
            }
            //IList<IList<object>> values = response.Values;

            //Console.WriteLine(values);
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

        public void ReadDisqualifiedEntriesCompany2()
        {
            var range = $"DisqualifiedQueue!A:G";

            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(SpreadsheetIdCompany2, range);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            Console.WriteLine(values);
        }

        public void ReadQualifiedEntriesCompany2()
        {
            var range = $"QualifiedQueue!A:G";

            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(SpreadsheetIdCompany2, range);
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
