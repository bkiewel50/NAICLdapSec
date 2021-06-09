using System;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    /// <summary> This class provides comparisons on two Role on name. It is used in Collection
    /// classes to provide sorting on name.
    /// </summary>
    public class RoleNameComparator : System.Collections.IComparer
    {
        // <summary> Doesn't do anything but creates the object.</summary>
        public RoleNameComparator()
        {
        }

        // <summary> Compare two roles on name.</summary>
        // <param name="lhs">The first Role object</param>
        // <param name="rhs">The second Role object.</param>
        // <returns> -1 if lhs.getName() is less than rhs.getName(), 
        // 0 if  lhs.getName() is equals rhs.getName(), and
        // +1  lhs.getName() is greater than rhs.getName()</returns>
        // <throws>  NullPointerException if lhs, rhs, lhs.getName(), or rhs.getNames() is null. </throws>
        public virtual int Compare(System.Object lhs, System.Object rhs)
        {
            Role r_lhs = (Role)lhs;
            Role r_rhs = (Role)rhs;

            //return String.CompareOrdinal(r_lhs.Name, r_rhs.Name);
            //switching to use Compare (strA As String,	strB As String,	ignoreCase As Boolean) As Integer
            return String.Compare(r_lhs.Name, r_rhs.Name,true);
        }
    }
}
