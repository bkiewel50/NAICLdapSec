
using Syscert = System.Security.Cryptography.X509Certificates;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

using Novell.Directory.Ldap;

using Novell.Directory.Ldap.Utilclass;
using Mono.Security.X509;
using Mono.Security.Cryptography;

namespace NAICLdapSec
{
    public class LDAPDao
    {

        protected static bool bOkToProceed, removeFlag = false;
        protected static int bindCount = 0;


        // <summary> The default realm for the LDAP tree.</summary>
        protected internal const System.String DEFAULT_REALM = "naic";

        // <summary> The root distinguished name for all Groups/Roles.</summary>
        protected internal const System.String GROUPS_ROOT_DN = "ou=groups,o=data";

        // <summary> The root distinguished name to start looking for DATABASE groups/roles.</summary>
        protected internal const System.String DATABASES_ROOT_DN = "ou=Databases," + GROUPS_ROOT_DN;

        // <summary> The root distinguished name for users.</summary>
        protected internal const System.String USERS_ROOT_DN = "ou=users,o=data";

        // <summary> The root distinguished name to start looking for the Application id.</summary>
        protected internal const System.String APP_ID_ROOT_DN = "ou=App ID,ou=App Support," + USERS_ROOT_DN;

        // <summary> The root distinguished name to start looking for the Application id.</summary>
        protected internal const System.String APPLICATIONS_ROOT_DN = "ou=Applications," + USERS_ROOT_DN;

        // <summary> The root distinguished name to start looking for the Application id.</summary>
        protected internal const System.String APPLICATIONS_GROUP_DN = "ou=Applications," + GROUPS_ROOT_DN;

        // <summary> The root distinguished name to start looking for the Application id.</summary>
        protected internal const System.String DATABASES_BASE_DN = "ou";
        //Commonly used as DATABASES_BASE_DN + "=" + databaseName + "," + DATABASES_ROOT_DN'

        private LDAPConnection connection;

        // <summary> Create a new LdapDao object owning the provided connection.</summary>
		// <param name="connection">a LDAP connection.</param>
        protected internal LDAPDao(LDAPConnection connection)
		{
			this.connection = connection;
		}

        // <summary> Provides an implementation of finding a node in the LDAP tree. If the node is found,
        // this method passes the attribute data to createObjectForFind. If the node is not found,
        // the method returns null.</summary>
        // <param name="starting_dn">The distinguishedName for the node to start the search.</param>
        // <param name="condition">The LDAP search condition.</param>
        // <param name="returning_attributes">The attributes to return from LDAP for the matching nodes.</param>
        // <returns> A new object created by createObjectForFind or null if not found.</returns>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        // <throws>  IllegalStateException Thrown if the connection object is not valid. </throws>
        protected internal virtual System.Object findObject(String starting_dn, String condition, String[] returning_attributes)
        {
            Object retval = null;
            ListDictionary myCol = new ListDictionary();
            string dB = System.String.Empty;
            DAOException error = null;
            string dn = System.String.Empty;
            String realm = System.String.Empty;
            try
            {
                
                assertConnected();
                LdapConnection conn = connection.getConnection();

                if (conn.Host.Equals("edir-dvlp.naic.org"))
                {
                    myCol.Add("realm", "dvlp");
                }
                else if (conn.Host.Equals("edir-qa.naic.org"))
                {
                    myCol.Add("realm", "qa");
                }
                else if (conn.Host.Equals("edir.naic.org"))
                {
                    myCol.Add("realm", "prod");
                }
                
                LdapSearchResults lsc = conn.Search(starting_dn, LdapConnection.SCOPE_SUB, condition, returning_attributes, false);
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
                    dn = nextEntry.DN;
                    LdapAttributeSet attributeSet = nextEntry.getAttributeSet();
                    System.Collections.IEnumerator ienum = attributeSet.GetEnumerator();
                    while (ienum.MoveNext())
                    {
                        LdapAttribute attribute = (LdapAttribute)ienum.Current;
                        string attributeName = attribute.Name;
                        string attributeVal = attribute.StringValue;
                        //Console.WriteLine(attributeName + "value:" + attributeVal);
                        myCol.Add(attributeName, attributeVal);
                    }
                }

                retval = createObjectForFind(dn, myCol);

             }
            catch (System.Exception e)
            {
                error = new DAOException("Failure trying to find a user.", e);
            }
            return retval;
        }

        // <summary> Provides an implementation of finding a node in the LDAP tree. If the node is found,
        // this method returns string data. If the node is not found, the method returns null.</summary>
        // <param name="starting_dn">The distinguishedName for the node to start the search.</param>
        // <param name="condition">The LDAP search condition.</param>
        // <param name="returning_attribute">The attribute to return from LDAP for the matching node.</param>
        // <returns> A String or null if not found.</returns>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        // <throws>  IllegalStateException Thrown if the connection object is not valid. </throws>
        protected internal virtual System.String findItem(String starting_dn, String condition, String[] returning_attribute)
        {
            //Object retval = null;
             string sRet = System.String.Empty;
            string dn = System.String.Empty;
            DAOException error = null;
            try
            {
                assertConnected();
                LdapConnection conn = connection.getConnection();

                LdapSearchResults lsc = conn.Search(starting_dn, LdapConnection.SCOPE_SUB, condition, returning_attribute, false);
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
                    dn = nextEntry.DN;
                    LdapAttributeSet attributeSet = nextEntry.getAttributeSet();
                    System.Collections.IEnumerator ienum = attributeSet.GetEnumerator();
                    while (ienum.MoveNext())
                    {
                        LdapAttribute attribute = (LdapAttribute)ienum.Current;

                        sRet = attribute.StringValue;
                    }
                }
            }
            catch (System.Exception e)
            {
                error = new DAOException("Failure trying to find a user.", e);
            }
            //Console.WriteLine("findItem " + sRet);
            return sRet;
        }

        // <summary> Provides an implementation of finding nodes in the LDAP tree. When a node is found
        // this method passes the attribute data to createObjectForFind. If the node is not found,
        // the method returns null.</summary>
        // <param name="starting_dn">The distinguishedName for the node to start the search.</param>
        // <param name="condition">The LDAP search condition.</param>
        // <param name="returning_attributes">The attributes to return from LDAP for the matching nodes.</param>
        // <returns> A new object created by createObjectForFind or null if not found.</returns>
        // <throws>  DaoException An error occurred in the persistence layer. </throws>
        // <throws>  IllegalStateException Thrown if the connection object is not valid. </throws>
        //protected internal virtual System.Object findObjects(String starting_dn, String condition, String[] returning_attributes)
        protected internal virtual Collection<System.Object> findObjects(String starting_dn, String condition, String[] returning_attributes)
        {
            //Object retval = null;
            ListDictionary myCol = new ListDictionary();
            Collection<System.Object> results = new Collection<System.Object>();
            DAOException error = null;
            String dn = System.String.Empty;
            try
            {
                assertConnected();


  
                LdapConnection conn = connection.getConnection();
               
                LdapSearchResults lsc = conn.Search(starting_dn, LdapConnection.SCOPE_SUB, condition, returning_attributes, false);
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
                    if (myCol.Count > 0)
                    {
                        System.Object myObj = createObjectForFind(dn, myCol);
                        results.Add(myObj);
                        myCol.Clear();
                    }
                    dn = nextEntry.DN;
                    //Console.WriteLine("\n" + "DN: " + dn);
                    myCol.Add("dn", dn);
                    LdapAttributeSet attributeSet = nextEntry.getAttributeSet();
                    System.Collections.IEnumerator ienum = attributeSet.GetEnumerator();
                    while (ienum.MoveNext())
                    {
                        LdapAttribute attribute = (LdapAttribute)ienum.Current;
                        string attributeName = attribute.Name;
                        string attributeVal = attribute.StringValue;
                        //Console.WriteLine(attributeName + "  value: " + attributeVal);
                        myCol.Add(attributeName, attributeVal);
                    }
                }


                if (myCol.Count > 0)
                {
                    System.Object myObj = createObjectForFind(dn, myCol);
                    results.Add(myObj);
                }
            }
            catch (System.Exception e)
            {
                error = new DAOException("Failure trying to find a user.", e);
            }
            return results;
        }

        // <summary> Children classes should implement this method if they use the findObject method. Child implementation
        // should create a new object using the distringuished name of the node and its attributes.
        // <p>
        // This implementation allways throws UnsupportedOperationException.</summary>
        // <param name="dn">The distinguishedName for the user.</param>
        // <param name="attrs">The attributes read from LDAP</param>
        // <returns> a new object</returns>
        // <throws>  NamingException JNDI exception occurred. </throws>
        // <throws>  UnsupportedOperationException if the method is not implemented in the child class. </throws>
        protected internal virtual System.Object createObjectForFind(System.String dn, ListDictionary attrs)
        {
            throw new System.NotSupportedException("The method, findObjectBySimpleName, was called with " + "having the method, createObjectForFind, implemented.");
        }


        // <summary> Throw an IllegalStateException if the LDAP connection is not connected.</summary>
        protected internal virtual void assertConnected()
        {
            if (connection.isConnected() == false)
            {
                throw new System.SystemException("The LDAP connection is not established.");
            }
        }

        // <summary> Read the attribute value from a Attributes object for a given attribute name.</summary>
        // <param name="name">A String and not null</param>
        // <param name="attrs">LDAP Attribute object and not null</param>
        // <returns> null or the value of the Attribute</returns>
        protected internal static System.String readAttribute(System.String name, ListDictionary  attrs)
        {
            System.String retval = System.String.Empty;
            try
            {
                string a = (string)attrs[name];
                if (a != null)
                {
                    retval = ((System.String)a);
                }
            }
            catch (NullReferenceException ne)
            {
                Console.WriteLine("NullReferenceException in LDAPDao::readAttribute() " + ne.Message );
            }
            return retval;
        }

        // <summary> Read the attribute value from a Attributes object for a given attribute name.</summary>
        // <param name="name">A String and not null</param>
        // <param name="attrs">LDAP Attribute object and not null</param>
        // <returns> null or the value of the Attribute</returns>
        protected internal static System.Collections.IList readAttributeStringValues(System.String name, ListDictionary attrs)
        {
            ArrayList retval = new ArrayList();

            try
            {
                string a = (string)attrs[name];
                retval.Add(a);
             }
            catch (NullReferenceException ne)
            {
                Console.WriteLine("Exception in LDAPDao::readAttributeStringValues() " + ne.Message);
            }      
            return retval;
        }


    }
}
