
using System;
using System.Xml;
using System.Net;
using System.IO;
using System.Text;

namespace NAICLdapSec
{

    public enum eReadyState : int
    {
        UNINITIALIZED = 0,
        LOADING = 1,
        LOADED = 2,
        INTERACTIVE = 3,
        COMPLETED = 4
    }

    class XMLHttpRequest
    {
        private eReadyState lgReadyState = eReadyState.UNINITIALIZED;
        private HttpWebRequest lgRequest;
        private HttpWebResponse lgResponse;

        public void Open(string Method, string Url)
        {
            if (lgRequest != null)
                throw new InvalidOperationException("Connection Already open");

            if (Url == "" || Url == null)
                throw new ArgumentNullException("URL must be specified");

            System.Uri uriObj = new System.Uri(Url);
            if (!((uriObj.Scheme == System.Uri.UriSchemeHttp) ||
                    (uriObj.Scheme == System.Uri.UriSchemeHttps)))
                throw new ArgumentOutOfRangeException("URL Scheme is not http or https");

            if (Method == null ||
                 (Method.ToUpper() != "POST" && Method.ToUpper() != "GET" &&
                   Method.ToUpper() != "PUT" &&
                    Method.ToUpper() != "PROPFIND"))
                throw new ArgumentOutOfRangeException("Method argument type not defined");

            lgRequest = (HttpWebRequest)WebRequest.CreateDefault(uriObj);
            lgRequest.Method = Method;
            lgRequest.ContentType = "text/xml";
            lgReadyState = eReadyState.LOADING;
        }

        public void SetRequestHeader(string headerName, string headerValue)
        {
            try
            {
                if (lgReadyState != eReadyState.LOADING)
                    throw new InvalidOperationException("Setting request headers is not allowed at this ReadyState");

                switch (headerName)
                {
                    case "Accept":
                        lgRequest.Accept = headerValue;
                        break;
                    case "Connection":
                        lgRequest.Connection = headerValue;
                        break;
                    case "Content-Length":
                        lgRequest.ContentLength = Convert.ToInt32(headerValue);
                        break;
                    case "Content-Type":
                        lgRequest.ContentType = headerValue;
                        break;
                    case "Expect":
                        lgRequest.Expect = headerValue;
                        break;
                    case "Date":
                        throw new Exception("These headers are set by the system");
                    case "Host":
                        throw new Exception("These headers are set by the system");
                    case "Range":
                        throw new Exception("This header is set with AddRange");
                    case "Referer":
                        lgRequest.Referer = headerValue;
                        break;
                    case "Transfer-Encoding":
                        lgRequest.TransferEncoding = headerValue;
                        break;
                    case "User-Agent":
                        lgRequest.UserAgent = headerValue;
                        break;
                    default:
                        lgRequest.Headers.Add(headerName, headerValue);
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred while setting request headers", e);
            }
        }

        public void Send(string body)
        {
            try
            {
                //Console.WriteLine("In XMLHttpRequest::send() body " + body);
                if (lgReadyState != eReadyState.LOADING)
                    throw new InvalidOperationException("Sending a message is not allowed at this ReadyState");
                if (body != null)
                {
                    StreamWriter stream = new StreamWriter
                        (lgRequest.GetRequestStream(), Encoding.ASCII);
                    stream.Write(body);
                    stream.Close();
                    lgResponse = (HttpWebResponse)lgRequest.GetResponse();
                    lgReadyState = eReadyState.COMPLETED;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in XMLHttpRequest::send() " + e.ToString());
            }
        }


        public XmlDocument GetResponseXML()
        {
            try
            {
                if(lgReadyState==eReadyState.COMPLETED)
                {
                    Stream stream = lgResponse.GetResponseStream();
                    XmlTextReader reader = new XmlTextReader(stream);
                    XmlDocument document = new XmlDocument();
                    document.Load(reader);
                    reader.Close();
                    stream.Close();
                    return document;               
                }
                else
                    throw new InvalidOperationException("Getting response XML is forbidden at current ReadyState");
            }
            catch (Exception e)
            {
                throw new Exception ("Error occurred while retrieving XML response",e);
            }
        }

        




    }
}
