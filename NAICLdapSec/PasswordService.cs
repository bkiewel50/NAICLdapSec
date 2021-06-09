using System;
using System.DirectoryServices;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    public class PasswordService : LdapService, PasswordServiceInterface
    {
        private PasswordDao dao;

        // <summary>Create a new PasswordService object along with a connection. </summary>
        public PasswordService()
            : base()
        {
            dao = new PasswordDao(Connection);
        }

        // <summary>Create a new PasswordService reusing a connection. 
        // This service doesn't own the connection so it will not close it 
        // when the client calls close. </summary>
        public PasswordService(LDAPConnection conn) : base (conn)
        {
            dao = new PasswordDao(Connection);
        }

        // <summary>Create a new PasswordService and create a new Connection using a 
        // properties file.  </summary>
        public PasswordService(string filename) : base(filename)
        {
            dao = new PasswordDao(Connection);
        }


        // <summary> Get the password for an application</summary> 
        public string getPassword(string applicationID)
        {
            String password = null;
            try
            {
                password = dao.getPassword(applicationID);
            }
            catch (Exception)
            {

            }
            return password;
        }

        public static string getApplicationPassword(string applicationID)
        {
            PasswordService service = new PasswordService();
            String password = null;
            try
            {
                password = service.getPassword(applicationID);
            }
            catch (Exception)
            {

            }
            return password;
        }

    }  // end of PasswordService
}
