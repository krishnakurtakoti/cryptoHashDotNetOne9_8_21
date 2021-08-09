using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class PymtReqData
    {
        public PymtReqParms PymtReq { get; set; }

    }

    public class PymtReqParms
    {
        public string digitalId { get; set; }
        public string systemID { get; set; }
        public string paymentReturnURL { get; set; }

        public string customerEmail { get; set; }
        public string merchTxnRef { get; set; }
        public int amount { get; set; }

        public string paymentType { get; set; }
        public string timestamp { get; set; }
        public string ipAddress { get; set; }

        public int screenToDisplay { get; set; }
        public string localevalue { get; set; }
        public string customerMsisdn { get; set; }
        public string isDummyEmail { get; set; }

        public BOGPay BOGPay { get; set; }

    }


}
