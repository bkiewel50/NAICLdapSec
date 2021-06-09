using System;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    // <summary> Provides the interface for the management of a LDAP Connection.</summary>
    public interface LdapServiceInterface
    {
        // <summary> Get the LDAP Connection.</summary>
        // <returns> a Connection object.</returns>
        LDAPConnection Connection
        {
            get;

        }

        // <summary> Clean up the service. If the connection is owned and connected, the connection is
        // closed.</summary>
        // <throws>  DaoException  thrown if the persistent layer fails. </throws>
        void close();
    }
}
