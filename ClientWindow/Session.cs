using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSuport;

namespace ClientWindow
{
    public class Session
    {
        string _username;
        string _password;
        bool _isInitialized;
        List<Address> _data;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public List<Address> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public bool IsInitialized
        {
            get { return _isInitialized; }
            set { _isInitialized = value; }
        }

    }
}
