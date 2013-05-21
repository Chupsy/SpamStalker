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


        public override void SendError(ErrorCode errorName)
        {
            _w.WriteLine("SendError: {0}", (int)errorName);
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

        public override void EHLOResponse(string domain)
        {
            _w.WriteLine("250 {0}", domain);
        }
        public override string ToString()
        {
            return _w.ToString();
        }
    }
}
