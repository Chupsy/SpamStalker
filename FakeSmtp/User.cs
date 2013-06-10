using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;
using System.IO;
using System.Configuration;

namespace FakeSmtp
{
    class User
    {
        string _username;
        string _password;
        string _accountType;
        List<Address> _data;
        string dataPath;

        public User(string username, string password, string accountType)
        {
            _username = username;
            _password = password;
            _accountType = accountType;
        }

        public List<Address> Data
        {
            get{ return _data;}
            set{_data = value;}
        }

        static User GetInfo(string username)
        {
            string password;
            string accountType;

            string fileUser = ConfigurationManager.AppSettings["dataDirectory"] + "\\User\\" + username + ".txt";
            
            if (File.Exists(fileUser))
            {

                string[] userData = File.ReadAllLines(fileUser);
                if (userData[0] != null && userData[0].Trim().StartsWith("password"))
                {
                    password = userData[0].Trim().Substring(userData[0].IndexOf(":"));

                    if (userData[1] != null && userData[0].Trim().StartsWith("account"))
                    {
                        accountType = userData[0].Trim().Substring(userData[1].IndexOf(":"));
                        return new User(username, password, accountType);                   
                    }
                    
                }
            }
            return null;
        }

        static User GetData(User user)
        {
            string fileUser = ConfigurationManager.AppSettings["dataDirectory"] + "\\User\\" + user._username + ".txt";
            EmailAddress mailUser;
            Description description;
            RelayAddress relayAddress;

            if (File.Exists(fileUser))
            {

                string[] userData = File.ReadAllLines(fileUser);
                for (int i = 3; i < userData.Length; i++)
                {
                    if (userData[i] != null && userData[i].Trim().StartsWith("address"))
                    {
                        mailUser = new EmailAddress(userData[i].Trim().Substring(userData[i].IndexOf(":")));
                        i++;
                        if (userData[i] != null && userData[i].Trim().StartsWith("description"))
                        {
                            description = new Description(userData[i].Trim().Substring(userData[i].IndexOf(":")));
                            i++;

                            if (userData[i] != null && userData[i].Trim().StartsWith("relay address"))
                            {
                                relayAddress = new RelayAddress(userData[i].Trim().Substring(userData[i].IndexOf(":")));
                                i++;

                                if (userData[i] != null && userData[i].Trim().StartsWith("blacklist"))
                                {
                                    i++;
                                    while (userData[i] != "")
                                    {
                                        if (userData[i].Trim().StartsWith("ignore") || userData[i].Trim().StartsWith("fuck"))
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return user;
        }
    }
}
