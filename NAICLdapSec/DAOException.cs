using System;
using System.Collections.Generic;
using System.Text;

namespace NAICLdapSec
{
    class DAOException : ApplicationException
    {
        public DAOException() : base() {}
        public DAOException(string s) : base(s) {}
        public DAOException(string s, Exception ex) : base(s, ex) { }
    }
}
