using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSupport
{
    public class UserReadInfo
    {
        public User User { get; private set; }

        public string ErrorMessage { get; private set; }

        public bool IsValid { get { return ErrorMessage == null; } }

        internal UserReadInfo(User u)
        {
            User = u;
        }
        
        internal UserReadInfo( string errorMessage )
        {
            ErrorMessage = errorMessage;
        }

    }
}
