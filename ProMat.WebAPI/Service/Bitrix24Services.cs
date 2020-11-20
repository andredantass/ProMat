using Newtonsoft.Json.Linq;
using ProMat.WebAPI.Model;
using ProMat.WebAPI.Service.Entity;
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
        public void CreateDisQualifiedLead(DisqualifiedLead disqualifiedLead, string title, decimal opportunity, string contactName, string PhoneNumber, string email, string status)
        {
            DisQualifiedLeadServices disQualifiedLeadService = new DisQualifiedLeadServices();

            try
            {
                //string accessToken = GetNewAccessToken();
                string portal_name = "https://luxassessoria.bitrix24.com.br/rest/6432/izu90pf0b1l3gjpr/";

                string url = string.Format("{0}crm.lead.add.json", portal_name);
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

                var data = new
                {
                    fields = new
                    {
                        TITLE = contactName + " (NQ)",
                        CURRENCY_ID = "RUB",
                        STATUS_ID = queue,
                        OPENED = "Y",
                        OPPORTUNITY = opportunity,
                        ASSIGNED_BY_ID = 8,
                        COMPANY_TITLE = contactName,
                        SOURCE_ID = "1",
                        //UF_CRM_1577711078940 = 4293,
                        //UF_CRM_1605798367693 = "TESTE2",
                        //UF_CRM_1605803452106 = "64",
                        //UF_CRM_1605728591038 = "44",
                        //UF_CRM_1605803452106 = new List<UF_CRM_1605728591038>() { new UF_CRM_1605728591038() { ID = "62",  VALUE = "Sim" } }.ToArray(),
                        PHONE = new List<Phone>() { new Phone() { VALUE_TYPE = "WORK", TYPE_ID = "PHONE", VALUE = PhoneNumber } }.ToArray(),
                        EMAIL = new List<Email>() { new Email() { VALUE_TYPE = "WORK", TYPE_ID = "EMAIL", VALUE = email } }.ToArray()
                    },
                    @params = new
                    {
                        REGISTER_SONET_EVENT = "Y"
                    }
                };

                BitrixLead lead = new BitrixLead();

                lead.TITLE = title;
                lead.CURRENCY_ID = "RUB";
                lead.STATUS_ID = "NEW";
                lead.OPENED = "Y";
                lead.OPPORTUNITY = opportunity.ToString();

                if (!string.IsNullOrEmpty(contactName))
                    lead.COMPANY_TITLE = contactName;

                if (!string.IsNullOrEmpty(PhoneNumber))
                    lead.PHONE = new List<Phone>() { new Phone() { VALUE_TYPE = "WORK", TYPE_ID = "PHONE", VALUE = PhoneNumber } }.ToArray();

                if (!string.IsNullOrEmpty(email))
                    lead.EMAIL = new List<Email>() { new Email() { VALUE_TYPE = "WORK", TYPE_ID = "EMAIL", VALUE = email } }.ToArray();


                PostToAPI(url, "", data);
                disQualifiedLeadService.InsertDisQualifiedLead(disqualifiedLead);
            }
            catch (Exception exc)
            {
            }
        }
        public void CreateQualifiedLead(QualifiedLead qualifiedLead, string title, decimal opportunity, string contactName, string PhoneNumber, string email, string status)
        {
            FormServices formService = new FormServices();
            QualifiedLeadServices qualifiedLeadService = new QualifiedLeadServices();

            try
            {
                //string accessToken = GetNewAccessToken();
                string portal_name = "https://luxassessoria.bitrix24.com.br/rest/6432/izu90pf0b1l3gjpr/";
                
                string url = string.Format("{0}crm.lead.add.json", portal_name);
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
             

                var data = new
                    {
                        fields = new
                        {
                            TITLE = contactName + " (Q)",
                            CURRENCY_ID = "RUB",
                            STATUS_ID = queue,
                            OPENED = "Y",
                            SOURCE_ID = "1",
                            //UF_CRM_1577711078940 = 4293,
                            //OPPORTUNITY = opportunity,
                            //ASSIGNED_BY_ID = qualifiedLead.AttendantID,
                            //COMPANY_TITLE = contactName,
                            UF_CRM_1605728591038 = new List<UF_CRM_1605728591038>() { new UF_CRM_1605728591038() { ID = "44", VALUE = "TRABALHEI REGISTRADA antes do nascer." } }.ToArray(),
                            PHONE = new List<Phone>() { new Phone() { VALUE_TYPE = "WORK", TYPE_ID = "PHONE", VALUE = PhoneNumber } }.ToArray(),
                            EMAIL = new List<Email>() { new Email() { VALUE_TYPE = "WORK", TYPE_ID = "EMAIL", VALUE = email } }.ToArray()
                        },
                        @params = new
                        {
                            REGISTER_SONET_EVENT = "Y",
                            UF_CRM_1605728591038 = "TRABALHEI REGISTRADA antes do nascer.",
                            UF_CRM_1605796275607 = "TESTE1"
                        }
                    };

                BitrixLead lead = new BitrixLead();

                lead.TITLE = title;
                lead.CURRENCY_ID = "RUB";
                lead.STATUS_ID = "NEW";
                lead.OPENED = "Y";
                lead.OPPORTUNITY = opportunity.ToString();

                if (!string.IsNullOrEmpty(contactName))
                    lead.COMPANY_TITLE = contactName;

                if (!string.IsNullOrEmpty(PhoneNumber))
                    lead.PHONE = new List<Phone>() { new Phone() { VALUE_TYPE = "WORK", TYPE_ID = "PHONE", VALUE = PhoneNumber } }.ToArray();

                if (!string.IsNullOrEmpty(email))
                    lead.EMAIL = new List<Email>() { new Email() { VALUE_TYPE = "WORK", TYPE_ID = "EMAIL", VALUE = email } }.ToArray();


                PostToAPI(url, "", data);
                qualifiedLeadService.InsertQualifiedLead(qualifiedLead);
            }
            catch (Exception exc)
            {
            }
        }

        public void GetDepartments()
        {
            string response = GetToAPI("https://b24-epzio5.bitrix24.com.br/rest/1/irb5zfozpvpbt5ty/department.get.json");
            var parsed = JObject.Parse(response);
            int rows = parsed["result"].Count();
            List<Department> lstUser = new List<Department>();
            DepartmentServices departmentService = new DepartmentServices();

            for (int i = 0; i < rows; i++)
            {
                Department department = new Department();
                department.DepartmentId = parsed.SelectToken("result[" + i + "].ID").Value<int>();
                department.Name = parsed.SelectToken("result[" + i + "].NAME").Value<string>();

                departmentService.InsertDepartment(department);
            }
        }
        public void GetEmployeeDepartments()
        {
            string department = "95.93.91.103.97.99";
            string[] lstDepartment = department.Split(".");

            foreach (string item in lstDepartment)
            {
                string response = GetToAPI("https://luxassessoria.bitrix24.com.br/rest/6432/izu90pf0b1l3gjpr/user.get.json?UF_DEPARTMENT=" + item);
                var parsed = JObject.Parse(response);
                int rows = parsed["result"].Count();
                List<Attendant> lstUser = new List<Attendant>();
                AttendantServices attendantService = new AttendantServices();

                for (int i = 0; i < rows; i++)
                {
                    Attendant user = new Attendant();
                    user.AttendantId = parsed.SelectToken("result[" + i + "].ID").Value<string>();
                    user.Name = parsed.SelectToken("result[" + i + "].NAME").Value<string>();
                    user.LastName = parsed.SelectToken("result[" + i + "].LAST_NAME").Value<string>();
                    user.Email = parsed.SelectToken("result[" + i + "].EMAIL").Value<string>();
                    user.Active = parsed.SelectToken("result[" + i + "].ACTIVE").Value<bool>();

                    int rowsDep = parsed.SelectToken("result[" + i + "].UF_DEPARTMENT").Count();
                    //user.UF_DEPARTMENT = new List<int>();
                    user.DepartmentId = int.Parse(item);
                    attendantService.InsertAttendant(user);

                    //for (int j = 0; j < rowsDep; j++)
                    //{

                    //    //user.DepartmentId = parsed.SelectToken("result[" + i + "].UF_DEPARTMENT[" + j + "]").Value<int>();
                       
                    //}
                }
            }
                
           
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
