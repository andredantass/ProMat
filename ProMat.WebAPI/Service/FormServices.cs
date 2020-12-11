using ProMat.WebAPI.Model;
using ProMat.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public class FormServices
    {
        int qualified = 0;
        Bitrix24Services btnService = new Bitrix24Services();



        public List<string> GetNextUserDepartmentLeadQualifield()
        {
            List<string> result = new List<string>();
            DepartmentServices departmentServices = new DepartmentServices();
            QualifiedLeadServices qualifiedLeadServices = new QualifiedLeadServices();
            AttendantServices attendantServices = new AttendantServices();

            List<Department> lstDepartments = departmentServices.GetDepartments();
            QualifiedLead lastQualifiedLeadInsert = qualifiedLeadServices.GetLastQualifiedLeadInserted();

            Department nextDepartment = null;

            if (lastQualifiedLeadInsert != null)
                nextDepartment = lstDepartments.SkipWhile(x => x.DepartmentId != lastQualifiedLeadInsert.DepartmentID).Skip(1).FirstOrDefault();

            if (nextDepartment == null)
                nextDepartment = lstDepartments[0];

            List<Attendant> lstAttendants = attendantServices.GetAttendantsByCompanyId(nextDepartment.DepartmentId);

            QualifiedLead lastQualifiedLeadInsertAttendant = qualifiedLeadServices.GetLastQualifiedLeadInsertedAttendant(nextDepartment.DepartmentId);

            if (lastQualifiedLeadInsertAttendant == null)
            {
                result.Add(nextDepartment.DepartmentId.ToString());
                result.Add(lstAttendants[0].AttendantId.ToString());
                if (nextDepartment.WebHook != null)
                    result.Add(nextDepartment.WebHook.WebhookPath);
            }
            else
            {
                var nextAttendant = lstAttendants.SkipWhile(x => x.AttendantId != lastQualifiedLeadInsertAttendant.AttendantID).Skip(1).FirstOrDefault();

                if (nextAttendant == null)
                    nextAttendant = lstAttendants[0];

                result.Add(nextDepartment.DepartmentId.ToString());
                result.Add(nextAttendant.AttendantId.ToString());
                if (nextDepartment.WebHook != null)
                    result.Add(nextDepartment.WebHook.WebhookPath);
            }

            return result;
        }
        public List<string> GetNextUserDepartmentLeadDisQualifield()
        {
            List<string> result = new List<string>();
            DepartmentServices departmentServices = new DepartmentServices();
            DisQualifiedLeadServices disQualifiedLeadServices = new DisQualifiedLeadServices();
            AttendantServices attendantServices = new AttendantServices();

            List<Department> lstDepartments = departmentServices.GetDepartments();
            DisqualifiedLead lastDisQualifiedLeadInsert = disQualifiedLeadServices.GetLastDisQualifiedLeadInserted();

            Department nextDepartment = null;

            if (lastDisQualifiedLeadInsert != null)
                nextDepartment = lstDepartments.SkipWhile(x => x.DepartmentId != lastDisQualifiedLeadInsert.DepartmentID).Skip(1).FirstOrDefault();

            if (nextDepartment == null)
                nextDepartment = lstDepartments[0];

            List<Attendant> lstAttendants = attendantServices.GetAttendantsByCompanyId(nextDepartment.DepartmentId);

            DisqualifiedLead lastDisQualifiedLeadInsertAttendant = disQualifiedLeadServices.GetLastDisQualifiedLeadInsertedAttendant(nextDepartment.DepartmentId);

            if (lastDisQualifiedLeadInsertAttendant == null)
            {
                result.Add(nextDepartment.DepartmentId.ToString());
                result.Add(lstAttendants[0].AttendantId.ToString());
                if (nextDepartment.WebHook != null)
                    result.Add(nextDepartment.WebHook.WebhookPath);
            }
            else
            {
                var nextAttendant = lstAttendants.SkipWhile(x => x.AttendantId != lastDisQualifiedLeadInsertAttendant.AttendantID).Skip(1).FirstOrDefault();

                if (nextAttendant == null)
                    nextAttendant = lstAttendants[0];

                result.Add(nextDepartment.DepartmentId.ToString());
                result.Add(nextAttendant.AttendantId.ToString());
                if (nextDepartment.WebHook != null)
                    result.Add(nextDepartment.WebHook.WebhookPath);
            }

            return result;
        }

        public bool CheckQualifieNoBornQuestionForm(QualifiedLead form)
        {
            YouAre youAreObj = (YouAre)Enum.Parse(typeof(YouAre), form.Situation);
            HasYouWorked hasYouWorkObj = (HasYouWorked)Enum.Parse(typeof(HasYouWorked), form.PrevSituation);
            ReceivedWorkInsurances receivedWorkInsurancesObj = ReceivedWorkInsurances.Empty;

            if (form.SegJobReceive != null)
                if (!form.SegJobReceive.Equals(""))
                    receivedWorkInsurancesObj = (ReceivedWorkInsurances)Enum.Parse(typeof(ReceivedWorkInsurances), form.SegJobReceive);

            // Check Situation condition
            if ((youAreObj == YouAre.PregnantFirstChild ||
                youAreObj == YouAre.PregnantChildLessFiveYears ||
                youAreObj == YouAre.PregnantChildMoreFiveYears) && (hasYouWorkObj == HasYouWorked.WorkRegisteredBeforeBorn ||
                   hasYouWorkObj == HasYouWorked.PayedFormByMyOn ||
                   hasYouWorkObj == HasYouWorked.ImMEI))
            {
                return true;
            }
            else if ((youAreObj == YouAre.PregnantFirstChild ||
                youAreObj == YouAre.PregnantChildLessFiveYears ||
                youAreObj == YouAre.PregnantChildMoreFiveYears) && hasYouWorkObj == HasYouWorked.WasFired)
            {
                if (receivedWorkInsurancesObj == ReceivedWorkInsurances.No || receivedWorkInsurancesObj == ReceivedWorkInsurances.Yes)
                {
                    return true;
                }

            }

            return false;

        }
        public bool CheckQualifieBornQuestionForm(QualifiedLead form)
        {
            YouAre youAreObj = (YouAre)Enum.Parse(typeof(YouAre), form.Situation);
            HasYouWorked hasYouWorkObj = HasYouWorked.Empty;
            ReceivedWorkInsurances receivedWorkInsurancesObj = ReceivedWorkInsurances.Empty;

            if (form.SegJobReceive != null)
                if (!form.SegJobReceive.Equals(""))
                    receivedWorkInsurancesObj = (ReceivedWorkInsurances)Enum.Parse(typeof(ReceivedWorkInsurances), form.SegJobReceive);

            if (form.PrevSituation != null)
                if (!form.PrevSituation.Equals(""))
                    hasYouWorkObj = (HasYouWorked)Enum.Parse(typeof(HasYouWorked), form.PrevSituation);

            if (youAreObj == YouAre.MotherChildMoreFiveYears)
                return false;

            if (youAreObj == YouAre.MotherChildLessFiveYears && (hasYouWorkObj == HasYouWorked.PayedFormByMyOn ||
             hasYouWorkObj == HasYouWorked.ImMEI))
            {
                return true;

            }
            if (youAreObj == YouAre.MotherChildLessFiveYears && hasYouWorkObj == HasYouWorked.WorkRegisteredBeforeBorn)
            {
                if (receivedWorkInsurancesObj == ReceivedWorkInsurances.No || receivedWorkInsurancesObj == ReceivedWorkInsurances.Yes)
                {
                    return true;
                }

            }

            return false;
        }

        public bool InsertLeadToGoogleDoc(QualifiedLead form, bool isQualified)
        {
            GoogleServices googleService = GoogleServices.GetInstance();
            googleService.InsertLeadEntryToGoogleDocs(form, isQualified);
            return true;
        }
        public bool InsertQualifiedLeadtoBitrixQueue(QualifiedLead form, bool isQualified, string status)
        {
            Bitrix24Services objService = new Bitrix24Services();
            try
            {
                List<string> nextDepartmentAttendantQueue = GetNextUserDepartmentLeadQualifield();
                form.InsertDate = DateTime.Now;
                form.AttendantID = int.Parse(nextDepartmentAttendantQueue[1]);
                form.DepartmentID = int.Parse(nextDepartmentAttendantQueue[0]);
                string webHookPath = nextDepartmentAttendantQueue[2];

                objService.CreateQualifiedLead(form, webHookPath, "lead@hotmail.com", status, false);

                return true;
            }
            catch
            {
                return false;
            }
           
        }
        public bool InsertDisQualifiedLeadtoBitrixQueue(QualifiedLead form, bool isQualified, string status)
        {
            Bitrix24Services objService = new Bitrix24Services();
            try
            {
                DisqualifiedLead disQualifiedLead = new DisqualifiedLead();
                List<string> nextDepartmentAttendantQueue = GetNextUserDepartmentLeadDisQualifield();
                string webHookPath = nextDepartmentAttendantQueue[2];

                disQualifiedLead.AttendantID = int.Parse(nextDepartmentAttendantQueue[1]);
                disQualifiedLead.DateBorn = form.DateBorn;
                disQualifiedLead.DateJobEnd = form.DateJobEnd;
                disQualifiedLead.DepartmentID = int.Parse(nextDepartmentAttendantQueue[0]);
                disQualifiedLead.FirstName = form.FirstName;
                disQualifiedLead.Phone = form.Phone;
                disQualifiedLead.PrevSituation = form.PrevSituation;
                disQualifiedLead.SegJobReceive = form.SegJobReceive;
                disQualifiedLead.Situation = form.Situation;
                disQualifiedLead.InsertDate = DateTime.Now;

                objService.CreateDisQualifiedLead(disQualifiedLead, webHookPath, "lead@hotmail.com", status, false);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
