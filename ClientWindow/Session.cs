using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;

namespace ClientWindow
{
    public class Session
    {
        bool _isInitialized;
        User _user;


        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
        public bool IsInitialized
        {
            get { return _isInitialized; }
            set { _isInitialized = value; }
        }

    }
}
