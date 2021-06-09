using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NAICLdapSec
{
    /// <summary> Provides a set of services on User object objects.</summary>
    public interface UserServiceInterface : LdapServiceInterface
    {
        // <summary> Search for a user in the persistent storage based on user id. If the user id is not found, 
        // return null else return the User object matching the user id.</summary>
        // <param name="uid">The user id.</param>
        // <returns> a User object or null if not found.</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        User findUserByUserId(System.String uid);

        // <summary> Search for a user in the persistent storage based on distinguished name. If the user id is 
        // not found, return null else return the User object matching the user id.</summary>
        // <param name="dn">The distinguished name.</param>
        // <returns> a User object or null if not found.</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        User findUserByDistinguishedName(System.String dn);

        // <summary> Search for a set user in the persistent storage based on user id. If the user id is not found, 
        // return null else return the User object matching the user id.</summary>
        // <param name="base_dn">The starting location in the LDAP tree.</param>
        // <param name="filter">The LDAP search condition.</param>
        // <returns> A Set</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        Collection<System.Object> findUsers(System.String base_dn, System.String filter);

        // <summary> Load the roles for an application and user into the provided User object.</summary>
        // <param name="user">The User object.</param>
        // <param name="application">The name of the application.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        void loadRolesForApplication(User user, System.String application);

        // <summary> Load the roles for an application and user into the provided User object. The roles are
        // sorted in the order given by the Comparator object.</summary>
        // <param name="user">The User object.</param>
        // <param name="application">The name of the application.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        void loadRolesForApplication(User user, System.String application, System.Collections.IComparer c);

        // <summary> Load the roles for an application and user into the provided User object.</summary>
        // <param name="user">The User object.</param>
        // <param name="application">The name of the application.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        void loadRolesForApplications(User user, NAICLDAPSec.HashSet<String> applications);

        // <summary> Load the roles for an application and user into the provided User object. The roles are
        // sorted in the order given by the Comparator object.</summary>
        // <param name="user">The User object.</param>
        // <param name="application">The name of the application.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        void loadRolesForApplications(User user, NAICLDAPSec.HashSet<String> applications, System.Collections.IComparer c);

        // <summary> Load the roles for a database and user  into the provided User object.</summary>
        // <param name="user">The User object.</param>
        // <param name="database">The ORACLE SID of the database.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        void loadRolesForDatabase(User user, System.String database);

        // <summary> Load the roles for a database and user  into the provided User object. The roles are
        // sorted in the order given by the Comparator object.</summary>
        // <param name="user">The User object.</param>
        // <param name="database">The ORACLE SID of the database.</param>
        // <param name="c">Sorts by the Compartor for the Set.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        void loadRolesForDatabase(User user, System.String database, System.Collections.IComparer c);
    }
}
