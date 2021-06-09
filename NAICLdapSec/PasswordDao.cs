using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace NAICLdapSec
{
    /// <summary> This data-access object class provides getting a password for an application from LDAP.</summary>
    public class PasswordDao : LDAPDao
    {
        // <summary> The LDAP password attribute for the application.</summary>
        private const System.String RESOURCE_NAME_ATTR = "orclResourceName";

        // <summary> The LDAP password attribute for the application.</summary>
        private const System.String PASSWORD_ATTR = "orclpasswordattribute";
        
        // <summary> The LDAP sdrive access attribute for the application.</summary>
        private const System.String SDRIVE_ATTR = "usedforsdrive";

        // <summary> Create a new UserDao object referencing the LDAP connection. The reference is kept.</summary>
        // <param name="connection">The LDAP connection object.</param>
        public PasswordDao(LDAPConnection connection)
            : base(connection)
        {
        }

        // <summary> Find the application's id and return its database password. If the password is not found,
        // return null.</summary>
        // <param name="app_id">The application's database id.</param>
        // <returns> a String contains the password or null for not found.</returns>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        public virtual System.String getPassword(System.String app_id)
        {
            return (System.String)findObject(APP_ID_ROOT_DN, RESOURCE_NAME_ATTR + "=" + app_id, new System.String[] { PASSWORD_ATTR });
        }

        // <summary> Find the S drive application's id and return its database password. If the password is not found,
        // return null.</summary>
        // <param name="app_id">The application's database id.</param>
        // <returns> a String contains the password or null for not found.</returns>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        public virtual System.String getPasswordForSdrive(System.String app_id)
        {
            return (System.String)findObject(APPLICATIONS_ROOT_DN, "(&(" + RESOURCE_NAME_ATTR + "=" + app_id + ")(" + SDRIVE_ATTR + "=true))", new System.String[] { PASSWORD_ATTR });
        }

        // <summary> Create a String from the password attribute in the LDAP data.</summary>
        // <param name="dn">The distinguishedName for the application node.</param>
        // <param name="attrs">The attributes read from LDAP</param>
        // <returns> a new String object.</returns>
        // <throws>  NamingException JNDI exception occurred. </throws>
         protected internal override System.Object createObjectForFind(System.String dn, ListDictionary attrs)
        {
            return readAttribute(PASSWORD_ATTR, attrs);
        }
    }
}
