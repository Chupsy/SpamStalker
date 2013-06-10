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
    public class User
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

        public List<Address> Data
        {
            get{ return _data;}
            set{_data = value;}
        }

        public static User GetInfo(string username, string path)
        {
            string password;
            string accountType;

            string fileUser = path + "\\User\\" + username + ".txt";
            
            if (File.Exists(fileUser))
            {

                string[] userData = File.ReadAllLines(fileUser);
                if (userData[0] != null && userData[0].Trim().StartsWith("password"))
                {
                    password = userData[0].Trim().Substring(userData[0].IndexOf(":") + 1).Trim();

                    if (userData[1] != null && userData[1].Trim().StartsWith("account"))
                    {
                        accountType = userData[1].Trim().Substring(userData[1].IndexOf(":") + 1).Trim();
                        return new User(username, password, accountType);                   
                    }
                    
                }
            }
            return null;
        }

        public static User GetData(User user, string path)
        {
            string fileUser = path + "\\User\\" + user._username + ".txt";
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
                                            blacklist.Add(new BlackEmailAddress(userData[i].Trim().Substring(userData[i].IndexOf(":")+1).Trim(), false));
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
            user.Data = data;
            return user;
        }
    }
}
