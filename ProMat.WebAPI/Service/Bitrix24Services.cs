using Newtonsoft.Json.Linq;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.Service.Entity;
using ProMat.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public class Bitrix24Services
    {
        public void CreateDisQualifiedLead(DisqualifiedLead disqualifiedLead, string webHookPath, string email, string status)
        {
            DisQualifiedLeadServices disQualifiedLeadService = new DisQualifiedLeadServices();

            try
            {
                //string accessToken = GetNewAccessToken();
                //string portal_name = "https://luxassessoria.bitrix24.com.br/rest/6432/izu90pf0b1l3gjpr/";

                string url = string.Format("{0}crm.lead.add.json", webHookPath);
                string queue = status;

                //HasYouWorked hasYouWorkObj = HasYouWorked.Empty;
                //ReceivedWorkInsurances receivedWorkInsurancesObj = ReceivedWorkInsurances.Empty;

                //if (disqualifiedLead.SegJobReceive != null)
                //    if (!disqualifiedLead.SegJobReceive.Equals(""))
                //        receivedWorkInsurancesObj = (ReceivedWorkInsurances)Enum.Parse(typeof(ReceivedWorkInsurances), form.SegJobReceive);

                //if (disqualifiedLead.PrevSituation != null)
                //    if (!disqualifiedLead.PrevSituation.Equals(""))
                //        hasYouWorkObj = (HasYouWorked)Enum.Parse(typeof(HasYouWorked), disqualifiedLead.PrevSituation);

                //if (status == "DPP")
                //    queue = "IN_PROCESS";
                //else if (status == "135")
                //    queue = "1";
                //else if (status == "PLUSS")
                //    queue = "PROCESSED";
                //else
                //    queue = "NEW";

                int PrevSituation = 0;
                int SegJobReceive = 0;

                string comments = disqualifiedLead.Situation;

                int Situation = Utility.Util.GetYouAreItemForm(disqualifiedLead.Situation);
                if (disqualifiedLead.PrevSituation != null && disqualifiedLead.PrevSituation != "")
                {
                    PrevSituation = Utility.Util.GetPrevSituationItemForm(disqualifiedLead.PrevSituation);
                    comments += "/" + disqualifiedLead.PrevSituation;
                }
                if (disqualifiedLead.SegJobReceive != null && disqualifiedLead.SegJobReceive != "")
                {
                    SegJobReceive = Utility.Util.GetReceivedWorkInsurancesItemForm(disqualifiedLead.SegJobReceive);
                    comments += "/" + disqualifiedLead.SegJobReceive;
                }
                if(disqualifiedLead.DateJobEnd != null && disqualifiedLead.DateJobEnd != "")
                {
                    comments += "/Job End Date =" + disqualifiedLead.DateJobEnd;
                }
                if (disqualifiedLead.DateBorn != null && disqualifiedLead.DateBorn != "")
                {
                    comments += "/Date Born =" + disqualifiedLead.DateBorn;
                }

                queue = "8";

                var data = new
                {
                    fields = new
                    {
                        TITLE = disqualifiedLead.FirstName,
                        NAME = disqualifiedLead.FirstName,
                        STATUS_ID = queue,
                        OPENED = "Y",
                        OPPORTUNITY = 12,
                        ASSIGNED_BY_ID = disqualifiedLead.AttendantID,
                        COMPANY_TITLE = disqualifiedLead.FirstName,
                        SOURCE_ID = "1",
                        UF_CRM_1597496714 = Situation,
                        UF_CRM_1597497342 = PrevSituation,
                        UF_CRM_1597497546 = SegJobReceive,
                        UF_CRM_1597497700 = disqualifiedLead.DateJobEnd != null ? disqualifiedLead.DateJobEnd : "",
                        UF_CRM_1580316545358 = disqualifiedLead.DateBorn != null ? disqualifiedLead.DateBorn : "",
                        COMMENTS = webHookPath.Contains("startprev") ? comments : "(NQ)",
                        //UF_CRM_1577711078940 = 4293,
                        //UF_CRM_1605798367693 = "TESTE2",
                        //UF_CRM_1605803452106 = "64",
                        //UF_CRM_1605728591038 = "44",
                        //UF_CRM_1605803452106 = new List<UF_CRM_1605728591038>() { new UF_CRM_1605728591038() { ID = "62",  VALUE = "Sim" } }.ToArray(),
                        PHONE = new List<Phone>() { new Phone() { VALUE_TYPE = "WORK", TYPE_ID = "PHONE", VALUE = disqualifiedLead.Phone } }.ToArray()
                        //EMAIL = new List<Email>() { new Email() { VALUE_TYPE = "WORK", TYPE_ID = "EMAIL", VALUE = email } }.ToArray()
                    },
                    @params = new
                    {
                        REGISTER_SONET_EVENT = "Y"
                    }
                };

                //BitrixLead lead = new BitrixLead();

                //lead.TITLE = title;
                //lead.CURRENCY_ID = "RUB";
                //lead.STATUS_ID = "NEW";
                //lead.OPENED = "Y";
                //lead.OPPORTUNITY = opportunity.ToString();

                //if (!string.IsNullOrEmpty(contactName))
                //    lead.COMPANY_TITLE = contactName;

                //if (!string.IsNullOrEmpty(PhoneNumber))
                //    lead.PHONE = new List<Phone>() { new Phone() { VALUE_TYPE = "WORK", TYPE_ID = "PHONE", VALUE = PhoneNumber } }.ToArray();

                //if (!string.IsNullOrEmpty(email))
                //    lead.EMAIL = new List<Email>() { new Email() { VALUE_TYPE = "WORK", TYPE_ID = "EMAIL", VALUE = email } }.ToArray();


                PostToAPI(url, "", data);
                disQualifiedLeadService.InsertDisQualifiedLead(disqualifiedLead);
            }
            catch (Exception exc)
            {
            }
        }
        public void CreateQualifiedLead(QualifiedLead qualifiedLead, string webHookPath, string email, string status)
        {
            FormServices formService = new FormServices();
            QualifiedLeadServices qualifiedLeadService = new QualifiedLeadServices();

            try
            {
                //string accessToken = GetNewAccessToken();
                //string portal_name = "https://luxassessoria.bitrix24.com.br/rest/6432/izu90pf0b1l3gjpr/";

                string url = string.Format("{0}crm.lead.add.json", webHookPath);
                string queue = status;

                //if (status == "DPP")
                //    queue = "IN_PROCESS";
                //else if (status == "135")
                //    queue = "1";
                //else if (status == "PLUSS")
                //    queue = "PROCESSED";
                //else
                //    queue = "NEW";

                queue = "8";

                int PrevSituation = 0;
                int SegJobReceive = 0;

                int Situation = Utility.Util.GetYouAreItemForm(qualifiedLead.Situation);
                string comments = qualifiedLead.Situation;

                if (qualifiedLead.PrevSituation != null && qualifiedLead.PrevSituation != "")
                {
                    PrevSituation = Utility.Util.GetPrevSituationItemForm(qualifiedLead.PrevSituation);
                    comments += "/" + qualifiedLead.PrevSituation;
                }
                if (qualifiedLead.SegJobReceive != null && qualifiedLead.SegJobReceive != "")
                {
                    SegJobReceive = Utility.Util.GetReceivedWorkInsurancesItemForm(qualifiedLead.SegJobReceive);
                    comments += "/" + qualifiedLead.SegJobReceive;
                }
                if (qualifiedLead.DateJobEnd != null && qualifiedLead.DateJobEnd != "")
                {
                    comments += "/Job End Date =" + qualifiedLead.DateJobEnd;
                }
                if (qualifiedLead.DateBorn != null && qualifiedLead.DateBorn != "")
                {
                    comments += "/Date Born =" + qualifiedLead.DateBorn;
                }


                var data = new
                {
                    fields = new
                    {
                        TITLE = qualifiedLead.FirstName ,
                        NAME = qualifiedLead.FirstName,
                        STATUS_ID = queue,
                        OPENED = "Y",
                        ASSIGNED_BY_ID = qualifiedLead.AttendantID,
                        COMPANY_TITLE = qualifiedLead.FirstName,
                        SOURCE_ID = "1",
                        UF_CRM_1597496714 = Situation,
                        UF_CRM_1597497342 = PrevSituation,
                        UF_CRM_1597497546 = SegJobReceive,
                        UF_CRM_1597497700 = qualifiedLead.DateJobEnd != null ? qualifiedLead.DateJobEnd : "",
                        UF_CRM_1580316545358 = qualifiedLead.DateBorn != null ? qualifiedLead.DateBorn : "",
                        COMMENTS = webHookPath.Contains("startprev") ? comments : "(Q)",
                        //COMPANY_TITLE = contactName,
                        //UF_CRM_1605728591038 = new List<UF_CRM_1605728591038>() { new UF_CRM_1605728591038() { ID = "44", VALUE = "TRABALHEI REGISTRADA antes do nascer." } }.ToArray(),
                        PHONE = new List<Phone>() { new Phone() { VALUE_TYPE = "WORK", TYPE_ID = "PHONE", VALUE = qualifiedLead.Phone } }.ToArray()
                        //EMAIL = new List<Email>() { new Email() { VALUE_TYPE = "WORK", TYPE_ID = "EMAIL", VALUE = email } }.ToArray()
                    },
                    @params = new
                    {
                        REGISTER_SONET_EVENT = "Y"
                    }
                };

                //BitrixLead lead = new BitrixLead();

                //lead.TITLE = title;
                //lead.CURRENCY_ID = "RUB";
                //lead.STATUS_ID = "NEW";
                //lead.OPENED = "Y";
                //lead.OPPORTUNITY = opportunity.ToString();

                //if (!string.IsNullOrEmpty(contactName))
                //    lead.COMPANY_TITLE = contactName;

                //if (!string.IsNullOrEmpty(PhoneNumber))
                //    lead.PHONE = new List<Phone>() { new Phone() { VALUE_TYPE = "WORK", TYPE_ID = "PHONE", VALUE = PhoneNumber } }.ToArray();

                //if (!string.IsNullOrEmpty(email))
                //    lead.EMAIL = new List<Email>() { new Email() { VALUE_TYPE = "WORK", TYPE_ID = "EMAIL", VALUE = email } }.ToArray();


                PostToAPI(url, "", data);
                qualifiedLeadService.InsertQualifiedLead(qualifiedLead);
            }
            catch (Exception exc)
            {
            }
        }

        public IList<Department> GetDepartments(string urlWebhook)
        {
            //string response = GetToAPI("https://b24-epzio5.bitrix24.com.br/rest/1/irb5zfozpvpbt5ty/department.get.json");
            urlWebhook = string.Format("{0}{1}", urlWebhook, "department.get.json");
            string response = GetToAPI(urlWebhook);
            var parsed = JObject.Parse(response);
            int rows = parsed["result"].Count();
            List<Department> lstDepartment = new List<Department>();

            for (int i = 0; i < rows; i++)
            {
                Department department = new Department();

                department.DepartmentId = parsed.SelectToken("result[" + i + "].ID").Value<int>();
                department.Name = parsed.SelectToken("result[" + i + "].NAME").Value<string>();
                if (department.Name.Contains("engine"))
                    lstDepartment.Add(department);

            }
            return lstDepartment;
        }
        public IList<Attendant> GetEmployeeDepartments(string urlWebhook, int departmentID)
        {

            //string response = GetToAPI("https://luxassessoria.bitrix24.com.br/rest/6432/izu90pf0b1l3gjpr/user.get.json?UF_DEPARTMENT=" + departmentID.ToString());
            urlWebhook = string.Format("{0}{1}{2}", urlWebhook, "user.get.json?UF_DEPARTMENT=", departmentID.ToString());
            string response = GetToAPI(urlWebhook);
            var parsed = JObject.Parse(response);
            int rows = parsed["result"].Count();
            List<Attendant> lstAttendat = new List<Attendant>();

            for (int i = 0; i < rows; i++)
            {
                Attendant user = new Attendant();
                user.AttendantId = parsed.SelectToken("result[" + i + "].ID").Value<int>();
                user.Name = parsed.SelectToken("result[" + i + "].NAME").Value<string>();
                user.LastName = parsed.SelectToken("result[" + i + "].LAST_NAME").Value<string>();
                user.Email = parsed.SelectToken("result[" + i + "].EMAIL").Value<string>();
                user.Active = parsed.SelectToken("result[" + i + "].ACTIVE").Value<bool>();

                int rowsDep = parsed.SelectToken("result[" + i + "].UF_DEPARTMENT").Count();
                //user.UF_DEPARTMENT = new List<int>();
                user.DepartmentId = departmentID;

                lstAttendat.Add(user);


                //for (int j = 0; j < rowsDep; j++)
                //{

                //    //user.DepartmentId = parsed.SelectToken("result[" + i + "].UF_DEPARTMENT[" + j + "]").Value<int>();

                //}
            }

            return lstAttendat;

        }
        private string GetToAPI(string url)
        {

            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            string responseFromServer = "";


            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
            }

            response.Close();
            return responseFromServer;

        }
        private void PostToAPI(string url, string token, object data)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            var http = (HttpWebRequest)WebRequest.Create(new Uri(url));
            http.Accept = "application/json; charset=utf-8";
            http.ContentType = "application/json; charset=utf-8";
            http.Method = "POST";
            http.Headers.Add("Authorization", "Bearer " + token);

            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(json);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();
        }
    }
}
