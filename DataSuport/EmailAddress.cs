using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSupport
{
    public class EmailAddress
    {
        string _address;

        EmailAddress(string address)
        {
            _address = address;
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

    }
}
