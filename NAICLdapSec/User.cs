using System;
using System.DirectoryServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Text;

namespace NAICLdapSec
{
    // <summary> This class represents a User. The primay key attribute is the distinguishedName of the User. A User
    // belongs to a set of a Roles.
    // <p>
    // A fully constructed User object should have the following invariants: (Not enforced by the code.)
    // <ol>
    // <li>The distinguishedName and name are not null.</li>
    // <li>The distinguishedName is unqiue for all Users in existance.</li>
    // <li>The userName is unqiue for all Users in existance.</li>
    // </ol>
    // <p>
    // The class fully implements the Object interface and can be used in Collection classes. It is
    // sorted by and compared on the distinguishedName attribute only.
    // </summary>
    [Serializable]
    public class User : System.IComparable, System.ICloneable, AttributeComparable
    {
        
        // <returns> the value of the distinguishedName for the User.</returns>
        // <summary> Set the value of the distinguishedName for the User. This value is the primary key value for the
        // object.</summary>
        // <param name="distinguishedName">a String and not null.</param>
        virtual public System.String DistinguishedName
        {
            get
            {
                return distinguishedName;
            }

            set
            {
                
                this.distinguishedName = value;
            }

        }

        // ******* deprecated in version 2.x *********
        //// <summary> Get the value of the userName for the User.</summary>
        //// <returns> a String and not null.</returns>
        //// <summary> Set the value of the userName for the User. The userName should be unique for all users.</summary>
        //// <param name="userName">a String and not null.</param>
        //virtual public System.String UserName
        //{
        //    get
        //    {
        //        return userName;
        //    }

        //    set
        //    {
        //        this.userName = value;
        //    }

        //}
        // ******* deprecated in version 2.x *********


        // <summary> Get the value of teh cn for the User.</summary>
        // <returns> a String and not null.</returns>
        // <summary> Set the value of the cn for the User.  The cn should be unique for all users.</summary>
        // <param name="cn">a String and not null.</param>
        virtual public System.String Cn
        {
            get
            {
                return cn;
            }

            set
            {
                this.cn = value;
            }
        }
 
        // <summary> Get the first name of the User.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the first name for the User.</summary>
        // <param name="firstName">a String or null</param>
        virtual public System.String FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                this.firstName = value;
            }

        }
  
        // <summary> Get the middle name of the User.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the middle name for the User.</summary>
        // <param name="middleName">a String or null</param>
        virtual public System.String MiddleName
        {
            get
            {
                return middleName;
            }

            set
            {
                this.middleName = value;
            }

        }
 
        // <summary> Get the last name of the User.</summary>
        // <returns> a String</returns>
        // <summary> Set the last name for the User.</summary>
        // <param name="lastName">a String and not null</param>
        virtual public System.String LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                this.lastName = value;
            }

        }

        // <summary> Get the e-mail address for the User.</summary>
        // <returns> A String or null.</returns>
        // <summary> Set the e-mail address of the User.</summary>
        // <param name="emailAddress">a String or null.</param>
        virtual public System.String EmailAddress
        {
            get
            {
                return emailAddress;
            }

            set
            {
                this.emailAddress = value;
            }

        }

        // <summary> Get the e-mail address for the User.</summary>
        // <returns> A String or null.</returns>
        // <summary> Set the e-mail address of the User.</summary>
        // <param name="emailAddress">a String or null.</param>
        virtual public System.String EmailAddress2
        {
            get
            {
                return emailAddress2;
            }

            set
            {
                this.emailAddress2 = value;
            }

        }

        // <summary> Get the e-mail address for the User.</summary>
        // <returns> A String or null.</returns>
        // <summary> Set the e-mail address of the User.</summary>
        // <param name="emailAddress">a String or null.</param>
        virtual public System.String EmailAddress3
        {
            get
            {
                return emailAddress3;
            }

            set
            {
                this.emailAddress3 = value;
            }

        }

        // <summary> Get the e-mail address for the User.</summary>
        // <returns> A String or null.</returns>
        // <summary> Set the e-mail address of the User.</summary>
        // <param name="emailAddress">a String or null.</param>
        virtual public System.String EmailAddress4
        {
            get
            {
                return emailAddress4;
            }

            set
            {
                this.emailAddress4 = value;
            }

        }

        // <summary> Get the department for the User.</summary>
        // <returns> A String or null.</returns>
        // <summary> Set the department of the User.</summary>
        // <param name="department">A String or null.</param>
        virtual public System.String Department
        {
            get
            {
                return department;
            }

            set
            {
                this.department = value;
            }

        }

        // <summary> Get the job title of the User.</summary>
        // <returns> A String or null.</returns>
        // <summary> Set the job title for the User.</summary>
        // <param name="jobTitle">A String or null.</param>
        virtual public System.String JobTitle
        {
            get
            {
                return jobTitle;
            }

            set
            {
                this.jobTitle = value;
            }

        }
 
        // <summary> Get the street address for the User.</summary>
        // <returns> A List of Strings.</returns>
        // <summary> Set the street address lines for the User.</summary>
        // <param name="street">A List of Strings.</param>
        virtual public System.Collections.IList Street
        {
            get
            {
                return street;
            }

            set
            {
                if (value == null)
                {
                    value = new System.Collections.ArrayList();
                }
                else
                {
                    this.street = value;
                }
            }
        }
 
        // <summary> Get the city for the User.</summary>
        // <returns> A String or null.</returns>
        // <summary> Set the City for the User.</summary>
        // <param name="city">A String or null.</param>
        virtual public System.String City
        {
            get
            {
                return city;
            }

            set
            {
                this.city = value;
            }
        }
          
        // <summary> Get the state for the Address of the User account or null if the user doesn't belong to a State.</summary>
        // <returns> a String or null</returns>
        // <summary> Set the state for the Address for the user account. Not all users belong to a state.</summary>
        // <param name="state">a String or null.</param>
        virtual public System.String State
        {
            get
            {
                return state;
            }

            set
            {
                this.state = value;
            }
        }
     
        // <summary> Get the zip code for a User.</summary>
        // <returns> A String or null.</returns>
        // <summary> Set the Zip code for the User.</summary>
        // <param name="zipCode">a String or null.</param>
        virtual public System.String ZipCode
        {
            get
            {
                return zipCode;
            }

            set
            {
                this.zipCode = value;
            }

        }
   
        // <summary> Get the country for a User.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the country for a User.</summary>
        // <param name="country">a String or null.</param>
        virtual public System.String Country
        {
            get
            {
                return country;
            }

            set
            {
                this.country = value;
            }

        }
    
        // <summary> Get the phone number for a User.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the phone number for a User.</summary>
        // <param name="phoneNumber">a String or null.</param>
        virtual public System.String PhoneNumber
        {
            get
            {
                return phoneNumber;
            }

            set
            {
                this.phoneNumber = value;
            }

        }
        
        // <summary> Get the phone number extension for a User.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the phone number extension for a User.</summary>
        // <param name="phoneNumberExtension">a String or null.</param>
        virtual public System.String PhoneNumberExtension
        {
            get
            {
                return phoneNumberExtension;
            }

            set
            {
                this.phoneNumberExtension = value;
            }

        }
       
        // <summary> Get the FAX number for a User.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the FAX number for a User.</summary>
        // <param name="faxNumber">a String or null.</param>
        virtual public System.String FaxNumber
        {
            get
            {
                return faxNumber;
            }

            set
            {
                this.faxNumber = value;
            }

        }
       
        // <summary> Returns if the User account is expired.</summary>
        // <returns> True if expired else false.</returns>
        // <summary> Set the User account expired flag.</summary>
        // <param name="expired">The expired flag.</param>
        virtual public bool Expired
        {
            get
            {
                return expired;
            }

            set
            {
                this.expired = value;
            }

        }

        // <summary> Get the zip code for a User.</summary>
        // <returns> A String or null.</returns>
        // <summary> Set the Zip code for the User.</summary>
        // <param name="zipCode">a String or null.</param>
        virtual public System.String UID
        {
            get
            {
                return uid;
            }

            set
            {
                this.uid = value;
            }

        }

        //// <summary> Get the list of databases the User account is on.</summary>
        //// <returns> A List and not null.</returns>
        //// <summary> Set the list of databases the User account is on. If the databases is null, set the value to an Empty list.</summary>
        //// <param name="databases">A List.</param>
        //virtual public System.Collections.IList Databases
        //{
        //    get
        //    {
        //        return databases;
        //    }

        //    set
        //    {
        //        if (value == null)
        //        {
        //            this.databases = new System.Collections.ArrayList();
        //        }
        //        else
        //        {
        //            this.databases = value;
        //        }
        //    }

        //}
        
        // <summary> Get if the User account is active.</summary>
        // <returns> true if active.</returns>
        // <summary> Set the User account is active.</summary>
        // <param name="active">true for active</param>
        virtual public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                this.active = value;
            }

        }
 
        // <summary> Get if the user is converted to using LDAP.</summary>
        // <returns> true if converted.</returns>
        // <summary> Set the flag if the user is converted to using LDAP.</summary>
        // <param name="converted">true if converted.</param>
        virtual public bool Converted
        {
            get
            {
                return converted;
            }

            set
            {
                this.converted = value;
            }

        }
   
        // <summary> Get the salutation for a user. A salutation is Mr., Mis. etc. Most user account do not have
        // this value set.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the salutation for a user. A salutation is Mr., Mis. etc. Most user account do not have
        // this value set.</summary>
        // <param name="salutation">The salutation string.</param>
        virtual public System.String Salutation
        {
            get
            {
                return salutation;
            }

            set
            {
                this.salutation = value;
            }

        }

        // <summary> Get the realm for a user. A realm could be dvlp, qa, ua, prod </summary>
        // <returns> a String or null.</returns>
        // <summary> Set the realm for a user. A realm could be dvlp, qa, ua, prod </summary>
        // <param name="realm">The realm string.</param>
        virtual public System.String Realm
        {
            get
            {
                return realm;
            }

            set
            {
                this.realm = value;
            }

        }

        /* 12-20-2008 - GAM Added EmployeeType (String) per eMail from RSchrum */
        // <summary> Get the EmployeeType for a user.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the employeeType for a user</summary>
        // <param name="employeeType">The EmployeeType string.</param>
        virtual public System.String EmployeeType
        {
            get
            {
                return employeeType;
            }

            set
            {
                this.employeeType = value;
            }

        }

        /* 12-20-2008 - GAM Added UserAccountControl (bool) per eMail from RSchrum */
        // <summary> Get the UserAccountControl for a user.</summary>
        // <returns> a bool or null.</returns>
        // <summary> Set the UserAccountControl for a user. </summary>
        // <param name="obuseraccountcontrol">The UserAccountControl bool</param>
        virtual public bool UserAccountControl
        {
            get
            {
                return obUserAccountControl;
            }

            set
            {
                this.obUserAccountControl = value;
            }

        }


        /* 01-06-2009 - GAM Added obPasswordCreationDate (String) per eMail from DTalbot */
        // <summary> Get the obPasswordCreationDate for a user.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the obPasswordCreationDate for a user</summary>
        // <param name="obPasswordCreationDate">The obPasswordCreationDate string.</param>
        virtual public System.DateTime PasswordCreationDate
        {
            get
            {
                return obPasswordCreationDate;
            }

            set
            {
                this.obPasswordCreationDate = value;
            }

        }

        /* 01-06-2009 - GAM Added WebService (bool) per eMail from DTalbot */
        // <summary> Get if this account is a WebService.</summary>
        // <returns> a bool or null.</returns>
        // <summary> Set if this account is a WebService. </summary>
        // <param name="webService">The webService bool</param>
        virtual public bool WebService
        {
            get
            {
                return webService;
            }

            set
            {
                this.webService = value;
            }

        }

        /* 01-13-2009 - GAM Added ActiveStartDate (bool) per eMail from RSchrum */
        // <summary> Get ActiveStartDate for this user.</summary>
        // <returns> a Date or null.</returns>
        // <summary> Set ActiveStartDate for this user. </summary>
        // <param name="orclActiveStartDate">The ActiveStartDate DateTime</param>
        virtual public System.String ActiveStartDate
        {
            get
            {
                return orclActiveStartDate;
            }

            set
            {
                this.orclActiveStartDate = value;
            }

        }

        /* 01-13-2009 - GAM Added ActiveEndDate (bool) per eMail from RSchrum */
        // <summary> Get ActiveStartDate for this user.</summary>
        // <returns> a DateTime or null.</returns>
        // <summary> Set ActiveEndDate for this user. </summary>
        // <param name="orclActiveEndDate">The ActiveEndDate DateTime</param>
        virtual public System.String ActiveEndDate
        {
            get
            {
                return orclActiveEndDate;
            }

            set
            {
                this.orclActiveEndDate = value;
            }

        }

        /* 01-13-2009 - GAM Added IsEnabled (bool) per eMail from RSchrum */
        // <summary> Get if this account is Enabled.</summary>
        // <returns> a bool or null.</returns>
        // <summary> Set if this account is Enabled. </summary>
        // <param name="orclIsEnabled  ">The IsEnabled bool</param>
        virtual public bool IsEnabled
        {
            get
            {
                return orclIsEnabled;
            }

            set
            {
                this.orclIsEnabled = value;
            }

        }


 
        // <summary> Get the State Producer Licensing Customer Id.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the State Producer Licensing Customer Id.</summary>
        // <param name="splCustomerId">A String or null.</param>
        virtual public System.String SplCustomerId
        {
            get
            {
                return splCustomerId;
            }

            set
            {
                this.splCustomerId = value;
            }

        }

        // <summary> Get the account type of the user. The values, STATES, INTERNAL, or EXTERNAL, 
        // are value from the second level of the tree and are based off the DN of the user.</summary>
        // <returns> A String</returns>
        // <throws>  IllegalStateException if the LDAP structure contains the User node </throws>
        // <summary>  at the wrong level.</summary>
        virtual public System.String AccountType
        {
            get
            {
                System.String dn = distinguishedName.ToUpper();
                System.String[] cmp = dn.Split(',');
                int len = cmp.Length;
                if (len < 3)
                {
                    throw new System.SystemException("The LDAP tree has a invalid user structure.");
                }
                return cmp[len - 3].Substring(3).Trim();
            }

        }

        // <summary> Get the organizational unit of the user in upper case. Example values are ALABAMA, KANSAS, SSO, SVO, NIPR, CONSUMER, etc.  
        // These values are from the thrid level of the tree and are based off the DN of the user.</summary>
        // <returns> A String</returns>
        // <throws>  IllegalStateException if the LDAP structure contains the User node </throws>
        // <summary>  at the wrong level.</summary>
        virtual public System.String OrganizationalUnit
        {
            get
            {
                System.String dn = distinguishedName.ToUpper();
                System.String[] cmp = dn.Split(',');
                int len = cmp.Length;
                if (len < 4)
                {
                    throw new System.SystemException("The LDAP tree has a invalid user structure.");
                }
                return NetIQPeriodFix.fixVirginIslandsOU(cmp[len - 4].Substring(3).Trim());
            }

        }
        
        // <summary> If the roles attribute is null, the value hasn't been load. The UserService object can 
        // load this attribute.</summary>
        // <returns> the set of roles for the user or null.</returns>
        // <summary> Set the set of roles for the user. If the roles attribute is null, the value hasn't been load. The
        // UserService object can load this attribute.</summary>
        // <param name="roles">a Set.</param>
        //virtual public SupportClass.SetSupport Roles
        virtual public Collection<Role> Roles
        {
            get
            {
                return roles;
            }

            set
            {
                this.roles = value;
            }

        }
 
        // <summary> Holds internal ldap information used by the API. Do not use this field.</summary>
        // <returns> A Map</returns>
        // <summary>Holds internal ldap information used by the API. Do not use this field.</summary>
        // <param name="dataMap">a Map</param>
        virtual public System.Collections.IDictionary DataMap
        {
            get
            {
                return dataMap;
            }

            set
            {
                this.dataMap = value;
            }

        }
        private System.String distinguishedName;
        private System.String cn;
        private System.String uid;
        private System.String firstName;
        private System.String middleName;
        private System.String lastName;
        private System.String emailAddress;
        private System.String emailAddress2;
        private System.String emailAddress3;
        private System.String emailAddress4;
        private System.String department;
        private System.String jobTitle;
        private System.Collections.IList street = new System.Collections.ArrayList();
        private System.String city;
        private System.String state;
        private System.String zipCode;
        private System.String country;
        private System.String phoneNumber;
        private System.String phoneNumberExtension;
        private System.String faxNumber;
        //private System.Collections.IList databases = new System.Collections.ArrayList();
        private bool active;
        private bool converted;
        private bool expired;
        private System.String salutation;
        private System.String realm;

        /* 12-20-2008 - GAM Added employeeType and UserAccountControl per eMail from RSchrum */
        private System.String employeeType;
        private bool obUserAccountControl;

        /* 01-06-2009 - GAM Added webService and obPasswordCreationDate per eMail from DTalbot */
        private bool webService;
        private System.DateTime obPasswordCreationDate;

        /* 01-13-2009 - GAM Added ActiveStartDate, ActiveEndDate and orclIsEnabled per eMail from RSchrum */
        private System.String orclActiveStartDate;
        private System.String orclActiveEndDate;
        private bool orclIsEnabled;
        
        //Dim d As Date 'This variable is passed in by reference.
        //Date.TryParseExact("22/05/1900", New String() {"dd/MM/yyyy"}, Nothing, Globalization.DateTimeStyles.AdjustToUniversal, d) 'no exception throws
        //http://social.msdn.microsoft.com/Forums/en-US/vbgeneral/thread/025aefef-e7ba-4fd4-b4a8-00925d9bee34/

        private System.String splCustomerId;
        private System.Collections.IDictionary dataMap = new System.Collections.Hashtable();


        /// <associates>  <{org.naic.common.security.model.Role}> </associates>
        private Collection<Role> roles;
        //private Dictionary<string, string> roles =
        //    new Dictionary<string, string>();


        /// <summary> Create a new User object with all its attributes set to null.</summary>
        public User()
        {
        }


        // <summary> This method makes it easier to get the Street Address Line for a user.</summary>
        // <param name="line">The zero-based index for the Street Address Line.</param>
        // <returns> A String or null</returns>
        public virtual System.String getStreetLine(int line)
        {
            System.String retval = null;
            if (street.Count > line)
            {
                retval = ((System.String)street[line]);
            }
            return retval;
        }

        // <summary> Perform an equality check on the primary key values, distinguishedName, between this and 
        // another User object. This method returns false if the parameter is null or is not an instance of
        // User.</summary>
        // <param name="o">The other Object to compare.</param>
        // <returns> true if this object and the other object are equal else false.</returns>
        public override bool Equals(System.Object o)
        {
            bool retval = false;
            if (o is User)
            {
                User rhs = (User)o;
                retval = Util.equals(distinguishedName, rhs.distinguishedName);
            }
            // else already false
            return retval;
        }

        // <summary> Perform an equality check on each attribute of this User object and another. The set of roles is 
        // checked between the two objects. Those sets must contain Role object satisfying the isEqual 
        // requirement. This method returns false if the parameter is null or is not an instance of User.</summary>
        // <param name="o">The other Object.</param>
        // <returns> true if all the attribute values of this object and the other object are equal else false.</returns>
        public virtual bool isEqual(AttributeComparable o)
        {
            bool retval = false;
            if (o is User)
            {
                User rhs = (User)o;
                retval = this.Equals(rhs);
                if (retval)
                {
                    retval = Util.equals(uid, rhs.uid);
                }
                if (retval)
                {
                    retval = Util.equals(state, rhs.state);
                }

                if (retval)
                {
                    //retval = Util.isEqual(roles, rhs.roles);
                    if ((roles == null) && (rhs.roles == null)) 
                    {
                        retval = true;
                    }
                    else if ((roles == null && rhs.roles != null) || (roles != null && rhs.roles == null))
                    {
                        retval = false;
                    }
                    else if (roles != null && rhs.roles != null)
                    {
                        if (roles.Count != rhs.roles.Count)
                        {
                            retval = false;
                        }
                        else
                        {
                            ArrayList List1 = new ArrayList(roles);
                            ArrayList List2 = new ArrayList(rhs.roles);
                            List1.Sort();
                            List2.Sort();
                            Boolean rolesEqual = true;

                            for (int i = 0; i < List1.Count && i < List2.Count; i++)
                            {
                                Role mine = (Role)List1[i];
                                Role yours = (Role)List2[i];
                                if (!mine.Equals(yours))
                                {
                                    rolesEqual = false;
                                    break;
                                }
                            }
                            retval = rolesEqual;
                        }
                    }
                }
            }
            // else already false
            return retval;
        }

        /// <summary> Calculate the hash code value of the primary key attribute, distinguishedName,
        /// if distinguishedName isn't null, else 0.</summary>
        /// <returns> a int value.</returns>
        public override int GetHashCode()
        {
            return (distinguishedName == null) ? 0 : distinguishedName.GetHashCode();
        }

        // <summary> Returns the compareTo value for the primary key attribute, distinguishedName.</summary>
        // <param name="o">The other Object.</param>
        // <returns> greater than 0 if o is greater than this object, 0 if both objects are equal, or less than 0
        // if o is less than this object.</returns>
        // <throws>  NullPointerException if o is null or the distinguishedName of either Object is null. </throws>
        public virtual int CompareTo(System.Object o)
        {
            User rhs = (User)o;
            //return String.CompareOrdinal(this.distinguishedName, rhs.distinguishedName);
            return String.Compare(this.distinguishedName, rhs.distinguishedName,true);
        }

        // <summary> Create a new User with the attribute values as this User.</summary>
        // <returns> a new User object.</returns>
        // <throws>  CloneNotSupportedException not thrown by this implementation. </throws>
        public virtual System.Object Clone()
        {
            return base.MemberwiseClone();
        }

        public virtual Boolean hasRole(Role checkedRole)
        {
            Boolean hasRole = false;

            if (checkedRole != null && this.roles != null)
            {
                foreach (Role role in roles)
                {
                    if (role.Name.Equals(checkedRole.Name))
                    {
                        hasRole = true;
                        break;
                    }
                }
            }

            return hasRole;
        }

        public virtual Boolean hasRole(String roleName)
        {
            Boolean hasRole = false;

            if (roleName != null && this.roles != null)
            {
                foreach (Role role in roles)
                {
                    //Console.WriteLine("roles.Count  " + roles.Count);
                    if (roleName.Equals(getRoleFromString(role.DistinguishedName.ToString())))
                    //if (role.Name.Equals(getRoleFromString(roleName)))
                    //if (role.Name.ToString().Contains(roleName))
                    {
                        hasRole = true;
                        break;
                    }
                }
            }

            return hasRole;
        }


        public virtual System.String getRoleFromString(String roleName)
        {
            System.String sStartSearch = "cn=";
            System.String sEndSearch = ",ou=";
            System.String sRet = System.String.Empty;
            int iStartPos = roleName.Trim().IndexOf(sStartSearch) + sStartSearch.Length;
            int iEndPos = roleName.Trim().IndexOf(sEndSearch) - sEndSearch.Length;
            if ((iStartPos >= sStartSearch.Length) && (iEndPos > iStartPos))
            {
                sRet = roleName.Substring(iStartPos , iEndPos + 1);
            }
            else {
                sRet = roleName;
            }

            return sRet;
        }

    }


}
