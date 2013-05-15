using System;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Net.Mail;
using System.Net.Sockets;


namespace FakeSmtp
{
    public class SMTPServer
    {
        [STAThread] 
        static void Main(string[] args)
        {
            SMTPServer server = new SMTPServer();
            server.RunServer();
        }

        public void RunServer()
        {
            ProtocolHandler dialogueSession = null;
            Thread threadSession = null;

            IPAddress ipadress;
            ipadress = IPAddress.Parse(ConfigurationManager.AppSettings["hostAdressReception"]);
            int receptionPort = Convert.ToInt32(ConfigurationManager.AppSettings["receptionPort"]);
            
            TcpListener listener = new TcpListener(ipadress, receptionPort);
            listener.Start();

            do
            {
                dialogueSession = new ProtocolHandler(listener.AcceptTcpClient());
                threadSession = new System.Threading.Thread(new ThreadStart(dialogueSession.Start));
                threadSession.Start();
            } while (dialogueSession != null);

        }
    }
}
