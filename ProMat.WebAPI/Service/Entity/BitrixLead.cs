using System.Collections.Generic;

namespace ProMat.WebAPI.Service
{
    internal class BitrixLead
    {
        public string TITLE { get; set; }
        public string CURRENCY_ID { get; set; }
        public string STATUS_ID { get; set; }
        public string OPENED { get; set; }
        public string OPPORTUNITY { get; set; }
        public string COMPANY_TITLE { get; set; }
        public Phone[] PHONE { get; set; }
        public Email[] EMAIL { get; set; }


    }
}