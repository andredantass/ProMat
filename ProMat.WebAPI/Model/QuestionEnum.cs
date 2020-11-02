using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Model
{
    public enum YouAre
    {
        [Description("Gestante do PRIMEIRO(A) FILHO(A).")]
        PregnantFirstChild,
        [Description("Gestante e filho(a) MENOR de 5 anos.")]
        PregnantChildLessFiveYears,
        [Description("Gestante e filho(a) com MAIS de 5 anos.")]
        PregnantChildMoreFiveYears,
        [Description("Mãe de filho(a) MENOR de 5 anos.")]
        MotherChildLessFiveYears,
        [Description("Mãe de filho(a) com MAIS de 5 anos.")]
        MotherChildMoreFiveYears

    }
    public enum HasYouWorked
    {
        [Description("TRABALHEI REGISTRADA antes de nascer.")]
        WorkRegisteredBeforeBorn,
        [Description("Sou MEI (Micro Empreendedor Individual).")]
        ImMEI,
        [Description("Paguei a previdência por conta (carnê GPS).")]
        PayedFormByMyOn,
        [Description("NÃO trabalhei registrada antes de nascer.")]
        NoWorkedRegisteredBeforeBorn,
        [Description("Nunca trabalhei registrada.")]
        NeverWorked

    }
    public enum ReceivedWorkInsurances
    {
        [Description("SIM, RECEBI seguro desemprego.")]
        Yes,
        [Description("NÃO recebi seguro desemprego.")]
        No
    }
    public class QuestionEnum
    {
    }
}
