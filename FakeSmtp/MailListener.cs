using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Configuration;

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
        private bool test;
        const string SUBJECT = "Subject: ";
        const string FROM = "From: ";
        const string TO = "To: ";
        const string MIME_VERSION = "MIME-Version: ";
        const string DATE = "Date: ";
        const string CONTENT_TYPE = "Content-Type: ";
        const string CONTENT_TRANSFER_ENCODING = "Content-Transfer-Encoding: ";
        string hostDestination = ConfigurationManager.AppSettings["hostAdressDestination"];
        int destinationPort = Convert.ToInt32(ConfigurationManager.AppSettings["destinationPort"]);
        bool sendAuthorization;
        MailTransfer sendMail;
        


        public MailListener(SMTPServer aOwner, IPAddress localaddr, int port)
            : base(localaddr, port)
        {
            owner = aOwner;
            OutputDirectory = "mail/";
            sendAuthorization = true;
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
            thread.Start();
        }

        protected void RunThread()
        {
            string line = null;
            string sender = null;
            string recipient = null;
            writer.WriteLine("220 localhost -- Fake proxy server");
            sendAuthorization = true;
            try
            {
                while (reader != null)
                {
                    line = reader.ReadLine();
                    Console.Error.WriteLine("Read line {0}", line);
                    string linecases = line;
                    if (line.Length > 3)
                    {
                        linecases = line.Substring(0, 4);
                    }

                    switch (linecases)
                    {
                        case "RCPT": 
                            recipient = "";
                            string[] adresses = System.IO.File.ReadAllLines(@"..\..\adresses.txt");
                            recipient = line.Substring("RCPT TO:<".Length);
                            recipient = recipient.Remove(recipient.Length - 1);
                            test = false;
                            foreach (string adress in adresses)
                            {
                                if (adress == recipient)
                                {
                                    writer.WriteLine("250 OK");
                                    test = true;
                                    break;
                                }
                            }
                            if (test == false)
                            {
                                writer.WriteLine("550 No such user here ");
                            }
                            break;

                        case "MAIL":
                            sender = "";
                            string[] senderAdresses = System.IO.File.ReadAllLines(@"..\..\senderAdresses.txt");
                            sender = line.Substring("MAIL FROM:<".Length);
                            sender = sender.Remove(sender.Length - 1);
                            foreach (string senderAdress in senderAdresses)
                            {
                                if (senderAdress == sender)
                                {
                                    sendAuthorization = false;
                                    writer.WriteLine("Importuner");
                                    break;
                                }
                            }
                                writer.WriteLine("251 OK");
                            break;
                            
                        case "DATA":
                            writer.WriteLine("354 Start input, end data with <CRLF>.<CRLF>");
                            StringBuilder data = new StringBuilder();
                            string subject = "";
                            string from = "";
                            string to = "";
                            string mimeVersion = "";
                            string date = "";
                            string contentType = "";
                            string contentTransferEncoding = "";
                            string content = "";

                            line = reader.ReadLine();

                            while (line != null && line != ".")
                            {
                                if (line.StartsWith(SUBJECT))
                                {
                                    subject = line.Substring(SUBJECT.Length);
                                }
                                else if (line.StartsWith(FROM))
                                {
                                    from = line.Substring(FROM.Length);
                                }
                                else if (line.StartsWith(TO))
                                {
                                    to = line.Substring(TO.Length);
                                }
                                else if (line.StartsWith(MIME_VERSION))
                                {
                                    mimeVersion = line.Substring(MIME_VERSION.Length);
                                }
                                else if (line.StartsWith(DATE))
                                {
                                    date = line.Substring(DATE.Length);
                                }
                                else if (line.StartsWith(CONTENT_TYPE))
                                {
                                    contentType = line.Substring(CONTENT_TYPE.Length);
                                }
                                else if (line.StartsWith(CONTENT_TRANSFER_ENCODING))
                                {
                                    contentTransferEncoding = line.Substring(CONTENT_TRANSFER_ENCODING.Length);
                                }
                                else
                                {
                                    content += line;
                                }

                                line = reader.ReadLine();
                            }

                            String message = data.ToString();
                            writer.WriteLine(recipient, " ", sender, " ", subject, " ");
                            WriteMessage(from, to, subject, message, contentType, contentTransferEncoding);

                            if (recipient != null && sender != null && subject != null && content != null && hostDestination != null && destinationPort >= 0 && sendAuthorization == true)
                            {
                                sendMail = new MailTransfer(recipient, sender, "noreply@jaimelesgauffres.com", subject, content, hostDestination, destinationPort);
                                sendMail.sendMessage();
                            }
                            writer.WriteLine("250 OK");
                            break;

                        case "RSET":
                            recipient = "";
                            sender = "";
                            subject = "";
                            from = "";
                            to = "";
                            mimeVersion = "";
                            date = "";
                            contentType = "";
                            contentTransferEncoding = "";
                            content = "";

                            writer.WriteLine("250 OK");
                            break;

                        case "QUIT":
                            writer.WriteLine("250 OK");
                            reader = null;
                            break;

                        default:
                            Thread.Sleep(1);
                            writer.WriteLine("550 command unknown");
                            break;
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Connection lost.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                client.Close();
                Stop();
            }
        }

        private string DecodeQuotedPrintable(string input)
        {
            var occurences = new Regex(@"(=[0-9A-Z][0-9A-Z])+", RegexOptions.Multiline);
            var matches = occurences.Matches(input);
            foreach (Match m in matches)
            {
                byte[] bytes = new byte[m.Value.Length / 3];
                for (int i = 0; i < bytes.Length; i++)
                {
                    string hex = m.Value.Substring(i * 3 + 1, 2);
                    int iHex = Convert.ToInt32(hex, 16);
                    bytes[i] = Convert.ToByte(iHex);
                }
                input = input.Replace(m.Value, Encoding.Default.GetString(bytes));
            }
            return input.Replace("=\r\n", "");
        }

        private void WriteMessage(string from, string to, string subject, string message, string contentType, string transferEncoding)
        {
            if (transferEncoding == "quoted-printable")
            {
                message = DecodeQuotedPrintable(message);
            }

            if (OutputToFile)
            {
                string header = string.Format("<strong>FROM: </strong>{0}<br/><strong>TO: </strong>{1}<br/><strong>SUBJECT: </strong>{2}<br/><br/>",
                    new object[] { from, to, subject });
                string docText = string.Format("<html><body>{0}{1}</body></html>", header, message);

                if (string.IsNullOrEmpty(OutputDirectory) == false && Directory.Exists(OutputDirectory) == false)
                {
                    Directory.CreateDirectory(OutputDirectory);
                }

                // Create a file to write to.
                string path = string.Format(@"{0}mail_{1}.html", OutputDirectory, DateTime.Now.ToFileTimeUtc());
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(docText);
                }
            }

            Console.Error.WriteLine("===============================================================================");
            Console.Error.WriteLine("Received ­email");
            Console.Error.WriteLine("Type: " + contentType);
            Console.Error.WriteLine("Encoding: " + transferEncoding);
            Console.Error.WriteLine("From: " + from);
            Console.Error.WriteLine("To: " + to);
            Console.Error.WriteLine("Subject: " + subject);
            Console.Error.WriteLine("-------------------------------------------------------------------------------");
            Console.Error.WriteLine(message);
            Console.Error.WriteLine("===============================================================================");
            Console.Error.WriteLine("");
        }

        public bool OutputToFile { get; set; }

        public string OutputDirectory { get; set; }

        public bool IsThreadAlive
        {
            get { return thread.IsAlive; }
        }
    }
}
