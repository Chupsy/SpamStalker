using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport.Test
{
    class SMTPClientTest : SMTPCallingClient
    {
        StringWriter _w = new StringWriter();


        public override void SendError(int errorNumber)
        {
            _w.WriteLine("SendError: {0}", errorNumber);
        }

        public override void Close()
        {
            _w.WriteLine("Close");
        }

        public override void SendSuccess()
        {
            _w.WriteLine("Success");
        }
        public void Clear()
        {
            _w.GetStringBuilder().Clear();
        }

        public override string ToString()
        {
            return _w.ToString();
        }
    }
}
