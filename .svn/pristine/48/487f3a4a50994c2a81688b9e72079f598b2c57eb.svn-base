using System;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    /// <summary> This class contains a set of static utility method.</summary>
    public class Util
    {
        private Util()
        {
        }

        // <summary> Perform a null safe equals check. If one parameter is null and the other isn't null, return false.
        // If both parameters are null, return true. Otherwise, return the result of lhs.equals(rhs).</summary>
        // <param name="lhs">any Object that supports equals</param>
        // <param name="rhs">any Object that supports equals</param>
        // <returns> true if the two objects are equal.</returns>
        public static bool equals(System.Object lhs, System.Object rhs)
        {
            //Console.WriteLine("lhs " + lhs);
            //Console.WriteLine("rhs " + rhs);
            //Console.WriteLine("lhs.Equals(rhs) " + lhs.Equals(rhs));
            return (lhs == null) ? (rhs == null) : lhs.Equals(rhs);
        }

        // <summary> Perform a null safe equals check. If one parameter is null and the other isn't null, return false.
        // If both parameters are null, return true. Otherwise, return the result of lhs.equals(rhs).</summary>
        // <param name="lhs">any Object that supports equals</param>
        // <param name="rhs">any Object that supports equals</param>
        // <returns> true if the two objects are equal.</returns>
        public static bool equalsIgnoreCase(System.String lhs, System.String rhs)
        {
            //Console.WriteLine("lhs " + lhs);
            //Console.WriteLine("rhs " + rhs);
            //Console.WriteLine("lhs.Equals(rhs) " + lhs.Equals(rhs));
            return (lhs == null) ? (rhs == null) : lhs.Equals(rhs, StringComparison.InvariantCultureIgnoreCase);
        }

        // <summary> Perform a null safe isEqual check on two AttributeComparable objects.  If one parameter is null 
        // and the other isn't null, return false.  If both parameters are null, return true. Otherwise, 
        // return the result of lhs.equals(rhs).</summary>
        // <param name="lhs">any AttributeComparable object</param>
        // <param name="rhs">any AttributeComparable object</param>
        // <returns> true if the two objects are equal at the attribute level or both are null.</returns>
        public static bool isEqual(AttributeComparable lhs, AttributeComparable rhs)
        {
            return (lhs == null) ? (rhs == null) : lhs.isEqual(rhs);
        }

        // <summary>	Perform an isEqual check on AttributeComparable objects in a Collection.  If one parameter is null 
        // and the other isn't null, return false.  If both parameters are null, return true. If the size of the collections
        // don't match, return false. Otherwise, iterate over the both collections and do an isEqual check on the
        // AttributeComparable objects. Once a check results in false, the iteration is stoped and the method
        // returns false. This method requires the iterator of both Collection to iterate the Collection
        // in the same order.</summary>
        // <param name="lhs">any Collection that contains AttributeComparable objects</param>
        // <param name="rhs">any Collection that contains AttributeComparable objects</param>
        // <returns> true if the Collections satifsy the isEqual requirement, else false.</returns>
        // <throws>  ClassCastException  Thrown if any object in the Collection does not implement the AttributeComparable interface. </throws>
        public static bool isEqual(System.Collections.ICollection lhs, System.Collections.ICollection rhs)
        {
            bool retval = true;
            if (lhs == null)
            {
                retval = (rhs == null);
            }
            else if (rhs == null)
            {
                retval = false;
            }
            else if (lhs.Count != rhs.Count)
            {
                retval = false;
            }
            else
            {
                // not null and have the same size
                System.Collections.IEnumerator lhs_iter = lhs.GetEnumerator();
                System.Collections.IEnumerator rhs_iter = rhs.GetEnumerator();
                while (retval && lhs_iter.MoveNext() && rhs_iter.MoveNext())
                {
                    AttributeComparable lhs_obj = (AttributeComparable)lhs_iter.Current;
                    AttributeComparable rhs_obj = (AttributeComparable)rhs_iter.Current;
                    retval = isEqual(lhs_obj, rhs_obj);
                }
            }
            return retval;
        }
    }
}
