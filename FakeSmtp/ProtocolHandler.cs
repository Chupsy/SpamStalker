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
    public class ProtocolHandler
    {
        private TcpClient _client;
        private NetworkStream stream;
        private System.IO.StreamReader reader;
        private System.IO.StreamWriter writer;
        SmtpClient smtpDestination ;
        SMTPParser parser;
        SMTPSession session;
        SMTPCallingClient callingClient;
        string hostDestination = ConfigurationManager.AppSettings["hostAdressDestination"];
        int destinationPort = Convert.ToInt32(ConfigurationManager.AppSettings["destinationPort"]);

        public ProtocolHandler(TcpClient client)
        {
            _client = client;
            smtpDestination = new SmtpClient(hostDestination, destinationPort);
        }

        public void Start()
        {
            _client.ReceiveTimeout = 50000;
            stream = _client.GetStream();
            reader = new System.IO.StreamReader(stream);
            writer = new System.IO.StreamWriter(stream);
            writer.NewLine = "\r\n";
            writer.AutoFlush = true;

            session = new SMTPSession();
            parser = new SMTPParser();
            callingClient = new SMTPCallingClient(reader, writer, session, _client);


            callingClient.SendError(ErrorCode.Ready);

            try
            {
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
                _client.Close();

            }
            catch (IOException)
            {
                Console.WriteLine("Connection lost.");
                _client.Close();
            }
        }

    }
}
