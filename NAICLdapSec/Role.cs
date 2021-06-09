using System;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    // <summary> This class represents a role on a database or for an application. The primary key attribute is the 
    // distinguishedName of the Role. 
    // <p>
    // A fully constructed Role object should have the following invariants: (Not enforced by the code.)
    // <ol>
    // <li>The distinguishedName and name are not null.</li>
    // <li>The distinguishedName is unqiue for all Roles in existance.</li>
    // <li>The name is unqiue for all Roles for a databaes or application .</li>
    // <li>If database is not null, application is null.</li>
    // <li>If application is not null, database is null.</li>
    // </ol>
    // <p>
    // The class fully implements the Object interface and can be used in Collection classes. It is
    // sorted by and compared on the distinguishedName attribute only.</summary>
    [Serializable]
    public class Role : System.IComparable, System.ICloneable, AttributeComparable
    {

        // <summary> The LDAP Role Base DN.</summary>
        private const System.String ROLE_BASE_DN = "cn";


        // <summary> Get the value of the distinguishedName for the Role.</summary>
        // <returns> a String and not null.</returns>
        // <summary> Set the value of the distinguishedName for the Role. This value is the primary key value for the
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
 
        // <summary> Get the value of the name for the Role.</summary>
        // <returns> A String and not null.</returns>
        // <summary> Set the name of the Role object. The name should be unique for a database or application.</summary>
        // <param name="name">a String and not null.</param>
        virtual public System.String Name
        {
            get
            {
                return name;
            }

            set
            {
                this.name = value;
            }

        }
        
        // <summary> Get the value of the database for the Role or null if the Role doesn't belong to a database.</summary>
        // <returns> a String or null.</returns>
        // <summary> Set the database containing the role.</summary>
        // <param name="database">a String or null.</param>
        virtual public System.String Database
        {
            get
            {
                return database;
            }

            set
            {
                this.database = value;
            }

        }
       
        // <summary> Get the value of the application for the Role or null if the Role doesn't belong to a application.</summary>
        // <returns> A String or null.</returns>
        // <summary> Set the application containing the role.</summary>
        // <param name="application">a String or null.</param>
        virtual public System.String Application
        {
            get
            {
                return application;
            }

            set
            {
                this.application = value;
            }

        }
        private System.String distinguishedName;
        private System.String name;
        private System.String database;
        private System.String application;

        // <summary> Create a Role object for a role on a database. The Role object has the correct 
        // distinguishedName value for the database.</summary>
        // <param name="database">The name of the database.</param>
        // <param name="role_name">The name of the role.</param>
        // <returns> A new Role object.</returns>
        public static Role createDatabaseRole(System.String database, System.String role_name)
        {
            System.Text.StringBuilder dn = new System.Text.StringBuilder();
            dn.Append(ROLE_BASE_DN + "=");
            //dn.Append(role_name);
            dn.Append(role_name.ToUpper());
            dn.Append("," + LDAPDao.DATABASES_BASE_DN + "=");
            dn.Append(database);
            dn.Append("," + LDAPDao.DATABASES_ROOT_DN);
            return new Role(dn.ToString(), role_name, database, null);
        }

        // <summary> Create a Role object for a role in LDAP for an application. The Role object has the correct 
        // distinguishedName value for the application.</summary>
        // <param name="application">The name of the application.</param>
        // <param name="role_name">The name of the role.</param>
        // <returns> A new Role object.</returns>
        public static Role createApplicationRole(System.String application, System.String role_name)
        {
            System.Text.StringBuilder dn = new System.Text.StringBuilder();
            dn.Append(ROLE_BASE_DN + "=");
            //dn.Append(role_name);
            dn.Append(role_name.ToUpper());
            dn.Append("," + LDAPDao.APPLICATIONS_GROUP_DN);
            return new Role(dn.ToString(), role_name, null, application);
        }

        // <summary> Create a new Role object with all its attributes set to null.</summary>
        public Role()
        {
        }

        // <summary> Create a new Role object taking the attribute values from the parameters. While not enforce
        // by the code, a client should set the application or database attribute to a value other than null 
        // but not both. Also, while not enforced by the class, the name for the role should be 
        // unique for a given database or application.</summary>
        // <param name="distinguishedName">the primary identifier for the role. The value should not be null.</param>
        // <param name="name">the name of the role. The value should not be null.</param>
        // <param name="database">the database containing the role. The value can be null.</param>
        // <param name="application">the application owning the role. The value can be null.</param>
        public Role(System.String distinguishedName, System.String name, System.String database, System.String application)
        {
            this.distinguishedName = distinguishedName;
            this.name = name;
            this.database = database;
            this.application = application;
        }

        // <summary> Perform an equality check on the primary key values, distinguishedName, between this and 
        // another Role object. This method returns false if the parameter is null or is not an instance of
        // Role.</summary>
        // <param name="o">The other Object to compare.</param>
        // <returns> true if this object and the other object are equal else false.</returns>
        public override bool Equals(System.Object o)
        {
            bool retval = false;
            if (o is Role)
            {
                Role rhs = (Role)o;
                //retval = Util.equals(distinguishedName, rhs.distinguishedName);
                retval = Util.equalsIgnoreCase(distinguishedName, rhs.distinguishedName);
            }
            // else already false
            return retval;
        }

        // <summary> Perform an equality check on each attribute of this Role object and another. This method 
        // returns false if the parameter is null or is not an instance of Role.</summary>
        // <param name="o">The other Object.</param>
        // <returns> true if all the attribute values of this object and the other object are equal else false.</returns>
        public virtual bool isEqual(AttributeComparable o)
        {
            bool retval = false;
            if (o is Role)
            {
                Role rhs = (Role)o;
                retval = this.Equals(rhs);
                if (retval)
                {
                    //retval = Util.equals(name, rhs.name);
                    retval = Util.equalsIgnoreCase(name, rhs.name);
                }
                if (retval)
                {
                    //retval = Util.equals(database, rhs.database);
                    retval = Util.equalsIgnoreCase(database, rhs.database);
                }
                if (retval)
                {
                    //retval = Util.equals(application, rhs.application);
                    retval = Util.equalsIgnoreCase(application, rhs.application);
                }
            }
            // else already false	    

            return retval;
        }

        // <summary> Calculate the hash code value of the primary key attribute, distinguishedName,
        // if distinguishedName isn't null, else 0.</summary>
        // <returns> a int value.</returns>
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
            Role rhs = (Role)o;
            //return String.CompareOrdinal(this.distinguishedName, rhs.distinguishedName);
            return String.Compare(this.distinguishedName, rhs.distinguishedName,true);
        }

        // <summary> Create a new Role with the attribute values as this Role.</summary>
        // <returns> a new Role object.</returns>
        // <throws>  CloneNotSupportedException not thrown by this implementation. </throws>
        public virtual System.Object Clone()
        {
            return base.MemberwiseClone();
        }
    }
}
