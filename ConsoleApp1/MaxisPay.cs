using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    public class MaxisPay
    {
        static string systemID = "MG0001";
        static string systemPassword = "K411Bu278KiOQLoGh";
        static string constant = "MaxISMGoB2141SaLTkeYAB";
        static string customerEmail = "null@cybersource.com";

        static string merchTxnRef = RandomString(8);
        static int amount = 500;
        static string paymentType = "basket";

        static string channel = "MMAzxcvbnm";
        static DateTime todaysDate = DateTime.Now;
        static string timeStamp = "2021080420452247";
            //todaysDate.ToString("yyyyMMddHHmmssff");
            //static string timeStampf = todaysDate.ToString("yyyyMMddHHmmssff");


        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //Hash Genration
        public static void GenrateHash()
        {
            GetUserData();

            string variable1 = timeStamp.Substring(2, 2)
                             + timeStamp.Substring(5, 1)
                             + timeStamp.Substring(7, 1)
                             + timeStamp.Substring(9, 1)
                             + timeStamp.Substring(11, 1)
                             + timeStamp.Substring(13, 3);


            var ip = (from address in NetworkInterface.GetAllNetworkInterfaces().Select(x => x.GetIPProperties()).SelectMany(x => x.UnicastAddresses).Select(x => x.Address)
                      where !IPAddress.IsLoopback(address) && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                      select address).FirstOrDefault().ToString();

            string variable2 = ip.Substring(ip.Length - 3).Replace('.', ' ').Trim();

            string saltKey = constant + variable1 + variable2;

            //// Hash Genration
            string unhashedData = saltKey + //salt key
                                  customerEmail.Substring(0, 4) 
                                  + customerEmail.Substring(customerEmail.Length - 5)  //left4 and right5
                                  + systemID
                                  + merchTxnRef + amount + paymentType + systemPassword;

            string hashedData = Hash.ComputeSha256Hash(unhashedData);

            PymtReqParms py = new PymtReqParms();
            py.digitalId = "DIGIT00001";
            py.systemID = systemID;
            py.paymentReturnURL = "https://www.w3schools.com/";
            py.customerEmail = customerEmail;
            py.merchTxnRef = merchTxnRef;
            py.amount = amount;
            py.paymentType = paymentType;
            py.timestamp = timeStamp;
            py.ipAddress = ip;
            py.screenToDisplay = 6;
            py.isDummyEmail = "Y";

            BOGPay o = new BOGPay();
            o.orderID = RandomString(8);
            o.orderInfo = "TestTransaction";
            o.hash = hashedData;
            py.BOGPay = o;


            paymentObj p = new paymentObj();
            p.channel = channel;

            PymtReqData pd = new PymtReqData();
            pd.PymtReq = py;

            p.payload = Newtonsoft.Json.JsonConvert.SerializeObject(pd);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(p);


            string signature = Hash.GenarateSignature(timeStamp, "/ms/genMsToken", json);

            Console.WriteLine("TokenSignature : " + signature);
            Console.ReadKey();
        }


        public static void GetUserData()
        {
            LoginUser l = new LoginUser();
            l.username = "Z21zdXNlcg";
            l.password = "Z21zdXNlcg";

            var Userjson = Newtonsoft.Json.JsonConvert.SerializeObject(l);

            string Logsignature = Hash.GenarateSignature(timeStamp, "/intLoginV2", Userjson);

            Console.WriteLine("TimeStamp : " + timeStamp);
            Console.WriteLine("LogSignature : " + Logsignature);

        }

     

       
    }
}
