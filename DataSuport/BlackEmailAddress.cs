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
        bool _isFucking;

        BlackEmailAddress(string address, bool isFucking)
        {
            _address = address;
            _isFucking = isFucking;

        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public bool IsFucking
        {
            get { return _isFucking; }
            set { _isFucking = value; }
        }
    }
}
