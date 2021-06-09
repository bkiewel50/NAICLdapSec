using System;

using Syscert = System.Security.Cryptography.X509Certificates;

using Novell.Directory.Ldap;

using Novell.Directory.Ldap.Utilclass;
using Mono.Security.X509;
using Mono.Security.Cryptography;

using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    /// <summary>
    /// This class encapsulates a LDAP connection. The connection is authenticated 
    /// to a LDAPv3 server on a host and a port. 
    /// </summary>
    public class LDAPConnection
    {

        private string host;
        private int port;
        private string username;
        private string password;

        protected static bool bOkToProceed, removeFlag = false;
        protected static int bindCount = 0;
 

        //private DirectoryEntry context;

        private LdapConnection connection;

        // <summary>Create an new LdapConnection object and Set the host, port, username 
        // and password for the connection</summary>
        // <param name="host">LDAP server host name (cannot be null)</param>
        // <param name="port">port the LDAP server is running  (cannot be null)</param>
        // <param name="username">user name to connect with (cannot be null)</param>
        // <param name="password">password for the user name (cannot be null)</param>
        public LDAPConnection(string host, int port, string username, string password) 
        {
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;
        } 

        // <summary> Connect to the LDAP server using the host, port, username, and password.</summary> 
        public void connect()
        {

            bOkToProceed = true;

            try
            {
                
                connection = new LdapConnection();
                connection.SecureSocketLayer = true;
                //Console.WriteLine("Connecting to:" + host);

                connection.UserDefinedServerCertValidationDelegate += new
                    CertificateValidationCallback(MySSLHandler);
                if (bOkToProceed == false)
                {
                    //Don't proceed....
                }
                if (bOkToProceed == true)
                {
                    connection.Connect(host, port);
                    connection.Bind(username, password);
                    
                    Console.WriteLine(" SSL Bind Successfull ");
                }
            }
            catch (LdapException ee)
            {
                Console.WriteLine(ee.LdapErrorMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }


        }


        public static bool MySSLHandler(Syscert.X509Certificate certificate,
                    int[] certificateErrors)
        {

            X509Store store = null;
            X509Stores stores = X509StoreManager.CurrentUser;
            store = stores.TrustedRoot;


            //Import the details of the certificate from the server.
            X509Certificate x509 = null;
            X509CertificateCollection coll = new X509CertificateCollection();
            byte[] data = certificate.GetRawCertData();
            if (data != null)
                x509 = new X509Certificate(data);

            //List the details of the Server

            //check for ceritficate in store
            X509CertificateCollection check = store.Certificates;
            if (!check.Contains(x509))
            {
                if (bindCount == 1)
                {
                    Console.WriteLine(" \n\nCERTIFICATE DETAILS: \n");
                    Console.WriteLine(" {0}X.509 v{1} Certificate", (x509.IsSelfSigned ? "Self-signed " : String.Empty), x509.Version);
                    Console.WriteLine("  Serial Number: {0}", CryptoConvert.ToHex(x509.SerialNumber));
                    Console.WriteLine("  Issuer Name:   {0}", x509.IssuerName);
                    Console.WriteLine("  Subject Name:  {0}", x509.SubjectName);
                    Console.WriteLine("  Valid From:    {0}", x509.ValidFrom);
                    Console.WriteLine("  Valid Until:   {0}", x509.ValidUntil);
                    Console.WriteLine("  Unique Hash:   {0}", CryptoConvert.ToHex(x509.Hash));
                    Console.WriteLine();
                }

                bOkToProceed = true;
    
            }
            else
            {
                if (bOkToProceed == true)
                {
                    //Add the certificate to the store.

                    if (x509 != null)
                        coll.Add(x509);
                    store.Import(x509);
                    if (bindCount == 1)
                        removeFlag = true;
                }
            }
            if (bOkToProceed == false)
            {
                //Remove the certificate added from the store.

                if (removeFlag == true && bindCount > 1)
                {
                    foreach (X509Certificate xt509 in store.Certificates)
                    {
                        if (CryptoConvert.ToHex(xt509.Hash) == CryptoConvert.ToHex(x509.Hash))
                        {
                            store.Remove(x509);
                        }
                    }
                }
                Console.WriteLine("SSL Bind Failed.");
            }
            return bOkToProceed;
        }

        // <summary>Disconnect from the LDAP server.</summary>
        public void disconnect()
        {
            DAOException error = null;
            try
            {
                //context.Dispose();
                //context = null;

                connection.Disconnect();
                connection = null;
            }
            catch (Exception ex)
            {
                error = new DAOException("Exception trying to Disconnect from LDAP.", ex);
            }

        }

        // <summary>True if the object is connected to the LDAP server</summary>
        public bool isConnected()
        {
            //return context != null;
            return connection != null;
        }

        // <summary>True if the object is connected to the LDAP server</summary>
        public LdapConnection getConnection()
        {
            //return context != null;
            return connection;
        }

        // <summary>True if the object is connected to the LDAP server</summary>
        public bool Connected()
        {
            //return context != null;
            return connection != null;

        }

        // <summary>Get/Set the LDAP server host name</summary>
        public String Host
        {
            get
            {
                return host;
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
                return port;
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
                return password;
            }
            set
            {
                this.password  = Password;
            }
        }

        // <summary>Get/Set the user id to authenticate with LDAP with</summary>
        public String UserName
        {
            get
            {
                return username;
            }
            set
            {
                this.username  = UserName;
            }
        }

        //public DirectoryEntry getContext()
        //{
        //    return context;
        //}
  

    } // end of LDAPConnection
    


}
