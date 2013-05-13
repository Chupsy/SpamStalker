using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        BadSequence = 503
    }

    public class SMTPCallingClient
    {
        System.IO.StreamReader _reader;
        System.IO.StreamWriter _writer;
        string _line;
        SMTPSession _session;
        Dictionary<ErrorCode, string> _errors;
        Dictionary<string, string> _commands;

        public SMTPCallingClient()
        {
            _errors = new Dictionary<ErrorCode,string>();
            CreateDictionnaryErrors();
 
            _commands = new Dictionary<string, string>();
            CreateDictionaryCommands();
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
            SendSuccess();
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
                _writer.WriteLine("");
            }
        }
        public virtual void SendHelp(string parameter)
        {

        }

        //public virtual void SendError(int p1, string p2)
        //{
            
        //}

        internal string GetError(int errorCode)
        {
            string returnValue;
            if (_errors.ContainsKey(errorCode))
            {
                returnValue = errorCode + " " + _errors[errorCode];
                return returnValue;
            }
            else
            {
                returnValue = "Unknown error(" + errorCode + ")";

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
        }

        private void CreateDictionaryCommands()
        {
            _commands.Add("MAIL", "MAIL => Syntaxe: MAIL FROM:<domain@name.com>");
            _commands.Add("RCPT", "RCPT => Syntaxe: RCPT TO:<domain@name.com>");
            _commands.Add("DATA", "DATA => Syntaxe: DATA ");
            _commands.Add("NOOP", "NOOP => NOOP ");
            _commands.Add("HELP", "HELP => Syntaxe: HELP or HELP Commandname");
            _commands.Add("EHLO", "EHLO => Syntaxe: EHLO");

        }
    }
}
