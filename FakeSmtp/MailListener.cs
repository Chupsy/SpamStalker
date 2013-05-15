using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Configuration;
using SMTPSupport;
using System.Net.Mail;

namespace FakeSmtp
{
    public class MailListener : TcpListener
    {
        private TcpClient client;
        private NetworkStream stream;
        private System.IO.StreamReader reader;
        private System.IO.StreamWriter writer;
        private Thread thread = null;
        private SMTPServer owner;
        string hostDestination = ConfigurationManager.AppSettings["hostAdressDestination"];
        int destinationPort = Convert.ToInt32(ConfigurationManager.AppSettings["destinationPort"]);
        SmtpClient smtpDestination ;
        SMTPParser parser;
        SMTPSession session;
        SMTPCallingClient callingClient;

        public MailListener(SMTPServer aOwner, IPAddress localaddr, int port)
            : base(localaddr, port)
        {
            smtpDestination = new SmtpClient(hostDestination, destinationPort);
            owner = aOwner;
        }

        new public void Start()
        {
            base.Start();

            client = AcceptTcpClient();
            client.ReceiveTimeout = 50000;
            stream = client.GetStream();
            reader = new System.IO.StreamReader(stream);
            writer = new System.IO.StreamWriter(stream);
            writer.NewLine = "\r\n";
            writer.AutoFlush = true;

            thread = new System.Threading.Thread(new ThreadStart(RunThread));
            session = new SMTPSession();
            parser = new SMTPParser();
            callingClient = new SMTPCallingClient(reader, writer, session, client);
            thread.Start();
        }

        protected void RunThread()
        {
            callingClient.SendError(ErrorCode.Ready);

            try
            {
                Console.WriteLine(reader.ReadLine());
                parser.Execute(reader.ReadLine(), session, callingClient);


                while (!callingClient.IsClosed())
                {
                    parser.Execute(reader.ReadLine(), session, callingClient);
                }
                if (session.IsReady())
                {
                    smtpDestination.Send(session.mail);
                }
                else if(!callingClient.IsClosed())
                {
                    callingClient.Close();
                }
                Stop();

            }
            catch (IOException)
            {
                Console.WriteLine("Connection lost.");
                Stop();
            }
        }

        public bool IsThreadAlive
        {
            get { return thread.IsAlive; }
        }
    }
}
