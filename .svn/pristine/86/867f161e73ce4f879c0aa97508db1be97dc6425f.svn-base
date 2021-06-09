using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;



namespace NAICLdapSec
{
    /** Read an encrypted properties file. **/
	public class PropertiesDao {

        private string host;
        private int port;
        private string username;
        private string password;

        public static System.String PROPERTY_FILENAME = "naicsec3.pwd";

        private static System.String CHECK = "CHECK";
        private static System.String VALUE = "VALUE";
 
        private string path;
        private Int16 iBufferLen;
        NameValueCollection DaoProps = new NameValueCollection();

	    byte[] KEY = 
	    {
		(byte)0x24, (byte)0x83, (byte)0x12, (byte)0x1C, (byte)0x93,
		(byte)0x22, (byte)0xCA, (byte)0xFE, (byte)0x28, (byte)0x41,
		(byte)0xA2, (byte)0x23, (byte)0x22, (byte)0x3C, (byte)0x22,
		(byte)0xDC, (byte)0x33, (byte)0x42, (byte)0x42, (byte)0xC4,
		(byte)0xBA, (byte)0xBE, (byte)0x62, (byte)0x5A
	    };

        public void Main()
	    {
            String NETWORK_PATH = "s:\\oracle8\\dvlp\\";
            try
            {
                NameValueCollection props = new NameValueCollection();
                props = read(NETWORK_PATH + PROPERTY_FILENAME);
                if (!props[CHECK].Equals(VALUE))
                {
                    throw new DAOException("Exception in PropertiesDAO:Main() CHECK != VALUE ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in PropertiesDAO:Main() " + ex.ToString());
            }
	    }

        private void setNameValue(string sValue)
        {
            try
            {
                char[] delimiterChars = { '\n' };
                string[] nvPair = sValue.Split(delimiterChars);
                foreach (string s in nvPair)
                {
                    int iPos = s.IndexOf("=");
                    if (iPos > 0)
                    {
                        String sTemp = s.Replace("\\=", "=");
                        String sName = sTemp.Substring(0, iPos);
                        String sKey = sTemp.Substring(iPos + 1);
                        DaoProps.Add(sName, sKey);
                      }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in PropertiesDAO::setNameValue() " + ex.ToString());
            }
        }

        public NameValueCollection read()
        {
            NameValueCollection props = new NameValueCollection();
            long lngBytesProcessed  = 0;
            Int32  intBytesInCurrentBlock =  0;
            byte[] byteBuffer = new byte[1024];

            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                long lngFileLength = fs.Length;
                while (lngBytesProcessed < lngFileLength) {
                    intBytesInCurrentBlock = fs.Read(byteBuffer, 0, byteBuffer.Length);
                    lngBytesProcessed = lngBytesProcessed + (long)(intBytesInCurrentBlock);
                }
                iBufferLen = (Int16) (lngBytesProcessed);
            }
            catch (Exception ex1)
            {
                System.Diagnostics.Trace.TraceWarning(ex1.Message);
            }
            try
            {
                string sRet = DESDecrypt(byteBuffer, iBufferLen);
                setNameValue(sRet);
                if (!DaoProps[CHECK].Equals(VALUE))
                {
                    throw new DAOException("Exception in PropertiesDAO:read() CHECK != VALUE ");
                }
                props = DaoProps;
            }
            catch (Exception ex2)
            {
                System.Diagnostics.Trace.TraceWarning(ex2.Message);
            }
            return props;
        }

        public NameValueCollection read(String filename)
        {
            NameValueCollection props = new NameValueCollection();
            long lngBytesProcessed = 0;
            Int32 intBytesInCurrentBlock = 0;
            byte[] byteBuffer = new byte[1024];
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

                long lngFileLength = fs.Length;
                while (lngBytesProcessed < lngFileLength)
                {
                    intBytesInCurrentBlock = fs.Read(byteBuffer, 0, byteBuffer.Length);
                    lngBytesProcessed = lngBytesProcessed + (long)(intBytesInCurrentBlock);
                }
                iBufferLen = (Int16)(lngBytesProcessed);
            }
            catch (Exception ex1)
            {
                System.Diagnostics.Trace.TraceWarning(ex1.Message);
            }
            try
            {
                string sRet = DESDecrypt(byteBuffer, iBufferLen);
                setNameValue(sRet);
                if (!DaoProps[CHECK].Equals(VALUE))
                {
                    throw new DAOException("Exception in PropertiesDAO:read() CHECK != VALUE ");
                }
                props = DaoProps;
            }
            catch (Exception ex2)
            {
                System.Diagnostics.Trace.TraceWarning(ex2.Message);
            }
            return props;
        }

        private string DESDecrypt(byte[] byteCipher, Int16 ibyteCipherArrayLen)
        {
            byte[] resultArray;
            String strReturn;
            try
            {
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = KEY;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform EncryptorDecryptor = tdes.CreateDecryptor();
                resultArray = EncryptorDecryptor.TransformFinalBlock(byteCipher, 0, ibyteCipherArrayLen);
                tdes.Clear();
                strReturn = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            return strReturn;
        }

        // <summary>Get/Set the LDAP server host name</summary>
        public String Host
        {
            get
            {
                if (DaoProps["HOST"] == null) { Main(); }
                return DaoProps["HOST"];
            }
            set
            {
                this.host = Host;
            }
        }

        // <summary>Get/Set the port the LDAP server is on</summary>
        public int Port
        {
            get
            {
                if (DaoProps["PORT"] == null) {  Main(); }
                return int.Parse(DaoProps["PORT"]);
            }
            set
            {
                this.port = Port;
            }
        }

        // <summary>Get/Set the password to authenticate with LDAP with</summary>
        public String Password
        {
            get
            {
                if (DaoProps["PASSWORD"] == null) { Main(); }
                return DaoProps["PASSWORD"];
            }
            set
            {
                this.password = Password;
            }
        }

        // <summary>Get/Set the user id to authenticate with LDAP with</summary>
        public String UserName
        {
            get
            {
                if (DaoProps["USERNAME"] == null) { Main(); }
                return DaoProps["USERNAME"];
            }
            set
            {
                this.username = UserName;
            }
        }
        

    }
}
