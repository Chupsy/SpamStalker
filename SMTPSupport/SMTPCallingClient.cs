using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SMTPSupport
{
    public enum ErrorCode
    { 
        Ok = 250,
        Help = 214,
        Ready = 220,
        Closing = 221,
        Start = 354,
        NotAvailable = 421,
        Unrecognized = 500,
        ArgumentError = 501,
        NotImplemented = 502,
        BadSequence = 503,
        AdressUnknow = 550
    }

    {
        System.IO.StreamReader _reader;
        System.IO.StreamWriter _writer;
        string _line;
        bool _closed;
        SMTPSession _session;
        Dictionary<ErrorCode, string> _errors;
        TcpClient _clientTcp;
        Dictionary<string, string> _commands;

        public SMTPCallingClient()
        {
            _errors = new Dictionary<ErrorCode,string>();
            CreateDictionnaryErrors();
 
            _commands = new Dictionary<string, string>();
            CreateDictionaryCommands();
        }

        public SMTPCallingClient(System.IO.StreamReader reader, System.IO.StreamWriter writer, SMTPSession session, TcpClient clientTcp)
        {
            _reader = reader;
            _writer = writer;
            _session = session;
            _errors = new Dictionary<int, string>();
            CreateDictionnaryErrors();
            _closed = false;
            _clientTcp = clientTcp;
        }


        public virtual void SendError(ErrorCode errorCode)
        {
            _writer.WriteLine(GetError(errorCode));
        
        }

        public virtual void SendSuccess()
        {
            _writer.WriteLine(GetError(ErrorCode.Ok));
        }


        public virtual void Close()
        {
            _writer.WriteLine(GetError(ErrorCode.Closing));
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
            foreach (KeyValuePair<string, string> _temp in _commands)
            {
                _writer.WriteLine("{0}",_temp.Value);
            }
        }
        public virtual void SendHelp(string parameter)
        {
            if (_commands.ContainsKey(parameter)) _writer.WriteLine("{0}", _commands[parameter]);
            else
            {
                foreach (KeyValuePair<string, string> _temp in _commands)
                {
                    _writer.WriteLine("{0}", _temp.Value);
                }
            }
        }

        public virtual bool IsClosed()
        {
            return _closed;
        }

        internal string GetError(ErrorCode errorName)
        {
            string returnValue;
            if (_errors.ContainsKey(errorName))
            {
                returnValue = (int)errorName + " " + _errors[errorName];
                return returnValue;

            }
            else
            {
                returnValue = "Unknown error(" + (int)errorName + ")";
                return returnValue;

            }
        } 

        private void CreateDictionnaryErrors()
        {
            _errors.Add(ErrorCode.Ok, "OK");
            _errors.Add(ErrorCode.Help, "Help message");
            _errors.Add(ErrorCode.Ready, "<SpamStalker> Service ready");
            _errors.Add(ErrorCode.Closing, "<SpamStalker> Service closing transmission channel");
            _errors.Add(ErrorCode.Start, "Start mail input; end with <CRLF>.<CRLF>");
            _errors.Add(ErrorCode.NotAvailable, "<SpamStalker> Service not available, closing transmission channel");
            _errors.Add(ErrorCode.Unrecognized, "Syntax error, command unrecognized");
            _errors.Add(ErrorCode.ArgumentError, "Syntax error in parameters or arguments");
            _errors.Add(ErrorCode.NotImplemented, "Command not implemented");
            _errors.Add(ErrorCode.BadSequence, "Bad sequence of commands");
            _errors.Add(ErrorCode.AdressUnknow, "Adress Unknow");
        }

        private void CreateDictionaryCommands()
        {
            _commands.Add("MAIL", "MAIL => Syntaxe: MAIL FROM:<domain@name.com>");
            _commands.Add("RCPT", "RCPT => Syntaxe: RCPT TO:<domain@name.com>");
            _commands.Add("DATA", "DATA => Syntaxe: DATA ");
            _commands.Add("NOOP", "NOOP => NOOP ");
            _commands.Add("HELP", "HELP => Syntaxe: HELP or HELP Commandname");
            _commands.Add("EHLO", "EHLO => Syntaxe: EHLO domain@name.com");

        }
    }
}
