using Alten.Connected_Vehicles.DTO;
using Alten.Connected_Vehicles.Infrastructure.RestClients;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPServer.Common.WebAPIConsumer
{
    public class WebApiConsumer
    {

        public static bool SendRawData(byte[] RawData)
        {
            string APIURL=ConfigurationManager.AppSettings["APIURL"];
            string MethodURL= ConfigurationManager.AppSettings["RawServiceMethod"];
            GlobalRestClient<string> restClient = new GlobalRestClient<string>(APIURL);
            string base64string = Convert.ToBase64String(RawData);
            string Result=restClient.Add(base64string, MethodURL);

            if (Result.ToLower() == "ok")
            {
                return true;
            }
            return false;
        }

        public static bool SendTransaction(TransactionDTO Transaction)
        {
            string APIURL = ConfigurationManager.AppSettings["APIURL"];
            string MethodURL = ConfigurationManager.AppSettings["TransactionServiceMethod"];
            GlobalRestClient<TransactionDTO> restClient = new GlobalRestClient<TransactionDTO>(APIURL);
            
            string Result = restClient.Add(Transaction, MethodURL);

            if (Result.ToLower() == "ok")
            {
                return true;
            }
            return false;
        }
    }
}
