using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;



namespace NAICLdapSec
{
    class WSAuthenticate
    {
        string strResponse = System.String.Empty;

        // 2009-01-05 - the WS that we were using has been removed... it used Basic Authentication
        // and with the new configuration of OAM basic authentication is not available... No date
        // for getting a replacement available...
        public bool isAuthorized(String userid, String password, String realm)
        {
            bool isAuthorized = true;
            //strSoapEnvelope = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            //strSoapEnvelope += " <SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas-xmlsoap.org/soap/envelope/\" xmlns:oblix=\"http://www.oblix.com\">";
            //strSoapEnvelope += "  <SOAP-ENV:Body>";
            //strSoapEnvelope += "    <oblix:authentication type=\"basic\">";
            //strSoapEnvelope += "      <oblix:login>" + userid.ToUpper() + "</oblix:login>";
            //strSoapEnvelope += "      <oblix:password>" + password + "</oblix:password>";
            //strSoapEnvelope += "    </oblix:authentication>";
            //strSoapEnvelope += "    <oblix:request function=\"view\" mode=\"dataonly\" version=\"NPWSDL1.0\">";
            //strSoapEnvelope += "      <oblix:params>";
            //strSoapEnvelope += "        <oblix:uid>cn=" + userid.ToUpper() + ",ou=SSO,cn=Internal,dc=naic,dc=org</oblix:uid>";
            //strSoapEnvelope += "        <oblix:attrName>uid</oblix:attrName>";
            //strSoapEnvelope += "        <oblix:attrName>sn</oblix:attrName>";
            //strSoapEnvelope += "        <oblix:attrName>mail</oblix:attrName>";
            //strSoapEnvelope += "      </oblix:params>";
            //strSoapEnvelope += "    </oblix:request>";
            //strSoapEnvelope += "  </SOAP-ENV:Body>";
            //strSoapEnvelope += "</SOAP-ENV:Envelope>";
            //strRequest = "userid=" + userid.ToUpper() + "&password=" + password;
            //Console.WriteLine(strRequest);

            //string Url = System.String.Empty;
            //if (realm.Equals("dvlp"))
            //{
            //    //Url = "http://authd.naic.org:8080/identity/oblix/apps/userservcenter/bin/userservcenter.cgi";
            //    Url = "http://iapps-dvlp.naic.org:8080/auth/Auth";
            //    Url = "http://iapps-int.naic.org:4040/auth/Auth"; 
            //}
            //else if (realm.Equals("qa"))
            //{
            //    //Url = "https://authq.naic.org:2020/identity/oblix/apps/userservcenter/bin/userservcenter.cgi";
            //    Url = "https://iapps-qa.naic.org:2020/auth/Auth";
            //}
            //else if (realm.Equals("prod"))
            //{
            //    //Url = "https://auth.naic.org/identity/oblix/apps/userservcenter/bin/userservcenter.cgi";
            //    Url = "https://iapps.naic.org/auth/Auth";
            //}
            ////XMLHttpRequest httpRequest = new XMLHttpRequest();
            ////httpRequest.Open("POST", Url);
            ////httpRequest.SetRequestHeader("Accept", "text/html");
            //////httpRequest.SetRequestHeader("Content-Length", strSoapEnvelope.Length.ToString());
            //////httpRequest.Send(strSoapEnvelope);
            ////httpRequest.Send();
            ////System.Xml.XmlDocument oXmlDoc = httpRequest.GetResponseXML();
            ////string responseText = oXmlDoc.OuterXml;
            ////Console.WriteLine(responseText);
            ////XmlNodeList nlist = oXmlDoc.GetElementsByTagName("ObTextMessage");
            ////foreach (XmlNode node in nlist)
            ////{
            ////    string InnerXml = node.InnerXml.Trim();
            ////    if (InnerXml.Equals("Invalid credential."))
            ////    {
            ////        Console.WriteLine("Invalid credential.");
            ////        isAuthorized = false;
            ////        break;
            ////    }
            ////    if (InnerXml.Equals("Invalid credential. Login failed."))
            ////    {
            ////        Console.WriteLine("Invalid credential. Login failed.");
            ////        isAuthorized = false;
            ////        break;
            ////    }
            ////}

            

            //ASCIIEncoding encoding = new ASCIIEncoding();
            //string postData = string.Format("userid={0}&password={1}", userid.ToUpper(), password);
            //byte[] buffer = encoding.GetBytes(postData);

            //// Prepare web request... use POST or GET. set Content Type and length
            //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
            //myRequest.Method = "POST";
            //myRequest.ContentType = "application/x-www-form-urlencoded";
            //myRequest.ContentLength = buffer.Length;

            //// Get request stream , send data and close stream
            //Stream newStream = myRequest.GetRequestStream();
            //newStream.Write(buffer, 0, buffer.Length);
            //newStream.Close();
            

            //// Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
            //HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();
            //Stream streamResponse = myHttpWebResponse.GetResponseStream();
            //StreamReader streamRead = new StreamReader(streamResponse);
            

            //Char[] readBuffer = new Char[256];

            //// Read from buffer
            //int count = streamRead.Read(readBuffer, 0, 256);
            //while (count > 0)
            //{
            //    // get string
            //    String resultData = new String(readBuffer, 0, count);
            //    strResponse = strResponse + resultData;

            //    // Read from buffer
            //    count = streamRead.Read(readBuffer, 0, 256);
            //}
            //int iPos = strResponse.IndexOf("PASSED");
            //Console.WriteLine(strResponse);
            //if (iPos > 0)
            //{
            //    isAuthorized = true;
            //}
            //else
            //{
            //    isAuthorized = false;
            //}

            //// Release the response object resources, and close response
            //streamRead.Close();
            //streamResponse.Close();
            //myHttpWebResponse.Close();
            
            return isAuthorized;
        }

    }
}
