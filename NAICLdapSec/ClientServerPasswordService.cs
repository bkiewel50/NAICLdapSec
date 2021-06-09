using System;
using System.Collections.Generic;
using System.Text;


namespace NAICLdapSec
{
    
    public class ClientServerPasswordService : LdapService, ClientServerPasswordServiceInterface
    {
        // <summary> Password service for Client/Server applications using the S drive.  <b>NOTE: Clients must call the close method when they are done with the object.</b></summary>

        private const System.String SDRIVE_APP_ID_ON_ROLE = "PASSWORD FILE";
        private const System.String SDRIVE_ROLE = "sdrive_password_access_pr";
        private System.String passwordFile;

        // <summary> Create a new object and connect to LDAP for a realm.</summary>
        // <param name="realm">The realm name. Clients should use the contants in ClientServerPasswordServiceInterface.</param>
        // <throws>  DaoException thrown when the realm is invalid or an LDAP error occurs. </throws>
        public ClientServerPasswordService(System.String realm) : base(false)
        { 
            // construct base class with no connection
            if (ClientServerPasswordServiceInterface_Fields.PROD_REALM.Equals(realm))
            {
                passwordFile = "s:\\oracle8\\prod\\" + PropertiesDao.PROPERTY_FILENAME;
            }
            else if (ClientServerPasswordServiceInterface_Fields.UA_REALM.Equals(realm))
            {
                passwordFile = "s:\\oracle8\\ua\\" + PropertiesDao.PROPERTY_FILENAME;
            }
            else if (ClientServerPasswordServiceInterface_Fields.QA_REALM.Equals(realm))
            {
                passwordFile = "s:\\oracle8\\qa\\" + PropertiesDao.PROPERTY_FILENAME;
            }
            else if (ClientServerPasswordServiceInterface_Fields.DVLP_REALM.Equals(realm))
            {
                passwordFile = "s:\\oracle8\\dvlp\\" + PropertiesDao.PROPERTY_FILENAME;
            }
            else
            {
                throw new DAOException("Invalid value for realm, " + realm);
            }
            createConnection(passwordFile);
        }

        // <summary> Returns the password for a application provided the user is a member of the SDRIVE_PASSWORD_ACCESS_PR role.</summary>
        // <param name="app_id">The application id</param>
        // <param name="user_id">The user of the application</param>
        // <param name="password_for_user_id">The password of the user.</param>
        // <returns> A String or null if the user is invalid or not a member of the role.</returns>
        // <throws>  DaoException indicates an LDAP error. </throws>
        public virtual System.String getPassword(System.String app_id, System.String user_id, System.String password_for_user_id)
        {
            System.String retval = null;
            UserService user_service = new UserService(Connection);
            try
            {
                User end_user = user_service.authenticate(user_id, password_for_user_id);
                if (end_user != null)
                {
                    user_service.loadRolesForApplication(end_user, SDRIVE_APP_ID_ON_ROLE);
                    if (end_user.Roles.Contains(Role.createApplicationRole(SDRIVE_APP_ID_ON_ROLE, SDRIVE_ROLE)))
                    {
                        PasswordDao dao = new PasswordDao(Connection);
                        retval = dao.getPasswordForSdrive(app_id);
                    }
                }
            }
            finally
            {
                user_service.close();
            }
            return retval;
        }
    }
}
