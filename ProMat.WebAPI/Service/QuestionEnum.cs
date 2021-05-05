using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Service
{
    public enum YouAre
    {
        [Description("Primeira gravidez")]
        PregnantFirstChild,
        [Description("Grávida filho menor de 5")]
        PregnantChildLessFiveYears,
        [Description("Gestante e filho(a) com MAIS de 5 anos.")]
        PregnantChildMoreFiveYears,
        [Description("Mãe de filho menor de 5 Anos")]
        MotherChildLessFiveYears,
        [Description("Mãe de filho maior de 5 Anos")]
        MotherChildMoreFiveYears,
        Empty

    }
    public enum HasYouWorked
    {
        [Description("TRABALHEI REGISTRADA.")]
        WasFired,
        [Description("TRABALHEI REGISTRADA antes de nascer.")]
        WorkRegisteredBeforeBorn,
        [Description("Sou MEI (Micro Empreendedor Individual).")]
        ImMEI,
        [Description("Paguei a previdência por conta (carnê GPS).")]
        PayedFormByMyOn,
        [Description("NÃO trabalhei registrada antes de nascer.")]
        NoWorkedRegisteredBeforeBorn,
        [Description("Nunca trabalhei registrada.")]
        NeverWorked,
        Empty

    }
    public enum ReceivedWorkInsurances
    {
        [Description("SIM, RECEBI seguro desemprego.")]
        Yes,
        [Description("NÃO recebi seguro desemprego.")]
        No,
        Empty
    }

}
