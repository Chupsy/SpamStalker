using System;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Net.Mail;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using SMTPSupport;


namespace FakeSmtp
{
    public class SMTPServer
    {
        
        TcpListener _listener;
        ServerStatus _status;
        string dataPath;

        [STAThread] 
        static void Main(string[] args)
        {
            SMTPServer server = new SMTPServer();
            server.RunServer();
        }

        public void RunServer()
        {
            dataPath = ConfigurationManager.AppSettings["dataDirectory"]+ "\\User";
            IPAddress ipadress;
            ipadress = IPAddress.Parse(ConfigurationManager.AppSettings["hostAdressReception"]);
            int receptionPort = Convert.ToInt32(ConfigurationManager.AppSettings["receptionPort"]);

            #region Admin System file creation

            
            Directory.CreateDirectory(dataPath);
            string fileSystem = dataPath + "\\System.txt";
            if (!File.Exists(fileSystem))
            {
                File.Create(fileSystem);
                StreamWriter stream = new StreamWriter(@fileSystem);
                stream.Write("System Password Admin");
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
                    if (ex.ErrorCode == 995 && _status == ServerStatus.ShuttingDown ) break;
                    throw;
                }
                ProtocolHandler p = new ProtocolHandler( client );
                Thread t = new Thread( p.Start );
                allThreads.Add(t);
                t.Start();
                for (int i = 0; i < allThreads.Count; ++i )
                {
                    if (!allThreads[i].IsAlive) allThreads.RemoveAt( i-- );
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

        public void CreateUser(string username, string password, string newAdress, string accountType)
        {
            string description = "Main adress from server";
            string fileUser = dataPath + "\\" + username + ".txt";
            if (!File.Exists(fileUser))
            {
                File.Create(fileUser);
                StreamWriter stream = new StreamWriter(@fileUser);
                string line = username + " " + password + " " + accountType;
                stream.Write(line);
            }
            AddAddress(username, newAdress, newAdress , description); 
        }

        public void RemoveAddress(string address, string username)
        {
            User.Write(User.RemoveAdress(User.SetUser(username, dataPath), address), dataPath);
        }

        public void AddBlacklistAddress(string username, string referenceAdress, string blackListedAdress)
        {
            string fileAdress = Directory.GetCurrentDirectory().ToString() + "\\Users\\" + username + "\\" + referenceAdress + ".txt";            
            
        }

        public void AddAddress(string username, string newAdress, string relayAdress, string description)
        {
            User.Write(User.AddAdress(User.SetUser(username, dataPath), newAdress, description, relayAdress), dataPath);
        }

        public void DeleteUser(string username)
        {
            string path = dataPath + "\\Users\\" + username + ".txt";
            File.Delete(path);
        }

        public bool CheckUser(string username)
        {
            string path = dataPath + "\\Users\\" + username + ".txt";
            return File.Exists(path);
        }

        public string Identify(string username, string password)
        {
            User user = User.GetInfo(username, dataPath);
            if (user.Username == username && user.Password == password)
            {
                return user.AccountType;
            }
            return null;
        }

        public void ModifyType(string username, string type)
        {
            User.ModifyType(User.SetUser(username, dataPath), dataPath, type);
        }

        public User SetUser(string username)
        {
            return User.SetUser(username, dataPath);
        }
        public void ModifyPassword(string username, string password)
        {
            User.ModifyPassword(User.SetUser(username, dataPath), dataPath, password);
        }

        public ServerStatus Status { get { return _status; } }

        #endregion
    }
}
