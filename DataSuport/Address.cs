using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace DataSuport
{
    public class Address
    {
        EmailAddress _userAddress;
        Blacklist _addressBlacklist;
        Description _addressDescription;
        RelayAddress _relayAddress;

        public Address(EmailAddress userAddress, Blacklist addressBlacklist, Description addressDescription, RelayAddress relayAddress)
        {
            _userAddress = userAddress;
            _addressBlacklist = addressBlacklist;
            _addressDescription = addressDescription;
            _relayAddress = relayAddress; 
        }

        public EmailAddress UserAddress
        {
            get { return _userAddress; }
            set { _userAddress = value; }
        }

        public Blacklist AddressBlacklist
        {
            get { return _addressBlacklist; }
            set { _addressBlacklist = value; }
        }

        public Description AddressDescription
        {
            get { return _addressDescription; }
            set { _addressDescription = value; }
        }

        public RelayAddress RelayAddress
        {
            get { return _relayAddress; }
            set { _relayAddress = value; }
        }


    }
}
