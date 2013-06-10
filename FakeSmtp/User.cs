using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;
using System.Configuration;
using System.IO;

namespace FakeSmtp
{
    class User
    {
        string _username;
        string _password;
        string _accountType;
        List<Address> _data;

        public User(string username, string password, string accountType)
        {
            _username = username;
        }

        static bool CreateUser(string username, string password, string newAdress, string AccountType)
        {
            string _dataPath = ConfigurationManager.AppSettings["dataDirectory"];
            _dataPath = _dataPath + "\\User\\";
            string _description = "Main adress";
            string fileUser = _dataPath + username + ".txt";
            string _passLine = "Password = " + password;
            string _accountTypeLine = "Account = " + AccountType;

            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
                File.Create(fileUser);
            }
            else if (!File.Exists(fileUser))
            {
                File.Create(fileUser);
            }

            StreamWriter stream = new StreamWriter(@fileUser);
            stream.WriteLine(_passLine);
            stream.WriteLine(_accountTypeLine);
            stream.Close();

            AddAdress(newAdress, _description, newAdress);
            return true;
        }

        static void AddAdress(string adress, string description, string relayAdress)
        {
            
        }
    }
}
