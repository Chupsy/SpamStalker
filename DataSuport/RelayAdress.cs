using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSupport
{
    public class RelayAddress
    {
        string _relayAddressName;

        public RelayAddress(string relayAddressName)
        {
            _relayAddressName = relayAddressName;
        }

        public string RelayAddressName
        {
            get { return _relayAddressName; }
            set { _relayAddressName = value; }
        }

    }
}
