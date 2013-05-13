using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace ClientListner
{
    class SocketHelper
    {
        TcpClient mscClient;
        string mstrMessage;
        string mstrResponse;
        byte[] bytesSent;
        public void processMsg(TcpClient client, NetworkStream stream, byte[] bytesReceived)
        {
            // Handle the message received and 
            // send a response back to the client.
            mstrMessage = Encoding.ASCII.GetString(bytesReceived, 0, bytesReceived.Length);
            mscClient = client;
            //mstrMessage = mstrMessage.Substring(0, 5);
            Console.WriteLine("input: {0}",mstrMessage);
            if (mstrMessage.Equals("Hello"))
            {
                mstrResponse = "Goodbye";
            }
            else
            {
                mstrResponse = "What?";
            }
            Console.WriteLine("output: {0}", mstrResponse);
            bytesSent = Encoding.ASCII.GetBytes(mstrResponse);
            stream.Write(bytesSent, 0, bytesSent.Length);
        }
    }
}
