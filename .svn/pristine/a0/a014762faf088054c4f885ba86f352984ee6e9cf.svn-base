using System;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    // <summary> Password service for Client/Server applications using the S drive.  <b>NOTE: Clients must call the close method when they are done with the object.</b></summary>
    public struct ClientServerPasswordServiceInterface_Fields
    {
        // <summary> Constant for the develop realm name.</summary>
        public readonly static System.String DVLP_REALM = "dvlp";
        
        // <summary> Constant for the QA realm name.</summary>
        public readonly static System.String QA_REALM = "qa";
        
        // <summary> Constant for the UA realm name.</summary>
        public readonly static System.String UA_REALM = "ua";
        
        // <summary> Constant for the production realm name.</summary>
        public readonly static System.String PROD_REALM = "prod";
    }

    public interface ClientServerPasswordServiceInterface : LdapServiceInterface
    {
        // <summary> Returns the password for a application provided the user is a member of the SDRIVE_PASSWORD_ACCESS_PR role.</summary>
        // <param name="app_id">The application id</param>
        // <param name="user_id">The user of the application</param>
        // <param name="password_for_user_id">The password of the user.</param>
        // <returns> A String or null if the user is invalid or not a member of the role.</returns>
        // <throws>  DaoException indicates an LDAP error. </throws>
        System.String getPassword(System.String app_id, System.String user_id, System.String password_for_user_id);
    }
}
