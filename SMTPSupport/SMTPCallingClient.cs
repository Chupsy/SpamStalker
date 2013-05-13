using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SMTPSupport
{
    public class SMTPCallingClient
    {
        System.IO.StreamReader _reader;
        System.IO.StreamWriter _writer;
        string _line;
        bool _closed;
        SMTPSession _session;
        Dictionary<int, string> _errors;
        TcpClient _clientTcp;
        private System.IO.StreamReader reader;
        private System.IO.StreamWriter writer;
        private SMTPSession session;
        private TcpClient client;

        public SMTPCallingClient()
        {
            _errors = new Dictionary<int,string>();
            CreateDictionnaryErrors();
        }

        public SMTPCallingClient(System.IO.StreamReader reader, System.IO.StreamWriter writer, SMTPSession session, TcpClient client)
        {
            _reader = reader;
            _writer = writer;
            _session = session;
            _errors = new Dictionary<int, string>();
            CreateDictionnaryErrors();
            _closed = false;
            _clientTcp = client;
        }


        public virtual void SendError( int errorNumber )
        {
            _writer.WriteLine(GetError(errorNumber));
        
        }

        public virtual void SendSuccess()
        {
            _writer.WriteLine(GetError(250));
        }


        public virtual void Close()
        {
            _writer.WriteLine(GetError(221));
            _clientTcp.Close();
            _closed = true;
        }

        public virtual void GetData()
        {
            _writer.WriteLine("354 Start input, end data with <CRLF>.<CRLF>");
            do
            {
                _line = _reader.ReadLine();
                AnalyzeData(_line);

            } while (_line != ".");
            _session.SetReadyToSend();
            if (!_session.IsReady())
            {
                SendError(550);
            }
            else
            {
                SendError(221);
            }
            Close();
            _closed = true;
        }

        public virtual void AnalyzeData(string line)
        {
            if(line.StartsWith("Subject: "))
            {
                _session.mail.Subject = line.Substring("Subject: ".Length);
                return;
            }
            else
            {
                _session.mail.Body += line;
                return;
            }
        }

        public virtual void SendHelp()
        {
        }
        public virtual void SendHelp(string parameter)
        {

        }

        public virtual bool IsClosed()
        {
            return _closed;
        }

        internal string GetError(int errorNumber)
        {
            string returnValue;
            if (_errors.ContainsKey(errorNumber))
            {
                returnValue = errorNumber + " " + _errors[errorNumber];
                return returnValue;
            }
            else
            {
                returnValue = "Unknown error(" + errorNumber + ")";
                return returnValue;
            }
        }
        private void CreateDictionnaryErrors()
        {
            _errors.Add(250, "OK");
            _errors.Add(214, "Help message");
            _errors.Add(220, "<SpamStalker> Service ready");
            _errors.Add(221, "<SpamStalker> Service closing transmission channel");
            _errors.Add(354, "Start mail input; end with <CRLF>.<CRLF>");
            _errors.Add(421, "<SpamStalker> Service not available, closing transmission channel");
            _errors.Add(500, "Syntax error, command unrecognized");
            _errors.Add(501, "Syntax error in parameters or arguments");
            _errors.Add(502, "Command not implemented");
            _errors.Add(503, "Bad sequence of commands");
            _errors.Add(550, "Mail adress not found");
        }
    }
}
