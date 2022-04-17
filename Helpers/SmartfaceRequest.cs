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
        private string serverName = "http://localhost:8098/api/v1/";

        public async Task<string> requestNoBody(string reqUrl, string methodType)
        {
            string res = null;
            await Task.Run(async () =>
            {
                var httpWebRequest =
                    (HttpWebRequest)WebRequest.Create(serverName + reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;
                var response = (HttpWebResponse)httpWebRequest.GetResponse();
                res = new StreamReader((response).GetResponseStream()) .ReadToEnd();
            });
            return res;
        }

        public async Task<string> requestWithBody(string reqUrl, string methodType, string json)
        {
            string res = null;
            await Task.Run(async () =>
            {
                var httpWebRequest =
                    (HttpWebRequest)WebRequest.Create(serverName + reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;

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
            });
            return res;
        }
    }
}