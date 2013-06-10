using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;

namespace FakeSmtp
{
    class User
    {
        string _username;
        string _password;
        string _accountType;
        List<Address> _data;

        public User(string username, string password, string accountType)
        {
            _username = username;
        }
    }
}
