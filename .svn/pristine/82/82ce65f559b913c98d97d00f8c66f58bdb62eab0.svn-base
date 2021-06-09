using System;
using System.DirectoryServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Globalization;

namespace NAICLdapSec
{
    // <summary> Provides a set of services on User object objects. <b>NOTE: Clients must call the close method when they are done with the object.</b></summary>
    public class UserService : LdapService, UserServiceInterface
    {
        private const String ROLE_LOCATION_DATABASE = "role.location.database";
        private const String ROLE_LOCATION_APPLICATION = "role.location.application";
        private const String ROLE_LOCATION_NONE = "role.location.none";

        private UserDao dao;


        // <summary> Create a new UserService object along with a connection.</summary>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public UserService() : base()
        {
            dao = new UserDao(Connection);
        }

        // <summary> Create a new UserService reusing a connection. This service doesn't own the connection
        // so it will not close it when the client calls close.</summary>
        // <param name="con">The Connection to a persistent storage.</param>
        public UserService(LDAPConnection con) : base(con)
        {
            dao = new UserDao(Connection);
        }

        // <summary> Create a new UserService and create a new Connection using a properties file.</summary>
        // <param name="filename">the encrypted properties file</param>
        public UserService(System.String filename) : base(filename)
        {
            dao = new UserDao(Connection);
        }

        // <summary> Search for a user in the persistent storage based on user id. If the user id is not found, 
        // return null else return the User object matching the user id.</summary>
        // <param name="uid">The user id.</param>
        // <returns> a User object or null if not found.</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual User findUserByUserId(System.String uid)
        {
            User user = dao.findUserByUserId(uid);
            return user;
        }

        // <summary> Search for a user in the persistent storage based on distinguished name. If the user id is 
        // not found, return null else return the User object matching the user id.</summary>
        // <param name="dn">The distinguished name.</param>
        // <returns> a User object or null if not found.</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual User findUserByDistinguishedName(System.String dn)
        {
            User user = dao.findUserByDistinguishedName(dn);
            return user;
        }

        // <summary> Search for a user in the persistent storage based on cn. If the user id is 
        // not found, return null else return the User object matching the user id.</summary>
        // <param name="cn">The common name.</param>
        // <returns> a User object or null if not found.</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual User findUserByCn(System.String cn)
        {
            User user = dao.findUserByCn(cn);
            return user;
        }

        // <summary> Search for a set user in the persistent storage based on user id. If the user id is not found, 
        // return null else return the User object matching the user id.</summary>
        // <param name="base_dn">The starting location in the LDAP tree.</param>
        // <param name="filter">The LDAP search condition.</param>
        // <returns> A Set</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        // TODO:
        public virtual Collection<System.Object> findUsers(System.String base_dn, System.String filter)
        {
            //retval = (User)dao.findUsers(base_dn, filter);
            //return retval;
            return (Collection<System.Object>)dao.findUsers(base_dn, filter);
        }
        

        // <summary> Load the roles for an application and user into the provided User object.</summary>
        // <param name="user">The User object.</param>
        // <param name="application">The name of the application.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual void loadRolesForApplication(User user, System.String application)
        {
            String[] appids = new String[1];
            appids[0] = application;
            //appids[0] = application.ToLower(new CultureInfo("en-US", false));
            dao.loadRolesForApplications(user, appids);
        }

        // <summary> Load the roles for an application and user into the provided User object. The roles are
        // sorted in the order given by the Comparator object.</summary>
        // <param name="user">The User object.</param>
        // <param name="application">The name of the application.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual void loadRolesForApplication(User user, System.String application, System.Collections.IComparer c)
        {
            String[] appids = new String[1];
            appids[0] = application;
            dao.loadRolesForApplications(user, appids, c);
        }

        // <summary> Load the roles for an application and user into the provided User object.</summary>
        // <param name="user">The User object.</param>
        // <param name="application">The name of the application.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual void loadRolesForApplications(User user, NAICLDAPSec.HashSet<String> applications)
        {
            String[] appIds = new String[applications.Count];
            applications.CopyTo(appIds);
            dao.loadRolesForApplications(user, appIds);
        }

        // <summary> Load the roles for an application and user into the provided User object. The roles are
        // sorted in the order given by the Comparator object.</summary>
        // <param name="user">The User object.</param>
        // <param name="application">The name of the application.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual void loadRolesForApplications(User user, NAICLDAPSec.HashSet<String> applications, System.Collections.IComparer c)
        {
            String[] appIds = new String[applications.Count];
            applications.CopyTo(appIds);
            dao.loadRolesForApplications(user, appIds, c);
        }

        // <summary> Load the roles for a database and user  into the provided User object.</summary>
        // <param name="user">The User object.</param>
        // <param name="database">The ORACLE SID of the database.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual void loadRolesForDatabase(User user, System.String database)
        {
            dao.loadRolesForDatabase(user, database);
        }

        // <summary> Load the roles for a database and user  into the provided User object. The roles are
        // sorted in the order given by the Comparator object.</summary>
        // <param name="user">The User object.</param>
        // <param name="database">The ORACLE SID of the database.</param>
        // <param name="c">Sorts by the Compartor for the Set.</param>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual void loadRolesForDatabase(User user, System.String database, System.Collections.IComparer c)
        {
            dao.loadRolesForDatabase(user, database, c);
        }

        // <summary> Authenticate and create a User object for the provided user id and password. If the user 
        // id or password do not match, the method returns null.</summary>
        // <param name="name">The user id.</param>
        // <param name="password">The user's password</param>
        // <returns> a User object or null.</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual User authenticate(System.String name, System.String password)
        {
            return dao.authenticate(name, password);
        }

        // <summary> Build the user object for the username and load the roles for the database. </summary>
        // <param name="username">The username.</param>
        // <param name="database">The database.</param>
        // <returns>  a User object or null if not found.</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public static User getUserWithDatabaseRoles(System.String username, System.String database)
        {
            System.String[] databases = new System.String[1];
            databases[0] = database;
            return getUser(username, null, ROLE_LOCATION_DATABASE, databases);
        }

        // <summary> Build the user object for the username and load the roles for the application.</summary>
        // <param name="username">The username</param>
        // <param name="app_id">The applicaiton</param>
        // <returns> a User object or null if not found</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public static User getUserWithApplicationRoles(System.String username, System.String app_id)
        {
            System.String[] app_ids = new System.String[1];
            app_ids[0] = app_id;
            return getUser(username, null, ROLE_LOCATION_APPLICATION, app_ids);
        }

        // <summary> Build the user object for the username and load the roles for the application.</summary>
        // <param name="username">The username</param>
        // <param name="app_ids">The applicaiton</param>
        // <returns> a User object or null if not found</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public static User getUserWithApplicationsRoles(System.String username, System.String [] app_ids)
        {
            return getUser(username, null, ROLE_LOCATION_APPLICATION, app_ids);
        }

        // <summary> Build the user object for the username. The client will need to load the roles later.</summary>
        // <param name="username">The user id</param>
        // <returns> a User object or null if not found</returns>
        // <throws>  DaoException  thrown if the persistent layer fails </throws>
        public static User getUserWithoutRoles(System.String username)
        {
            return getUser(username, null, ROLE_LOCATION_NONE, null);
        }

        // <summary> Authenticate the user and return the User object with the database roles if the user is found and authenticated else return null.</summary>
        // <param name="username">The user id</param>
        // <param name="password">The password for the user</param>
        // <param name="database">The database name</param>
        // <returns> a User object or null if not found</returns>
        // <throws>  DaoException thrown if the persistent layer fails </throws>
        public static User getAuthenticatedUserWithDatabaseRoles(System.String username, System.String password, System.String database)
        {
            System.String[] databases = new System.String[1];
            databases[0] = database;
            return getUser(username, password, ROLE_LOCATION_DATABASE, databases);
        }

        // <summary> Authenticate the user and return the User object with the application roles if the user is found and authenticated else return null.</summary>
        // <param name="username">The user id</param>
        // <param name="password">The password for the user</param>
        // <param name="app_id">The application id for the client of the method</param>
        // <returns> a User object or null if not found</returns>
        // <throws>  DaoException thrown if the persistent layer fails </throws>
        public static User getAuthenticatedUserWithApplicationRoles(System.String username, System.String password, System.String app_id)
        {
            System.String[] app_ids = new System.String[1];
            app_ids[0] = app_id;
            return getUser(username, password, ROLE_LOCATION_APPLICATION, app_ids);
        }

        // <summary> Authenticate the user and return the User object with the application roles if the user is found and authenticated else return null.</summary>
        // <param name="username">The user id</param>
        // <param name="password">The password for the user</param>
        // <param name="app_id">The application id for the client of the method</param>
        // <returns> a User object or null if not found</returns>
        // <throws>  DaoException thrown if the persistent layer fails </throws>
        public static User getAuthenticatedUserWithApplicationsRoles(System.String username, System.String password, System.String [] app_ids)
        {
            return getUser(username, password, ROLE_LOCATION_APPLICATION, app_ids);
        }

        // <summary> Authenticate the user and return the User object without roles if the user is found and authenticated else return null.</summary>
        // <param name="username">The user id</param>
        // <param name="password">The password for the user</param>
        // <returns> a User object or null if not found</returns>
        // <throws>  DaoException thrown if the persistent layer fails </throws>
        public static User getAuthenticatedUserWithoutRoles(System.String username, System.String password)
        {
            return getUser(username, password, ROLE_LOCATION_NONE, null);
        }

        // <summary> Search for a user in the persistent storage based on user id. If the user id is not found, 
        // return null else return the User object matching the user id. If the password is not null,
        // authenticate the user; otherwise, find the user in the LDAP tree
        // 
        // Also, load the roles for a user if locationType and location are not null.</summary>
        // <param name="username">The user id.</param>
        // <param name="password">The user password or null</param>
        // <param name="location_type">The way we will load the roles. Use ROLE_LOCATION_DATABASE or ROLE_LOCATION_APPLICATION</param>
        // <param name="location">The location where the user roles will be loaded from</param>
        // <returns> a User object or null if not found or failed authentication</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        private static User getUser(System.String username, System.String password, System.String location_type, System.String[] locations)
        {
            User user = null;
            UserService user_service = null;
            DAOException problem = null;
            String passwordFile = System.String.Empty;
            try
            {
    
                user_service = new UserService();
                if (password == null)
                {
                    user = user_service.findUserByUserId(username);
                }
                else
                {
                    user = user_service.authenticate(username, password);
                }

                if (user != null)
                {
                    if (ROLE_LOCATION_DATABASE.Equals(location_type))
                    {
                        user_service.loadRolesForDatabase(user, locations[0]);
                    }
                    else if (ROLE_LOCATION_APPLICATION.Equals(location_type))
                    {
                        NAICLDAPSec.HashSet<string> appIds = new NAICLDAPSec.HashSet<string>();
                        for(int x = 0; x < locations.Length; x++)
                        {
                            if (locations[x] != null)
                            {
                                appIds.Add(locations[x]);
                            }
                        }
                        user_service.loadRolesForApplications(user, appIds);
                    }
                    else if (!ROLE_LOCATION_NONE.Equals(location_type))
                    {
                        //invalid location
                        throw new DAOException("Invalid role location. Use ROLE_LOCATION_DATABASE or ROLE_LOCATION_APPLICATION.");
                    }
                }
                // else don't load the roles for the user.
            }
            catch (DAOException e)
            {
                problem = e;
            }
            finally
            {
                if (user_service != null)
                {
                    try
                    {
                        user_service.close();
                    }
                    catch (DAOException e)
                    {
                        if (problem == null)
                        {
                            problem = e;
                        }
                    }
                }
            }
            if (problem != null)
            {
                throw problem;
            }
            return user;
        }
    }  // end of UserService
}
