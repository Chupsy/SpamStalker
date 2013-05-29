using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSuport
{
    class RelayAdress
    {
        string _relayAdress;

        RelayAdress(string relayAdress)
        {
            _relayAdress = relayAdress;
        }

        public string relayAdress
        {
            get { return _relayAdress; }
            set { _relayAdress = value; }
        }

    }
}
