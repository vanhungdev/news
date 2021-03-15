using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace news_FE.Request
{
    public static class SendRequest
    {
        public static string sendRequestGET(string Url,string token = null)
        {
            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(Url);           
                httpWReq.Method = "GET";
                httpWReq.Headers["HEADER_AUTHORIZATION"] = (string)HttpContext.Current.Session["access_token"];
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                string jsonresponse = "";
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                }
                return jsonresponse;
            }
            catch (WebException e)
            {
                return e.Message;
            }
        }

        public static string sendRequestPOSTwithJsonContent(string Url,string postJsonString)
        {
            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(Url);
                var postData = postJsonString;
                var data = Encoding.UTF8.GetBytes(postData);
                httpWReq.ProtocolVersion = HttpVersion.Version11;
                httpWReq.Headers["HEADER_AUTHORIZATION"] = (string)HttpContext.Current.Session["access_token"];
                httpWReq.ContentLength = data.Length;   
                httpWReq.ContentType = "application/json";
                httpWReq.Method = "POST";
                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                string jsonresponse = "";
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                }
                return jsonresponse;
            }
            catch (WebException e)
            {
                return e.Message;
            }
        }
    }
}