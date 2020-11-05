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

        public bool CheckQualifieNoBornQuestionForm(QualifiedQueue form)
        {
            YouAre youAreObj = (YouAre)Enum.Parse(typeof(YouAre), form.Situation);
            HasYouWorked hasYouWorkObj = (HasYouWorked)Enum.Parse(typeof(HasYouWorked), form.PrevSituation);
            ReceivedWorkInsurances receivedWorkInsurancesObj = ReceivedWorkInsurances.Empty;

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
                if (receivedWorkInsurancesObj == ReceivedWorkInsurances.No)
                {
                    return true;
                }
                else if (receivedWorkInsurancesObj == ReceivedWorkInsurances.Yes)
                {
                    return false;
                }

            }

            return false;

        }
        public bool CheckQualifieBornQuestionForm(QualifiedQueue form)
        {
            YouAre youAreObj = (YouAre)Enum.Parse(typeof(YouAre), form.Situation);
            HasYouWorked hasYouWorkObj = HasYouWorked.Empty;
            ReceivedWorkInsurances receivedWorkInsurancesObj = ReceivedWorkInsurances.Empty;

            if (!form.SegJobReceive.Equals(""))
                receivedWorkInsurancesObj = (ReceivedWorkInsurances)Enum.Parse(typeof(ReceivedWorkInsurances), form.SegJobReceive);
            if (!form.PrevSituation.Equals(""))
                hasYouWorkObj = (HasYouWorked)Enum.Parse(typeof(HasYouWorked), form.PrevSituation);

            if (youAreObj == YouAre.MotherChildMoreFiveYears)
                return false;

            if (youAreObj == YouAre.MotherChildLessFiveYears  && (hasYouWorkObj == HasYouWorked.PayedFormByMyOn ||
             hasYouWorkObj == HasYouWorked.ImMEI))
            {
                return true;
               
            }
            if(youAreObj == YouAre.MotherChildLessFiveYears && hasYouWorkObj == HasYouWorked.WorkRegisteredBeforeBorn)
            {
                if (receivedWorkInsurancesObj == ReceivedWorkInsurances.No)
                {
                    return true;
                }
                else if (receivedWorkInsurancesObj == ReceivedWorkInsurances.Yes)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
