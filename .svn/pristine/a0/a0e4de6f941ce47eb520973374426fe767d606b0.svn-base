using System;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    // <summary> Provides an application its password.</summary>
    public interface PasswordServiceInterface : LdapServiceInterface
    {
        // <summary> Get the password for an application.</summary>
        // <param name="app_id">The applicaton id</param>
        // <returns> The password or null if not found.</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        System.String getPassword(System.String app_id);
    }
}
