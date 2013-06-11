using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using DataSupport;


namespace ClientWindow
{
    public class Client
    {

        System.IO.StreamReader reader;
        System.IO.StreamWriter writer;
        NetworkStream stream;
        TcpClient client;

        string serverIP = ConfigurationManager.AppSettings["ServerIP"];
        int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

        public Client()
        {
        }

        public void Send(string message)
        {
            writer.WriteLine(message);
        }

        public string Waitresponse()
        {
            string output;
            output = reader.ReadLine();
            return(output);
        }



        public bool Connect(string login, string pass)
        {
            string output = "";

            try
            {
               
                client = new TcpClient(serverIP, port);
                stream = client.GetStream();
                reader = new System.IO.StreamReader(stream);
                writer = new System.IO.StreamWriter(stream);
                writer.AutoFlush = true;
                output = reader.ReadLine();

                if (output == "220 <SpamStalker> Service ready")
                {
                    writer.WriteLine("!EHLO " + login +" "+pass);
                    output = reader.ReadLine();
                    if (output != "250 OK")
                    {
                        MessageBox.Show("Sever Error");
                        return false;
                    }
                    return true;
                }
                return false;     
            }
            catch (ArgumentNullException e)
            {
                output = "ArgumentNullException: " + e;
                MessageBox.Show(output);
                return false;
            }
            catch (SocketException e)
            {
                output = "SocketException: " + e.ToString();
                MessageBox.Show(output);
                return false;
            }
        }

        public User GetData()
        {
            writer.WriteLine("!GETA");
            User user = User.Read(reader).User;
            return(user);
        }

        public void CloseStream()
        {
            stream.Close();
            client.Close();
        }
    }
}
