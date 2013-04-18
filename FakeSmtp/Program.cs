using System;
using System.Configuration;
using System.Net;
using System.Threading;



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
            MailListener listener = null;
            IPAddress ipadress;
            ipadress = IPAddress.Parse(ConfigurationManager.AppSettings["hostAdressReception"]);
            do
            {
                int receptionPort = Convert.ToInt32(ConfigurationManager.AppSettings["receptionPort"]);

                Console.WriteLine("New MailListener started");
                listener = new MailListener(this, ipadress, receptionPort);
                listener.OutputToFile = true;
                listener.Start();
                while (listener.IsThreadAlive)
                {
                    Thread.Sleep(500);
                }
            } while (listener != null);

        }
    }
}
