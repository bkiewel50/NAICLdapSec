using System;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    // <summary> A class implementing this interface is stating that a client of the class can compare two object of 
    // the class on each attribute of the class. This concept is different than the equals method on Object
    // where classes implementing the method only return the result of comparing the primary key of the
    // Object.</summary>
    public interface AttributeComparable
    {
        // <summary> Compare the values of each attribute of this class. If they are equal in value, return true else
        // return false. For classes with attributes that are collections, the implementation should check
        // that the objects in the Collection statify the isEqual requirement.
        // <p>
        // The implementation should not throw a ClassCastException or generate a NullPointerException.</summary>
        // <param name="rhs">the AttributeComparable object to compare with.</param>
        /// <returns> true if the two Classes satisfy the isEqual requirement else false.</returns>
        bool isEqual(AttributeComparable rhs);
    }
}
