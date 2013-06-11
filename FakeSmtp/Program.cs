using System;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Net.Mail;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using SMTPSupport;
using DataSupport;


namespace FakeSmtp
{
    public class SMTPServer
    {

        TcpListener _listener;
        ServerStatus _status;
        string dataPath;
        List<User> _users;

        public List<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        [STAThread]
        static void Main(string[] args)
        {
            SMTPServer server = new SMTPServer();
            server.RunServer();
        }

        public void RunServer()
        {
            _users = new List<User>();

            dataPath = ConfigurationManager.AppSettings["dataDirectory"];
            dataPath = Path.Combine(dataPath, "User\\");
            IPAddress ipadress;
            ipadress = IPAddress.Parse(ConfigurationManager.AppSettings["hostAdressReception"]);
            int receptionPort = Convert.ToInt32(ConfigurationManager.AppSettings["receptionPort"]);

            #region Admin System file creation


            Directory.CreateDirectory(dataPath);
            string fileSystem = dataPath + "system" + ".txt";
            if (!File.Exists(fileSystem))
            {
                User.CreateUser("system", "password", "coucou@hotmail.com", "admin").Write(dataPath);

            }

            #endregion

            #region User Load
            

            foreach (string u in Directory.GetFiles(dataPath))
            {
                User user = User.Read(u).User;
                user.Username = u.Trim().Substring(u.Trim().LastIndexOf("\\")+1, u.Trim().Substring(u.Trim().LastIndexOf("\\")+1).Length -4);
                _users.Add(user);
            }

            #endregion

            _listener = new TcpListener(ipadress, receptionPort);
            _listener.Start();
            List<Thread> allThreads = new List<Thread>();
            do
            {
                TcpClient client;
                try
                {
                    client = _listener.AcceptTcpClient();
                }
                catch (SocketException ex)
                {
                    if (ex.ErrorCode == 10004 && _status == ServerStatus.ShuttingDown) break;
                    throw;
                }
                ProtocolHandler p = new ProtocolHandler(client);
                Thread t = new Thread(p.Start);
                allThreads.Add(t);
                p.AddServer(this);
                t.Start();

                for (int i = 0; i < allThreads.Count; ++i)
                {
                    if (!allThreads[i].IsAlive) allThreads.RemoveAt(i--);
                }
            }
            while (_status != ServerStatus.ShuttingDown);
            _listener.Stop();
            foreach (Thread t in allThreads) t.Join();

        }



        #region MetaCommandAPI Functions
        public void ShutDown()
        {
            if (_status != ServerStatus.ShuttingDown)
            {
                _listener.Stop();
                _status = ServerStatus.ShuttingDown;
            }
        }

        public void Pause()
        {
        }

        public void Resume()
        {
        }



        public void DeleteUser(string username)
        {

            string path = Path.Combine(dataPath, username + ".txt");
            File.Delete(path);
        }

        public bool CheckUser(string username)
        {
            string path = Path.Combine(dataPath, username + ".txt");
            return File.Exists(path);
        }

        public ServerStatus Status { get { return _status; } }



        public MailAddressCollection CheckAllSpammer(MailAddressCollection recipientAddress, string sender)
        {
            MailAddressCollection blacklister = new MailAddressCollection();
            foreach (User u in _users)
            {
                foreach (Address a in u.Addresses)
                {
                    foreach(MailAddress m in recipientAddress)
                    {
                        if(m.Address == a.SubscriptionAddress)
                        {
                            foreach (BlackEmailAddress b in a.Blacklist)
                            {
                                if (b.Address == sender)
                                {
                                    blacklister.Add(m);
                                }
                            }
                        }
                    }

                }
            }
            return blacklister;
        }
        #endregion


        public Address FindUserAddress(string subscriptionAddress)
        {
            foreach (User u in _users)
            {
                foreach (Address a in u.Addresses)
                {
                    if (a.SubscriptionAddress == subscriptionAddress) return a;
                }
            }
            return null;
        }

        public bool CheckUserExist(string username)
        {
            string userPath = Path.Combine(dataPath, username + ".txt");
            return File.Exists(userPath);
        }


        public User FindUser(string username)
        {
            return User.Read(username, dataPath).User;
        }

        
        public void Write(User u)
        {
            u.Write(dataPath);
        }
    }
}
