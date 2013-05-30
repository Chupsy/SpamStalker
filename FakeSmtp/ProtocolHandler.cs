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
        MetaCommandAPI _metaAPI;
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
            _metaAPI = new MetaCommandAPI();
            stream = _client.GetStream();
            reader = new System.IO.StreamReader(stream);
            writer = new System.IO.StreamWriter(stream);
            writer.NewLine = "\r\n";
            writer.AutoFlush = true;

            string testRead;

            session = new SMTPSession( _metaAPI );
            parser = new SMTPParser();
            callingClient = new SMTPCallingClient(reader, writer, session, _client);


            callingClient.SendError(ErrorCode.Ready);

            try
            {
                testRead =  reader.ReadLine();
                if (testRead != null)
                {
                    parser.Execute(testRead, session, callingClient);

                }
                else
                {
                    callingClient.ForceClose();
                }

                while (!callingClient.IsClosed())
                {
                    testRead =  reader.ReadLine();
                    if (testRead != null)
                    {
                        parser.Execute(testRead, session, callingClient);
                    }
                    else
                    {
                        callingClient.ForceClose();
                    }
                }
                if (session.IsReady())
                {
                    try
                    {
                        smtpDestination.Send(session.mail);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else if(!callingClient.IsClosed())
                {
                    callingClient.Close();
                }
                _client.Close();

            }
            catch (IOException ex)
            {
                Console.WriteLine("Connection lost : {0}", ex);
                _client.Close();
            }
        }

    }
}
