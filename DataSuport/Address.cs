using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;

namespace DataSupport
{
    public class Address
    {
        readonly User _user;
        readonly string _subscriptionAddress;
        readonly Blacklist _blacklist;
        string _addressDescription;
        string _relayAddress;

        public Address( User u, string subscriptionAddress, string addressDescription = null, string relayAddress = null )
        {
            _user = u;
            _subscriptionAddress = subscriptionAddress;
            _blacklist = new Blacklist();
            _addressDescription = addressDescription;
            _relayAddress = relayAddress; 
        }

        public User User
        {
            get { return _user; }
        }

        public string SubscriptionAddress
        {
            get { return _subscriptionAddress; }
        }

        public Blacklist Blacklist
        {
            get { return _blacklist; }
        }

        public string AddressDescription
        {
            get { return _addressDescription; }
            set { _addressDescription = value; }
        }

        public string RelayAddress
        {
            get { return _relayAddress; }
            set { _relayAddress = value; }
        }

        public void Write( TextWriter stream)
        {
            stream.WriteLine("address: {0}", _subscriptionAddress );
            stream.WriteLine("description: {0}", _addressDescription);
            stream.WriteLine("relay address: {0}", _relayAddress);
            stream.WriteLine("blacklist: ");
            _blacklist.Write(stream);
            stream.WriteLine();
        }

        public string Read(TextReader reader)
        {
            string line;
            
            line = reader.ReadLine();
            if(User.ParseLine(line, "description", out _addressDescription) == false) return "Expected 'description : xxxxx'";
            line = reader.ReadLine();
            if (User.ParseLine(line, "relay address", out _relayAddress) == false) return "Expected 'relay address : xxxxx'";
            line = reader.ReadLine();
            if (line.Trim().StartsWith("blacklist :") == false) return "Expected 'blacklist :'";

            if (_blacklist.Read(reader) == false) return "Expected 'fuck : xxxx' or 'ignore : xxxxx'";

            return null;
        }
    }
}
