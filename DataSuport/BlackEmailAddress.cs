using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSupport
{
    public class BlackEmailAddress
    {
        string _address;
        bool _isfucking;

        BlackEmailAddress(string address, bool isfucking)
        {
            _address = address;
            _isfucking = isfucking;

        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public bool Isficking
        {
            get { return _isfucking; }
            set { _isfucking = value; }
        }
    }
}
