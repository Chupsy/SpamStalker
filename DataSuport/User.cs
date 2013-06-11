﻿using System;
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
            if (writeUserName) stream.WriteLine("name: {0} ", Username);
            stream.WriteLine("password: {0} ", Password);
            stream.WriteLine("account: {0}", AccountType);
            stream.WriteLine();
            foreach (Address a in Addresses) a.Write(stream);
        }

        public static UserReadInfo Read(string username, string dataPath)
        {
            string userPath = Path.Combine(dataPath, username + ".txt");
            using (StreamReader stream = new StreamReader(userPath))
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
            if (ParseLine( line, "userName", out userName)) line = reader.ReadLine();
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





        public static bool CreateUser(string username, string password, string newAdress, string AccountType, string path)
        {
            string directoryPath = path;
            string _description = "Main adress";
            string fileUser = Path.Combine(directoryPath, username + ".txt");
            User newUser = new User(username, password, AccountType);
            List<Address> data = new List<Address>();
            newUser.Addresses = new List<Address>();
            AddAdress(newUser, newAdress, _description, newAdress);

            if (!Directory.Exists(directoryPath))
            {
                newUser.Write(path);
            }
            else if (!File.Exists(fileUser))
            {
                newUser.Write(directoryPath);
            }
            else
            {
                newUser.Write(directoryPath);
            }

            if (GetInfo(username, path) != null)
            {
                return true;
            }
            else return false;
        }

        public static bool UserIsValid(string username, string password, string accounType, string directorypath)
        {
            User user = Read(username, directorypath);
            if (string.IsNullOrEmpty(username) || 
                string.IsNullOrEmpty(password) || 
                string.IsNullOrEmpty(accounType)) return false;
            else return true;
        }

        public static User AddBlacklist(string username, string blacklistFrom, string addressToBlacklist, string datapath)
        {
            User user = Read(username, datapath);
            foreach (Address a in user.Addresses)
            {

                if (a.SubscriptionAddress.Address == blacklistFrom)
                {
                    a.AddressBlacklist.list.Add(new BlackEmailAddress(addressToBlacklist, false));
                    break;
                }
            }
            return user;
        }

        public static User ModifBlacklistedAddress(string username, string blacklistFrom, string addressToModify, string datapath, bool newStatus)
        {
            User u = Read(username, datapath);
            foreach (Address a in u.Addresses)
            {
                if (a.SubscriptionAddress.Address == blacklistFrom)
                {
                    foreach (BlackEmailAddress b in a.AddressBlacklist.list)
                    {
                        if (b.Address == addressToModify)
                        {
                            b.IsFucking = newStatus;
                        }
                    }
                }
            }
            return u;
        }

        public static bool CheckSpammer(string username, string blackListFrom, string blacklistedAddress, string dataPath)
        {
            User User = Read(username, dataPath);
            foreach (Address a in User.Addresses)
            {
                if (a.SubscriptionAddress.Address == blackListFrom)
                {
                    foreach (BlackEmailAddress b in a.AddressBlacklist.list)
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

        public static User RemoveBlacklistedAdress(string username, string blackListFrom, string addressToRemove, string datapath)
        {
            User u = Read(username, datapath);
            Address a = u.FindAddress(blackListFrom);
            if( a != null ) a.Blacklist.Remove(addressToRemove);



            foreach (Address a in u.Addresses)
            {
                if (a.SubscriptionAddress.Address == blackListFrom)
                {
                    foreach (BlackEmailAddress b in a.AddressBlacklist.list)
                    {
                        if (b.Address == addressToRemove)
                        {
                            a.AddressBlacklist.list.Remove(b);
                        }
                    }
                }
            }
            return u;
        }

        public static User AddAdress(User User, string address, string description, string relayAddress)
        {

            EmailAddress userAddress = new EmailAddress(address);
            Description userDescription = new Description(description);
            RelayAddress userRelayAddress = new RelayAddress(relayAddress);
            User.Addresses.Add(new Address(userAddress, null, userDescription, userRelayAddress));
            return User;
        }

        public static User RemoveAdress(User User, string address)
        {
            foreach (Address a in User.Addresses)
            {
                if (a.SubscriptionAddress.Address == address)
                {
                    User.Addresses.Remove(a);
                    break;
                }
            }
            return User;
        }

        public static User GetData(User user, string path)
        {
            string fileUser = Path.Combine(path, user._username + ".txt");
            EmailAddress mailUser;
            Description description;
            RelayAddress relayAddress;
            List<BlackEmailAddress> blacklist;
            List<Address> data = new List<Address>();


            if (File.Exists(fileUser))
            {

                string[] userData = File.ReadAllLines(fileUser);
                for (int i = 3; i < userData.Length; i++)
                {
                    if (userData[i] != null && userData[i].Trim().StartsWith("address"))
                    {
                        mailUser = new EmailAddress(userData[i].Trim().Substring(userData[i].IndexOf(":") + 1).Trim());
                        i++;
                        if (userData[i] != null && userData[i].Trim().StartsWith("description"))
                        {
                            description = new Description(userData[i].Trim().Substring(userData[i].IndexOf(":") + 1).Trim());
                            i++;

                            if (userData[i] != null && userData[i].Trim().StartsWith("relay address"))
                            {
                                relayAddress = new RelayAddress(userData[i].Trim().Substring(userData[i].IndexOf(":") + 1).Trim());
                                i++;

                                if (userData[i] != null && userData[i].Trim().StartsWith("blacklist"))
                                {
                                    blacklist = new List<BlackEmailAddress>();

                                    while (i < userData.Length && userData[i] != "")
                                    {

                                        if (userData[i].Trim().StartsWith("ignore"))
                                        {
                                            blacklist.Add(new BlackEmailAddress(userData[i].Trim().Substring(userData[i].IndexOf(":") + 1).Trim(), false));
                                        }
                                        else if (userData[i].Trim().StartsWith("fuck"))
                                        {
                                            blacklist.Add(new BlackEmailAddress(userData[i].Trim().Substring(userData[i].IndexOf(":") + 1).Trim(), false));
                                        }
                                        i++;
                                    }
                                    data.Add(new Address(mailUser, new Blacklist(blacklist), description, relayAddress));
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            user.Addresses = data;
            return user;
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



        public static string GetAllInformations(string username, string dataPath)
        {
            string path = Path.Combine(dataPath, username + ".txt");

            return File.ReadAllText(path).Replace(Environment.NewLine, "/"); ;
        }

        public static bool CheckAddress(string address, string datapath)
        {
            foreach (string s in Directory.GetFiles(datapath))
            {
                string username = s.Trim().Substring((s.LastIndexOf("\\") + 1)).Trim();
                username = username.Substring(0, username.IndexOf("."));
                User u = User.Read(username, datapath);
                foreach (Address a in u.Addresses)
                {
                    if (a.SubscriptionAddress.Address == address)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public static bool CheckAddressBelonging(string username, string belongAddress, string dataPath)
        {
            User u = User.Read(username, dataPath);
            foreach (Address a in u.Addresses)
            {
                if (a.SubscriptionAddress.Address == belongAddress)
                {
                    return true;
                }
            }
            return false;
        }

        public static System.Net.Mail.MailAddressCollection CheckSpammer(System.Net.Mail.MailAddressCollection recipientAddress, string sender, string dataPath)
        {
            MailAddressCollection collectionSpammed = new MailAddressCollection();
            foreach (MailAddress a in recipientAddress)
            {
                string userPath = dataPath + a.Address + ".txt";
                if (File.Exists(userPath))
                {
                    foreach (Address b in Read(a.Address, userPath).Addresses)
                    {
                        foreach (BlackEmailAddress c in b.AddressBlacklist.list)
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
    }
}
