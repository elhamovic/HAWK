using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HAWK_v.Helpers
{
    public class SmartfaceRequest
    {
        private string serverName = "https://localhost:5001/Smartface/";
        private string Token;
        public SmartfaceRequest() { }
        public SmartfaceRequest(string token){ 
            this.Token = token;
        }

        public string requestNoBody(string reqUrl, string methodType)
        {
            string res = null;
            
                var httpWebRequest =
                    (HttpWebRequest)WebRequest.Create(serverName + reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;
                httpWebRequest.Headers.Add("Authorization", "Bearer " + Token);
                var response = (HttpWebResponse)httpWebRequest.GetResponse();
                res = new StreamReader((response).GetResponseStream()) .ReadToEnd();
            
            return res;
        }

        public  string requestWithBody(string reqUrl, string methodType, string json)
        {
            string res = null;
            
                var httpWebRequest =
                    (HttpWebRequest)WebRequest.Create(serverName + reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;
            httpWebRequest.Headers.Add("Authorization", "Bearer " + Token);
            Console.WriteLine(json);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    try
                    {
                        streamWriter.Write(json);
                    }
                    finally
                    {
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                var response = (HttpWebResponse)httpWebRequest.GetResponse();
                res = res = new StreamReader((response).GetResponseStream()).ReadToEnd();
            
            return res;
        }
    }
}