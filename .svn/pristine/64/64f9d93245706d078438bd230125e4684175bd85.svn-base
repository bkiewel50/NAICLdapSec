using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using Novell.Directory.Ldap;

using Novell.Directory.Ldap.Utilclass;

namespace NAICLdapSec
{
    // <summary> Provides the persistence implementation for a User on LDAP.</summary>
    public class UserDao : LDAPDao
    {

        private LDAPConnection connection;

        // <summary> Build the list of LDAP attributes for a person entry that match a User.</summary>
        // <returns> A String array.</returns>
        virtual protected internal System.String[] UserAttrList
        {
            get
            {
                return USER_ATTR_LIST;
            }
        }

        
        virtual protected internal System.String[] AliasAttrList
        {
            get
            {
                return DBALIAS_ATTR_LIST;
            }
        }
        //private const System.String USER_NAME_ATTR = "uid";
        private const System.String USER_NAME_ATTR = "uid";
        private const System.String USER_CN_ATTR = "cn";
        private const System.String COMMON_NAME_ATTR = "uid";
        private const System.String FIRST_NAME_ATTR = "givenname";
        private const System.String MIDDLE_NAME_ATTR = "middlename";
        private const System.String LAST_NAME_ATTR = "sn";
        private const System.String EMAIL_ATTR = "mail";
        private const System.String EMAIL2_ATTR = "mail2";
        private const System.String EMAIL3_ATTR = "mail3";
        private const System.String EMAIL4_ATTR = "mail4";
        private const System.String DEPARTMENT_ATTR = "departmentnumber";
        private const System.String JOB_TITLE_ATTR = "jobtitle";
        private const System.String STREET_ATTR = "street";
        private const System.String CITY_ATTR = "l";
        private const System.String STATE_ATTR = "st";
        private const System.String ORGANIZATIONAL_UNIT_ATTR = "ou";
        private const System.String ZIP_CODE_ATTR = "postalcode";
        private const System.String COUNTRY_ATTR = "c";
        private const System.String PHONE_ATTR = "telephonenumber";
        private const System.String PHONE_EXT_ATTR = "ext";
        private const System.String FAX_ATTR = "facsimiletelephonenumber";
        private const System.String ACTIVATED_ATTR = "orclisenabled";
        private const System.String CONVERTED_ATTR = "convertedtoldap";
        private const System.String EXPIRED_ATTR = "obpasswordchangeflag";
        //private const System.String DATABASES_ATTR = "dblist";
        private const System.String SALUTATION_ATTR = "salutation";

        private const System.String DATABASE_ALIAS_ATTR = "dbalias";
        /* 12-20-2008 - GAM Added employeeType and UserAccountControl per eMail from RSchrum */
        private const System.String EMPLOYEE_TYPE_ATTR = "employeeType";
        private const System.String USERACCOUNTCONTROL_ATTR = "obuseraccountcontrol";

        /* 01-06-2009 - GAM Added webService and obPasswordCreationDate per eMail from DTalbot */
        private const System.String WEBSERVICE_ATTR = "webservice";
        private const System.String PASSWORDCREATIONDATE_ATTR = "obpasswordcreationdate";

        /* 01-13-2009 - GAM Added ActiveStartDate, ActiveEndDate and IsEnabled per eMail from RSchrum */
        private const System.String ACTIVESTARTDATE_ATTR = "orclactivestartdate";
        private const System.String ACTIVEENDDATE_ATTR = "orclactiveenddate";
        private const System.String ISENABLED_ATTR = "orclisenabled";
        

        private const System.String SPL_CUST_ID_ATTR = "custid";

        // dynamic group attributes used to fill-in SPL roles. 
        // TODO: These should go away when dynamic groups work.
        // they are mapped into the dataMap attribute of a user.
        private const System.String SPL_ROLE_PDB_USER_ATTR = "pdbuserrole";
        private const System.String SPL_ROLE_INDUSTRY_ADMIN_ATTR = "industryroleadmin";
        private const System.String SPL_ROLE_PDB_USER_NAME = "pdb_user_pr";
        private const System.String SPL_ROLE_INDUSTRY_ADMIN_NAME = "industry_admin_pr";


        // dynamic group attributes used to fill-in OAM roles.
        // They are mapped into the dataMap attribute of a user.
        private const System.String ROLE_DP_COORDINATOR_ATTR = "dpRoleAdmin";
        private const System.String ROLE_HELP_DESK_ADMIN_ATTR = "helpdeskRoleAdmin";
        private const System.String ROLE_SECURITY_ADMIN_ATTR = "secRoleAdmin";
        private const System.String ROLE_DP_COORDINATOR_NAME = "dp_coordinators";
        private const System.String ROLE_HELP_DESK_ADMIN_NAME = "help_desk";
        private const System.String ROLE_SECURITY_ADMIN_NAME = "security_admin";

        private const System.String ROLE_NAME_ATTR = "cn";
        private const System.String OLD_ROLE_NAME_ATTR = "group2";
        private const System.String BUSINESS_CATEGORY_ATTR = "businesscategory";
        private const System.String ROLE_USER_MEMBERSHIP_ATTR = "uniquemember";
        private static readonly System.String[] USER_ATTR_LIST = new System.String[] { USER_NAME_ATTR, USER_CN_ATTR, FIRST_NAME_ATTR, MIDDLE_NAME_ATTR, LAST_NAME_ATTR, EMAIL_ATTR, EMAIL2_ATTR, EMAIL3_ATTR, EMAIL4_ATTR, DEPARTMENT_ATTR, JOB_TITLE_ATTR, STREET_ATTR, CITY_ATTR, STATE_ATTR, ORGANIZATIONAL_UNIT_ATTR, ZIP_CODE_ATTR, COUNTRY_ATTR, PHONE_ATTR, PHONE_EXT_ATTR, FAX_ATTR, ACTIVATED_ATTR, CONVERTED_ATTR, EXPIRED_ATTR, SALUTATION_ATTR, EMPLOYEE_TYPE_ATTR, USERACCOUNTCONTROL_ATTR, WEBSERVICE_ATTR, PASSWORDCREATIONDATE_ATTR, ACTIVESTARTDATE_ATTR, ACTIVEENDDATE_ATTR, SPL_CUST_ID_ATTR, SPL_ROLE_PDB_USER_ATTR, SPL_ROLE_INDUSTRY_ADMIN_ATTR, ROLE_DP_COORDINATOR_ATTR, ROLE_HELP_DESK_ADMIN_ATTR, ROLE_SECURITY_ADMIN_ATTR };
        private static readonly System.String[] DBALIAS_ATTR_LIST = new System.String[] { DATABASE_ALIAS_ATTR };
        private const System.String ACTIVE_VALUE = "ENABLED";
        private const System.String CONVERTED_TRUE_VALUE = "TRUE";
        private const System.String EXPIRED_TRUE_VALUE = "TRUE";
        private const System.String DYNAMIC_GROUP_TRUE_VALUE = "true";

        private const System.String WEBSERVICE_VALUE = "TRUE";
        private const System.String USERACCOUNTCONTROL_VALUE = "ACTIVATED";
        private const System.String ISENABLED_TRUE_VALUE = "ENABLED";

        // <summary> Create a new UserDao object referencing the LDAP connection. The reference is kept.</summary>
        // <param name="connection">The LDAP connection object.</param>
        public UserDao(LDAPConnection connection) : base(connection)
        {
            this.connection = connection;
        }

        // <summary> Search for a user in the LDAP tree by user id. If the user is found, create a new User object with the correct values
        // else return null.</summary>
        // <param name="name">The user id for the User</param>
        // <returns> a User object if the user is found else null.</returns>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        public virtual User findUserByUserId(System.String value)
        {
            //return (User)findObject(USERS_ROOT_DN, "uid=" + name, UserAttrList);
            //return (User)findObject(USERS_ROOT_DN, "uid=" + name, UserAttrList);
            //Since the UID and CN have been flipped in NetIQ... we need to change our
            //filter to return what the app was originally getting..
            return (User)findObject(USERS_ROOT_DN, "cn=" + value, UserAttrList);
        }

        // <summary> Search for a user in the LDAP tree by cn. If the user is found, create a new User object with the correct values
        // else return null.</summary>
        // <param name="name">The common name for the User</param>
        // <returns> a User object if the user is found else null.</returns>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        public virtual User findUserByCn(System.String value)
        {
            //return (User)findObject(USERS_ROOT_DN, "cn=" + cn, UserAttrList);
            //Since the UID and CN have been flipped in NetIQ... we need to change our
            //filter to return what the app was originally getting..
            return (User)findObject(USERS_ROOT_DN, COMMON_NAME_ATTR + value, UserAttrList);
        }

        // <summary> Search for a user in the LDAP tree by distinguished name. If the user is found, create a new User object with 
        // the correct values else return null.</summary>
        // <param name="dn">The distinguished name for the User</param>
        // <returns> a User object if the user is found else null.</returns>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        public virtual User findUserByDistinguishedName(System.String dn)
        {
            return (User)findObject(dn, "objectClass=person", UserAttrList);
        }

        // <summary> Load the roles for a user and an application into the provided User object.</summary>
        // <param name="user">The User object.</param>
        // <param name="application">The name of the application.</param>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        public virtual void loadRolesForApplications(User user, String [] appids)
        {
            loadRoles(user, appids, false, null);
        }

        // <summary> Load the roles for a user and an application into the provided User object. Sorting in the 
        // order by the given Comparator object.</summary>
        // <param name="user">The User object.</param>
        // <param name="application">The name of the application.</param>
        // <param name="c">The Comparator object.</param>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        public virtual void loadRolesForApplications(User user, String[] appids, System.Collections.IComparer c)
        {
            loadRoles(user, appids, false, c);
        }

        // <summary> Load the roles for a user and a database into the provided User object.</summary>
        // <param name="user">The User object.</param>
        // <param name="database">The database.</param>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        public virtual void loadRolesForDatabase(User user, System.String database)
        {
            String[] appids = new String[1];
            appids[0] = database;
            loadRoles(user, appids, true, null);
            if (user.Roles.Count == 0)
            {
                System.String dbalias = (System.String)findItem(DATABASES_ROOT_DN, USER_NAME_ATTR + database, AliasAttrList);
                if (dbalias.Trim().Length > 0)
                {
                    appids[0] = database;
                    loadRoles(user, appids, true, null);
                }
            }
        }

        // <summary> Load the roles for a user and a database into the provided User object. Sorting in the order given
        // by the Comparator object.</summary>
        // <param name="user">The User object.</param>
        // <param name="database">The database.</param>
        // <param name="c">The Comparator object.</param>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        public virtual void loadRolesForDatabase(User user, System.String database, System.Collections.IComparer c)
        {
            String[] appids = new String[1];
            appids[0] = database;
            loadRoles(user, appids, true, c);
            if (user.Roles.Count == 0)
            {
                System.String dbalias = (System.String)findItem(DATABASES_ROOT_DN, USER_NAME_ATTR + database, AliasAttrList);
                if (dbalias.Trim().Length > 0)
                {
                    appids[0] = dbalias;
                    loadRoles(user, appids, true, c);
                }
            }

        }

        // <summary> Find a set of users starting at a location in the LDAP tree that match 
        // a LDAP filter.</summary>
        // <param name="base_dn">The starting location in the LDAP tree.</param>
        // <param name="filter">The LDAP search condition.</param>
        // <returns> A Set</returns>
        // <throws>  DaoException thrown if the persistent layer fails. </throws>
        public virtual Collection<System.Object> findUsers(System.String base_dn, System.String filter)
        {
            return (Collection<System.Object>)findObjects(NetIQPeriodFix.fixVirginIslandsOU(base_dn), NetIQPeriodFix.fixVirginIslandsOU(filter), UserAttrList);
        }
        
        // <summary> Authenticate a user using the provided user id and password. If the user and 
        // password are valid, returns a new User object.</summary>
        // <param name="name">The user id.</param>
        // <param name="password">The password for the user.</param>
        // <returns> a new User object or null if the user id or password is incorrect.</returns>
        // * Uses WS in OAM to authenticate user.
        public virtual User authenticate(System.String name, System.String password)
        {
            User result = findUserByUserId(name);
            string realm = System.String.Empty;
            //if (result != null)
            //{
            //    //DirectoryEntry context = connection.getContext();
            //    //DirectoryEntry context = new DirectoryEntry();
            //    // set context path back....
            //    realm = result.Realm;
            //    //if (cntxpath.Equals("ldaps://idmr-test.naic.org:636/ou=users,o=data"))
            //    //{
            //    //    realm = "dvlp";
            //    //}
            //    //if (cntxpath.Equals("ldaps://idmr-qa.naic.org:636/ou=users,o=data"))
            //    //{
            //    //    realm = "qa";
            //    //}
            //    //if (cntxpath.Equals("ldaps://idmr.naic.org:636/ou=users,o=data"))
            //    //{
            //    //    realm = "prod";
            //    //}
            //    //Console.WriteLine("Connection ctx: " + cntxpath);
            //    //WSAuthenticate authenticate = new WSAuthenticate();

            //    //bool isAuthorized = authenticate.isAuthorized(name, password, realm);
            //    //bool isAuthorized = true;
            //    //if (!isAuthorized)
            //    //{
            //    //    result = null;
            //    //}
            //}
            return result;
        }

        // <summary> Create a new User from the LDAP data.</summary>
        // <param name="dn">The distinguishedName for the user.</param>
        // <param name="attrs">The attributes read from LDAP</param>
        // <returns> a new User object.</returns>
        // <throws>  NamingException JNDI exception occurred. </throws>
        protected internal override System.Object createObjectForFind(System.String dn, ListDictionary attrs)
        {
            User retval = new User();
            mapObjectAttributes(dn, attrs, retval);
            return retval;
        }

        // <summary> In order to support inheritance, this method sets all the attributes of user based on the LDAP values.</summary>
        // <param name="dn">The distinguishedName for the user.</param>
        // <param name="attrs">The attributes read from LDAP</param>
        // <param name="user">The object to set the values on</param>
        // <throws>  NamingException JNDI exception occurred. </throws>
        protected internal virtual void mapObjectAttributes(System.String dn, ListDictionary  attrs, User user)
        {
            user.DistinguishedName = dn;
            user.Realm = readAttribute("realm", attrs);
            user.Cn = readAttribute(USER_NAME_ATTR, attrs);
            user.UID = readAttribute(USER_CN_ATTR, attrs);
            //user.UserName = readAttribute(USER_NAME_ATTR, attrs);
            user.FirstName = readAttribute(FIRST_NAME_ATTR, attrs);
            user.MiddleName = readAttribute(MIDDLE_NAME_ATTR, attrs);
            user.LastName = readAttribute(LAST_NAME_ATTR, attrs);
            user.EmailAddress = readAttribute(EMAIL_ATTR, attrs);
            user.EmailAddress2 = readAttribute(EMAIL2_ATTR, attrs);
            user.EmailAddress3 = readAttribute(EMAIL3_ATTR, attrs);
            user.EmailAddress4 = readAttribute(EMAIL4_ATTR, attrs);
            user.Department = readAttribute(DEPARTMENT_ATTR, attrs);
            user.JobTitle = readAttribute(JOB_TITLE_ATTR, attrs);
            user.Street = readAttributeStringValues(STREET_ATTR, attrs);
            user.City = readAttribute(CITY_ATTR, attrs);
            user.State = readAttribute(STATE_ATTR, attrs);
            user.ZipCode = readAttribute(ZIP_CODE_ATTR, attrs);
            user.Country = readAttribute(COUNTRY_ATTR, attrs);
            user.PhoneNumber = readAttribute(PHONE_ATTR, attrs);
            user.PhoneNumberExtension = readAttribute(PHONE_EXT_ATTR, attrs);
            user.FaxNumber = readAttribute(FAX_ATTR, attrs);
            //if (user.UserName.Equals("NJ824"))
            //{
            //    Console.WriteLine("nj824 " + readAttribute(ACTIVATED_ATTR, attrs).ToUpper());
            //    Console.WriteLine("nj824 " + readAttribute(ISENABLED_ATTR, attrs).ToUpper());
            //}
            user.Active = ACTIVE_VALUE.ToUpper().Equals(readAttribute(ACTIVATED_ATTR, attrs).ToUpper());
            user.Converted = CONVERTED_TRUE_VALUE.ToUpper().Equals(readAttribute(CONVERTED_ATTR, attrs).ToUpper());
            user.Expired = EXPIRED_TRUE_VALUE.ToUpper().Equals(readAttribute(EXPIRED_ATTR, attrs).ToUpper());
            //user.Databases = readAttributeStringValues(DATABASES_ATTR, attrs);
            user.Salutation = readAttribute(SALUTATION_ATTR, attrs);

             /* 12-20-2008 - GAM Added employeeType and UserAccountControl per eMail from RSchrum */
            user.EmployeeType = readAttribute(EMPLOYEE_TYPE_ATTR, attrs);
            //Console.WriteLine("readAttribute(USERACCOUNTCONTROL_ATTR, attrs) " + readAttribute(USERACCOUNTCONTROL_ATTR, attrs));
            user.UserAccountControl = USERACCOUNTCONTROL_VALUE.ToUpper().Equals(readAttribute(USERACCOUNTCONTROL_ATTR, attrs).ToUpper());  
            
            /* 01-06-2009 - GAM Added webService and obPasswordCreationDate per eMail from DTalbot */
            user.WebService = WEBSERVICE_VALUE.ToUpper().Equals(readAttribute(WEBSERVICE_ATTR, attrs).ToUpper());
            //Console.WriteLine("readAttribute(PASSWORDCREATIONDATE_ATTR, attrs) " + readAttribute(PASSWORDCREATIONDATE_ATTR, attrs) + "  " + readAttribute(PASSWORDCREATIONDATE_ATTR, attrs).Trim().Length);
            //if (readAttribute(PASSWORDCREATIONDATE_ATTR, attrs) == null) {
            if ((readAttribute(PASSWORDCREATIONDATE_ATTR, attrs) == null) || (readAttribute(PASSWORDCREATIONDATE_ATTR, attrs).Trim().Length < 5)) {
                   //user.PasswordCreationDate = null;
            }
            else {
                string pwdDate = System.String.Empty;
                pwdDate = readAttribute(PASSWORDCREATIONDATE_ATTR, attrs);
                if (pwdDate.Trim().Length > 5)
                {
                    user.PasswordCreationDate = DateTime.Parse(readAttribute(PASSWORDCREATIONDATE_ATTR, attrs));
                }
            }
            /* 01-13-2009 - GAM Added ActiveStartDate, ActiveEndDate and IsEnabled per eMail from RSchrum */
            //Console.WriteLine("readAttribute(ISENABLED_ATTR, attrs) " + readAttribute(ISENABLED_ATTR, attrs));
            user.IsEnabled = ISENABLED_TRUE_VALUE.ToUpper().Equals(readAttribute(ISENABLED_ATTR, attrs).ToUpper());
            user.ActiveStartDate = readAttribute(ACTIVESTARTDATE_ATTR, attrs);
            user.ActiveEndDate = readAttribute(ACTIVEENDDATE_ATTR, attrs);
            
            
            user.SplCustomerId = readAttribute(SPL_CUST_ID_ATTR, attrs);

            
            System.Collections.Hashtable data_map = new System.Collections.Hashtable();
            // Group attributes used to fill-in DP coordinator, help desk
            // admin and security admin roles.
            // They are mapped into the dataMap attribute of a user.
            System.String dp_coord_indicator = readAttribute(ROLE_DP_COORDINATOR_ATTR, attrs);
            data_map[ROLE_DP_COORDINATOR_ATTR] = dp_coord_indicator;

            System.String help_desk_indicator = readAttribute(ROLE_HELP_DESK_ADMIN_ATTR, attrs);
            data_map[ROLE_HELP_DESK_ADMIN_ATTR] = help_desk_indicator;

            System.String sec_admin_indicator = readAttribute(ROLE_SECURITY_ADMIN_ATTR, attrs);
            data_map[ROLE_SECURITY_ADMIN_ATTR] = sec_admin_indicator;

            // dynamic group attributes used to fill-in SPL roles.
            // These should go away when dynamic groups work.
            // they are mapped into the dataMap attribute of a user.
            System.String spl_user_role = readAttribute(SPL_ROLE_PDB_USER_ATTR, attrs);
            data_map[SPL_ROLE_PDB_USER_ATTR] = spl_user_role;
            System.String spl_industry_admin_role = readAttribute(SPL_ROLE_INDUSTRY_ADMIN_ATTR, attrs);
            data_map[SPL_ROLE_INDUSTRY_ADMIN_ATTR] = spl_industry_admin_role;

            user.DataMap = data_map;

            user.Roles = null;
        }

        //public virtual System.String getRoleFromString(String roleName)
        //{
        //    System.String sStartSearch = "cn=";
        //    System.String sEndSearch = ",ou=";
        //    System.String sRet = System.String.Empty;
        //    int iStartPos = roleName.Trim().IndexOf(sStartSearch) + sStartSearch.Length;
        //    int iEndPos = roleName.Trim().IndexOf(sEndSearch) - sEndSearch.Length;
        //    if ((iStartPos >= sStartSearch.Length) && (iEndPos > iStartPos))
        //    {
        //        sRet = roleName.Substring(iStartPos, iEndPos + 1);
        //    }
        //    else
        //    {
        //        sRet = roleName;
        //    }

        //    return sRet;
        //}

        private void loadRoles(User user, String[] app_db_names, bool for_database, System.Collections.IComparer c)
        {
            if (user == null)
	        {
	            throw new DAOException("User can not be null.");
	        }
            else if (app_db_names == null || app_db_names.Length == 0)
	        {
	            throw new DAOException("appDbNames can not be null or empty.");
	        }
            assertConnected();
            LdapConnection conn = connection.getConnection();
            Collection<Role> results = new Collection<Role>();

            DAOException error = null;

            String dn = System.String.Empty;
            String cn = System.String.Empty;
            String applicationId = System.String.Empty;
            try
            {
                System.String starting_dn = null;
                System.String filter = null;

                Hashtable temp_roles = new Hashtable();
                string sKey = System.String.Empty;

                if (for_database)
                {
                    //starting_dn = "ou=" + app_db_names[0] + "," + DATABASES_ROOT_DN;
                    starting_dn = DATABASES_BASE_DN + "=" + app_db_names[0] + "," + DATABASES_ROOT_DN;
                    filter = ROLE_USER_MEMBERSHIP_ATTR + "=" + user.DistinguishedName;
                }
                else
                {
                    for (int x = 0; x < app_db_names.Length; x++)
                    {
                        if ((app_db_names[x].ToLower().StartsWith("fdra")) || (app_db_names[x].ToLower().StartsWith("svo")))
                        {
                            //starting_dn = "dc=naic,dc=org";
                            starting_dn = "ou=Applications,ou=groups,o=data";
                            starting_dn = APPLICATIONS_GROUP_DN;
                        }
                    }
                    if (starting_dn == null)
                    {
                        starting_dn = "ou=Applications," + GROUPS_ROOT_DN;
                    }

                    if (app_db_names.Length == 1)
                    {
                        filter = "(&(" + ROLE_USER_MEMBERSHIP_ATTR + "=" + user.DistinguishedName + ")(businesscategory=" + app_db_names[0] + "))";
                    }
                    else
                    {
                        filter = "(&(" + ROLE_USER_MEMBERSHIP_ATTR + "=" + user.DistinguishedName + ")(|";
                        for (int x = 0; x < app_db_names.Length; x++)
                        {
                            filter = filter + "(businesscategory=" + app_db_names[x] + ")";
                        }
                        filter = filter + "))";
                    }
                }

                LdapSearchResults lsc = conn.Search(starting_dn, LdapConnection.SCOPE_SUB, filter, new System.String[] { ROLE_NAME_ATTR, OLD_ROLE_NAME_ATTR, BUSINESS_CATEGORY_ATTR }, false);
                while (lsc.hasMore())
                {
                    LdapEntry nextEntry = null;
                    try
                    {
                        nextEntry = lsc.next();
                    }
                    catch (LdapException e)
                    {
                        Console.WriteLine("Error: " + e.LdapErrorMessage);
                        // Exception is thrown, go for next entry
                        continue;
                    }
                    //Console.WriteLine("\n" + "DN: " + nextEntry.DN);
                    starting_dn = nextEntry.DN;
                    dn = starting_dn;
                    LdapAttributeSet attributeSet = nextEntry.getAttributeSet();
                    System.Collections.IEnumerator ienum = attributeSet.GetEnumerator();
                    while (ienum.MoveNext())
                    {
                        LdapAttribute attribute = (LdapAttribute)ienum.Current;
                        string attributeName = attribute.Name;
                        string attributeVal = attribute.StringValue;

                        /**** This should go away once all roles are compared case insensitively ****/
                        if (attributeName.Equals(OLD_ROLE_NAME_ATTR))
                        {
                            cn = attributeVal;
                        }
                        /**** End of section ****/
                        if (attributeName.Equals("cn"))
                        {
                            cn = attributeVal;
                        }
                        if (attributeName.Equals(BUSINESS_CATEGORY_ATTR))
                        {
                            applicationId = attributeVal;
                        }
                        if ((dn.Length > 0) && (cn.Length > 0))
                        {
                            //Console.WriteLine("\n" + "dn: " + getRoleFromString(dn)  + "  cn: " + cn);
                            if (for_database)
                            {
                                results.Add(new Role(dn, cn, app_db_names[0], null));
                                dn = System.String.Empty;
                                cn = System.String.Empty;
                            }
                            else
                            {
                                results.Add(new Role(dn, cn, null, applicationId.ToUpper()));
                                dn = System.String.Empty;
                                cn = System.String.Empty;
                            }
                        }


                    }
                }
                user.Roles = results;
            }
            catch (InvalidOperationException ie)
            {
                Console.WriteLine("InvalidOperationException in UserDao::loadRoles() " + ie.Message);
            }
            catch (NotSupportedException ne)
            {
                Console.WriteLine("NotSupportedException in UserDao::loadRoles() " + ne.Message);
            }
            catch (System.Exception e)
            {
                error = new DAOException("Failure trying to load roles for the user, " + user.DistinguishedName + ".", e);
            }
            finally
            {
                try
                {
                    if (results != null)
                    {
  
                    }
                }
                catch (System.Exception e)
                {
                    if (error == null)
                    {
                        error = new DAOException("Failure trying to find a user.", e);
                    }
                    // else we already have a exception in play which is the real problem.
                }
                if (error != null)
                {
                    throw new DAOException("Exception thrown in UserDao::loadRoles."); ;
                }
            }
        }
    }
}
