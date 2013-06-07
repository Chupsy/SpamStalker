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
        
        bool _shutdown;
        TcpListener _listener;
        ServerStatus _status;
        string _dataPath;

        [STAThread] 
        static void Main(string[] args)
        {
            SMTPServer server = new SMTPServer();
            server.RunServer();
        }

        public void RunServer()
        {
            _dataPath = ConfigurationManager.AppSettings["dataDirectory"];
            IPAddress ipadress;
            ipadress = IPAddress.Parse(ConfigurationManager.AppSettings["hostAdressReception"]);
            int receptionPort = Convert.ToInt32(ConfigurationManager.AppSettings["receptionPort"]);

            #region Admin System file creation

            
            Directory.CreateDirectory(_dataPath);
            string path = _dataPath + "\\System";
            Directory.CreateDirectory(path);
            string fileSystem = path + "\\Informations.txt";
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
            string path = _dataPath + "\\Users\\" + username;
            Directory.CreateDirectory(path);
            string fileUser = path + "\\Informations.txt";
            if (!File.Exists(fileUser))
            {
                File.Create(fileUser);
                StreamWriter stream = new StreamWriter(@fileUser);
                string line = username + " " + password + " " + accountType;
                stream.Write(line);
            }
            AddAddress(username, newAdress, newAdress , description); 
        }

        public bool RemoveAddress(string address, string username)
        {
            string fileAdress = _dataPath + "\\Users\\" + username + "\\" + address + ".txt";
            if (File.Exists(fileAdress))
            {
                File.Delete(fileAdress);
                return true;
            }
            return false;
        }

        public void AddBlacklistAddress(string username, string referenceAdress, string blackListedAdress)
        {
            string fileAdress = Directory.GetCurrentDirectory().ToString() + "\\Users\\" + username + "\\" + referenceAdress + ".txt";            
            
        }

        public bool AddAddress(string username, string newAdress, string relayAdress, string description)
        {
            string fileAdress = _dataPath + "\\Users\\" + username + "\\" + newAdress +".txt";
            if (!File.Exists(fileAdress))
            {
                File.Create(fileAdress);
                StreamWriter stream = new StreamWriter(@fileAdress);
                stream.WriteLine(relayAdress);
                stream.WriteLine(description);
                return true;
            }
            else return false;
        }

        public void DeleteUser(string username)
        {
            string path = _dataPath + "\\Users\\" + username;
            Directory.Delete(path, true);
        }

        public bool CheckUser(string username)
        {
            string path = _dataPath + "\\Users\\" + username;
            return Directory.Exists(path);
        }

        public string Identify(string username, string password)
        {
            string fileUser =_dataPath + "\\Users\\" + username + "\\Informations.txt";
            Dictionary<string, string> userInfos = GetIdentity(username);
            if (File.Exists(fileUser) && userInfos != null)
            {
                if (userInfos["username"] == username && userInfos["password"] == password)
                {
                    return userInfos["type"];
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public void ModifyType(string username, string type)
        {
            string fileUser = _dataPath + "\\Users\\" + username + "\\Informations.txt";
            Dictionary<string, string> userInfos = GetIdentity(username);
            if (File.Exists(fileUser) && userInfos != null)
            {
                StreamWriter stream = new StreamWriter(@fileUser);

                string line = userInfos["username"] + " " + userInfos["password"] + " " + type;
                stream.Write(line);
            }
        }

        public void ModifyPassword(string username, string password)
        {
            string fileUser = _dataPath + "\\Users\\" + username + "\\Informations.txt";
            Dictionary<string, string> userInfos = GetIdentity(username);
            if (File.Exists(fileUser) && userInfos != null)
            {
                StreamWriter stream = new StreamWriter(@fileUser);

                string line = userInfos["username"] + " " + password +" " + userInfos["type"];
                stream.Write(line);
            }
        }

        private Dictionary<string, string> GetIdentity(string username)
        {
            string fileUser = _dataPath + "\\Users\\" + username + "\\Informations.txt";
            Dictionary<string, string> identity = new Dictionary<string, string>();
            if (File.Exists(fileUser))
            {
                StreamReader reader = new StreamReader(@fileUser);

                string infos = reader.ReadLine();
                identity.Add("username", infos.Trim().Substring(0, infos.IndexOf(" ")));
                identity.Add("password", infos.Trim().Substring(identity["username"].Length).Trim().Substring(0, infos.IndexOf(" ")));
                identity.Add("type", infos.Trim().Substring(infos.LastIndexOf(" ")));
                return identity;
            }
            return null;
        }

        public ServerStatus Status { get { return _status; } }

        #endregion
    }
}
