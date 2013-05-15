using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ClientWindow
{
    public class Client
    {
        public Client()
        {
        }

        public void Connect(string serverIP, string message)
        {
            string output = "";
            System.IO.StreamReader reader;
            System.IO.StreamWriter writer;

            try
            {
                // Create a TcpClient.
                // The client requires a TcpServer that is connected
                // to the same address specified by the server and port
                // combination.
               
                Int32 port = 25;
                TcpClient client = new TcpClient(serverIP, port);

                // Get a client stream for reading and writing.
                // Stream stream = client.GetStream();
                NetworkStream stream = client.GetStream();
                //writer = new System.IO.StreamWriter(stream);
                //reader = new System.IO.StreamReader(stream);

                //output = "Sent: " + message;
                //MessageBox.Show(output);
                //output = reader.ReadLine();
                //MessageBox.Show(output);
                //writer.WriteLine(message);
                //MessageBox.Show(output);
                //output = reader.ReadLine();
                //MessageBox.Show(output);


                // Translate the passed message into ASCII and store it as a byte array.
                Byte[] data = new Byte[256];
                data = System.Text.Encoding.ASCII.GetBytes(message);

                

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

              

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                output = "Received: " + responseData;
                MessageBox.Show(output);

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
