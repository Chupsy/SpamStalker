using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;
using System.Configuration;
using System.IO;
using System.Net.Mail;

namespace DataSupport
{
    public class User
    {
        string _username;
        string _password;
        string _accountType;
        readonly List<Address> _addresses;
        bool _isInitialized;

        public User(string username, string password, string accountType)
        {
            _username = username;
            _password = password;
            _accountType = accountType;
            _addresses = new List<Address>();
        }

        public bool IsInitialized
        {
            get { return _isInitialized; }
            set { _isInitialized = value; }
        }

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

        public string AccountType
        {
            get { return _accountType; }
            set { _accountType = value; }
        }

        public List<Address> Addresses
        {
            get { return _addresses; }
        }

        public Address FindAddress(string subscriptionAddress)
        {
            foreach (var a in _addresses)
                if (a.SubscriptionAddress == subscriptionAddress) return a;
            return null;
        }

        public void Write(string usersDiretory)
        {
            Directory.CreateDirectory(usersDiretory);
            string dataPath = Path.Combine(usersDiretory, Username + ".txt");
            using (StreamWriter stream = File.CreateText(dataPath))
            {
                Write(stream);
            }
        }

        public void Write(TextWriter stream, bool writeUserName = false)
        {
            if(writeUserName) stream.WriteLine("name: {0} ", Username);
            stream.WriteLine("password: {0} ", Password);
            stream.WriteLine("account: {0}", AccountType);
            stream.WriteLine();
            foreach (Address a in Addresses) a.Write(stream);
        }

        public static UserReadInfo Read(string username, string dataPath)
        {
            return Read( Path.Combine(dataPath, username + ".txt") );
        }

        public static UserReadInfo Read(string userDataPath)
        {
            using (StreamReader stream = new StreamReader(userDataPath))
            {
                return Read(stream);
            }
        }

        public static UserReadInfo Read(TextReader reader)
        {
            string line = reader.ReadLine();
            string userName;
            string password;
            string account;
            if (ParseLine(line, "username", out userName)) line = reader.ReadLine();
            if (ParseLine( line, "password", out password)) line = reader.ReadLine();
            else
            {
                return new UserReadInfo("Expected: 'password: xxxxx'");
            }
            if (ParseLine( line, "account", out account) 
                && (account == "user" || account == "admin") ) line = reader.ReadLine();
            else
            {
                return new UserReadInfo("Expected: 'account: user' or 'account: admin' ");
            }
            User u = new User(userName, password, account);
            if (ParseEmptyLine(line)) line = reader.ReadLine();
            else
            {
                return new UserReadInfo("Expected a blanck line.'");
            }

            string subscriptionAddress;
            while (ParseLine(line, "address", out subscriptionAddress))
            {
                Address a = new Address( u, subscriptionAddress);
                string errorMessage = a.Read(reader);
                if( errorMessage != null ) return new UserReadInfo(errorMessage);
                u.Addresses.Add(a);
                line = reader.ReadLine();
            }
            return new UserReadInfo(u); 
        }

        internal static bool ParseEmptyLine(string line)
        {
            if (line == null) return false;
            return line.Trim().Length == 0;
        }

        internal static bool ParseLine(string line, string propertName, out string value )
        {
            value = null;
            if (line == null) return false;
            string[] split = line.Split(':');
            if (split.Length != 2) return false;
            if (split[0].Trim() != propertName) return false;
            value = split[1].Trim();
            return true;
        }

        public void ModifyType(string type)
        {
            if (type == "admin" || type == "user")
            {
                AccountType = type;
            }
        }

        public void ModifyPassword(string password)
        {
            Password = password;
        }

        public void AddAddress(string address,  string relayAddress, string description)
        {
            _addresses.Add( new Address(this, address, description, relayAddress));

        }


        public bool CheckSpammer(string blackListFrom, string blacklistedAddress)
        {
            foreach (Address a in Addresses)
            {
                if (a.SubscriptionAddress == blackListFrom)
                {
                    foreach (BlackEmailAddress b in a.Blacklist)
                    {
                        if (b.Address == blacklistedAddress)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        public void AddBlacklistAddress(string blacklistFrom, string addressToBlacklist)
        {
            foreach (Address a in Addresses)
            {

                if (a.SubscriptionAddress == blacklistFrom)
                {
                    a.Blacklist.Add(new BlackEmailAddress(addressToBlacklist, false));
                    break;
                }
            }
        }

        public static User CreateUser(string username, string password, string newAdress, string AccountType)
        {
            
            string _description = "Main adress";
            User newUser = new User(username, password, AccountType);
            List<Address> data = new List<Address>();
            newUser.AddAddress(newAdress, newAdress, _description);
            return newUser;
        }

        public void RemoveBlacklistedAddress(string blackListFrom, string addressToRemove)
        {
            Address addressBlack = null;
            BlackEmailAddress blacklistToRemove = null;
            foreach (Address a in _addresses)
            {
                if (a.SubscriptionAddress == blackListFrom)
                {
                    addressBlack = a;
                    foreach (BlackEmailAddress b in a.Blacklist)
                    {
                        if (addressToRemove == b.Address) blacklistToRemove = b;
                    }
                }
            }
            if (addressBlack != null && blacklistToRemove != null)
            {
                addressBlack.Blacklist.Remove(blacklistToRemove);
            }
            
        }

        public static System.Net.Mail.MailAddressCollection CheckSpammer(System.Net.Mail.MailAddressCollection recipientAddress, string sender, string dataPath)
        {
            MailAddressCollection collectionSpammed = new MailAddressCollection();
            foreach (MailAddress a in recipientAddress)
            {
                string userPath = dataPath + a.Address + ".txt";
                if (File.Exists(userPath))
                {
                    foreach (Address b in Read(a.Address, userPath).User.Addresses)
                    {
                        foreach (BlackEmailAddress c in b.Blacklist)
                        {
                            if (c.Address == sender)
                            {
                                collectionSpammed.Add(c.Address);
                                break;
                            }
                        }
                    }
                }

            }
            return collectionSpammed;
        }
    
        public void ModBlacklistAddress(Address address,string _blacklistedAddress,string _blacklistMod)
{
 	foreach(BlackEmailAddress b in address.Blacklist)
    {
        if(b.Address == _blacklistedAddress)
        {
            if(_blacklistMod == "fuck") b.IsFucking = true;

            else b.IsFucking = false;
            break;
        }
    }
}
        
        public void RemoveAddress(Address a)
        {
            Address addressToRemove = a;
            foreach(Address address in _addresses)
            {
                if (a.AddressDescription == address.AddressDescription && a.RelayAddress == address.RelayAddress && a.SubscriptionAddress == address.SubscriptionAddress)
                {
                    addressToRemove = address;
                }
            }
            _addresses.Remove(addressToRemove);
        }
    }
}
