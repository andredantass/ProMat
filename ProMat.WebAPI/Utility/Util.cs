using ProMat.WebAPI.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Utility
{
    public static class Util
    {
        public static int GetReceivedWorkInsurancesItemForm(string SegJobReceive)
        {
            ReceivedWorkInsurances receivedWorkInsurancesObj = (ReceivedWorkInsurances)Enum.Parse(typeof(ReceivedWorkInsurances), SegJobReceive);

            switch (receivedWorkInsurancesObj)
            {
                case ReceivedWorkInsurances.Yes:
                    return 3499;
                case ReceivedWorkInsurances.No:
                    return 3501;
                default:
                    return 0;
            }
        }
        public static int GetPrevSituationItemForm(string PrevSituation)
        {
            HasYouWorked hasYouWorkObj = (HasYouWorked)Enum.Parse(typeof(HasYouWorked), PrevSituation);

            switch (hasYouWorkObj)
            {
                case HasYouWorked.WorkRegisteredBeforeBorn:
                    return 3493;
                case HasYouWorked.WasFired:
                    return 3493;
                case HasYouWorked.ImMEI:
                    return 3495;
                case HasYouWorked.PayedFormByMyOn:
                    return 3707;
                case HasYouWorked.NoWorkedRegisteredBeforeBorn:
                    return 3491;
                case HasYouWorked.NeverWorked:
                    return 3489;
                default:
                    return 0;
            }
        }
        public static int GetYouAreItemForm(string Situation)
        {
            YouAre youAreObj = (YouAre)Enum.Parse(typeof(YouAre), Situation);
            switch (youAreObj)
            {
                case YouAre.MotherChildLessFiveYears:
                        return 3469;
                case YouAre.MotherChildMoreFiveYears:
                        return 3517;
                case YouAre.PregnantFirstChild:
                        return 3471;
                case YouAre.PregnantChildLessFiveYears:
                        return 3473;
                case YouAre.PregnantChildMoreFiveYears:
                        return 3515;
                default:
                    return 0;
            }
        }

        public static int GetReceivedWorkInsurancesItemFormStart(string SegJobReceive)
        {
            ReceivedWorkInsurances receivedWorkInsurancesObj = (ReceivedWorkInsurances)Enum.Parse(typeof(ReceivedWorkInsurances), SegJobReceive);

            switch (receivedWorkInsurancesObj)
            {
                case ReceivedWorkInsurances.Yes:
                    return 101;
                case ReceivedWorkInsurances.No:
                    return 103;
                default:
                    return 0;
            }
        }
        public static string GetPrevSituationItemFormStart(string PrevSituation)
        {
            HasYouWorked hasYouWorkObj = (HasYouWorked)Enum.Parse(typeof(HasYouWorked), PrevSituation);
            return GetDescription<HasYouWorked>(hasYouWorkObj);
           
        }
        public static string GetYouAreItemFormStart(string Situation)
        {
            YouAre youAreObj = (YouAre)Enum.Parse(typeof(YouAre), Situation);
            return GetDescription<YouAre>(youAreObj);
           
        }
        public static void StoreLastCompany(string currentCompanyUser, string path)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                file.Write(currentCompanyUser);
            }
        }
        public static string ReadLastCompany(string url)
        {
           
            FileStream fileStream = new FileStream(url, FileMode.Open);
            string line = "";
            using (StreamReader reader = new StreamReader(fileStream))
            {
                line = reader.ReadLine();
            }
            return line;
        }
        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }
    }
}
