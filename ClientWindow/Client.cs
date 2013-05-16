using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Configuration;


namespace ClientWindow
{
    public class Client
    {
        public Client()
        {
        }

        public void Connect(string message)
        {
            string output = "";
            System.IO.StreamReader reader;
            System.IO.StreamWriter writer;
            NetworkStream stream;
            TcpClient client;
            
            string serverIP = ConfigurationManager.AppSettings["ServerIP"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

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
                    writer.WriteLine(message);
                    output = reader.ReadLine();
                    if (output != "250 OK")
                    {
                        MessageBox.Show("Sever Error");
                    }
                }

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                output = "ArgumentNullException: " + e;
                MessageBox.Show(output);
            }
            catch (SocketException e)
            {
                output = "SocketException: " + e.ToString();
                MessageBox.Show(output);
            }
        }
    }
}
