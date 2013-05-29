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
        public List<Adress> data;
        
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string password
        {
            get { return _password; }
            set { _password = value; }
        }


       
      
        
    }
}
