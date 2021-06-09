using System;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    // <summary> Provides the management of a LDAP Connection. The connection can be owned or not owned by
    // the service. If the service owns the connection, it will close it when the close() method is called.
    // Otherwise, the connection remains open.</summary>
    public abstract class LdapService : LdapServiceInterface
    {
        // <summary> Get the LDAP Connection.</summary>
        // <returns> a Connection object.</returns>
        virtual public LDAPConnection Connection
        {
            get
            {
                return connection;
            }

        }
        private bool ownedConnection = false;
        private LDAPConnection connection;


        protected static bool bHowToProceed, quit = false, removeFlag = false;
        protected static int bindCount = 0;

        // <summary> Connected to the LDAP server and create a new Connection object.</summary>
        // <throws>  DaoException  thrown if the persistent layer fails. </throws>
        public LdapService()
        {
            string filePath = System.Environment.GetEnvironmentVariable("NAICPWD");
            if (!(filePath.EndsWith("\\") || filePath.EndsWith("/")))
            {
                filePath = filePath + "\\";
            }
            filePath = filePath + PropertiesDao.PROPERTY_FILENAME;
            createConnection(filePath);
        }

        // <summary> Connected to the LDAP server using the properties in the encrypted file
        // and create a new Connection object.</summary>
        // <param name="filename">The properties file for the connection.</param>
        // <throws>  DaoException  thrown if the persistent layer fails. </throws>
        public LdapService(System.String filename)
        {
            createConnection(filename);
        }

        // <summary> Set the reference to the provided connection and mark it as not owned.</summary>
        // <param name="con">The LDAP conneciton.</param>
        public LdapService(LDAPConnection con)
        {
            this.connection = con;
            ownedConnection = false;
        }

        // <summary> Create the LdapService that doesn't own a connection.</summary>
        protected internal LdapService(bool place_holder)
        {
            connection = null;
            ownedConnection = false;
        }

        // <summary> Create and set the LdapConnection as owned by this object.</summary>
        // <param name="filename">The properties file for the connection.</param>
        // <throws>  DaoException  thrown if the persistent layer fails. </throws>
        protected internal virtual void createConnection(System.String filename)
        {
            ownedConnection = true;
            System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection();
            props = new PropertiesDao().read(filename);
            connect(props);
        }

        // <summary> Clean up the service. If the connection is owned and connected, the connection is
        // closed.</summary>
        // <throws>  DaoException  thrown if the persistent layer fails. </throws>
        public virtual void close()
        {
            if (ownedConnection && connection.Connected())
            {
                connection.disconnect();
            }
        }

        ~LdapService()
        {
            try
            {
                if (ownedConnection && connection.Connected())
                {
                    System.Console.Error.WriteLine("A client of the LdapService class did not call close on the object. " + "LDAP resources are leaking and will cause system problems.");
                    close();
                }
            }
            finally
            {
        
            }
        }

        
        private void connect(System.Collections.Specialized.NameValueCollection props)
        {
            System.String host = props.Get("HOST");
            int port = System.Int32.Parse(props.Get("PORT"));
            System.String username = props.Get("USERNAME");
            System.String password = props.Get("PASSWORD");


            //host = "idmr-test.naic.org";
            //port = 636;
            //username = "cn=commonapp,ou=sa,o=data";
            //password = "D3Ok70i9s9ly4j3";
            connection = new LDAPConnection(host, port, username, password);
            connection.connect();


        }
    }
}
