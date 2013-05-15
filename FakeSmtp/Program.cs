using System;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Net.Mail;
using System.Net.Sockets;
using System.Collections.Generic;


namespace FakeSmtp
{
    public class SMTPServer
    {
        bool _shutdown;

        [STAThread] 
        static void Main(string[] args)
        {
            SMTPServer server = new SMTPServer();
            server.RunServer();
        }

        public void RunServer()
        {


            IPAddress ipadress;
            ipadress = IPAddress.Parse(ConfigurationManager.AppSettings["hostAdressReception"]);
            int receptionPort = Convert.ToInt32(ConfigurationManager.AppSettings["receptionPort"]);
            
            TcpListener listener = new TcpListener(ipadress, receptionPort);
            listener.Start();
            List<Thread> allThreads = new List<Thread>();
            do
            {
                TcpClient client = listener.AcceptTcpClient();
                ProtocolHandler p = new ProtocolHandler( client );
                Thread t = new Thread( p.Start );
                allThreads.Add(t);
                t.Start();
                for (int i = 0; i < allThreads.Count; ++i )
                {
                    if (!allThreads[i].IsAlive) allThreads.RemoveAt( i-- );
                }
            } 
            while (!_shutdown);
            listener.Stop();
            foreach (Thread t in allThreads) t.Join();

        }
    }
}
