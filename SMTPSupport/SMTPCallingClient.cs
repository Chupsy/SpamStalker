using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net.Mail;

namespace SMTPSupport
{
    public enum ErrorCode
    { 
        ShutDown = 42,
        InformationSend = 163,
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
        AddressUnknown = 550,
        AccountExist = 726,
        AddressUsed = 737,
        Abort = 950,
        NotAllowed = 640,
        AddressDoesNotExist = 911,
        AddressAlreadyBlacklisted = 880,
        AddressNotBlacklisted = 881
    }

    public class SMTPCallingClient
    {
        System.IO.StreamReader _reader;
        System.IO.StreamWriter _writer;
        string _line;
        bool _closed;
        SMTPMetaCallingClient _meta;
        SMTPSession _session;
        Dictionary<ErrorCode, string> _errors;
        TcpClient _clientTcp;
        SMTPParser _parser;

        public SMTPCallingClient()
        {
            _errors = new Dictionary<ErrorCode,string>();
            CreateDictionnaryErrors();
         }

        public SMTPCallingClient(System.IO.StreamReader reader, System.IO.StreamWriter writer, SMTPSession session, TcpClient client)
        {
            _reader = reader;
            _writer = writer;
            _session = session;
            _errors = new Dictionary<ErrorCode, string>();
            CreateDictionnaryErrors();
            _closed = false;
            _clientTcp = client;
            _parser = new SMTPParser();
        }


        public virtual void SendError(ErrorCode errorCode)
        {
            _writer.WriteLine(GetError(errorCode));
        
        }

        public virtual void WriteThis(string txt)
        {
            _writer.WriteLine(txt);
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

        public virtual void ForceClose()
        {
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

            MailAddressCollection blacklistedRecipient = _session.MetaAPI.CheckSpammer(_session.mail.To, _session.mail.Sender.ToString());
            if ( blacklistedRecipient != null)
            {
                _session.SpamReact(blacklistedRecipient);
            }

            _session.SetReadyToSend();
            if (!_session.IsReady())
            {
                SendError(ErrorCode.AddressUnknown);
            }
            else
            {
                SendError(ErrorCode.Closing);
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
            foreach (SMTPCommand temp in SMTPParser.Commands )
            {
                _writer.WriteLine("{0} : {1}", temp.Name, temp.HelpText );
            }
        }
        public virtual void SendHelp(string parameter)
        {
            SMTPCommand cmd = SMTPParser.FindCommand(parameter);
            if ( cmd != null )
            {
                _writer.WriteLine("{0} : {1}", parameter.ToUpper(), cmd.HelpText );
            }
            else SendHelp();
        }

        public virtual void EHLOResponse(string domain)
        {
            _writer.WriteLine("250 {0}", domain);
        }

        public virtual bool IsClosed()
        {
            return _closed;
        }

        internal void EnableMetaClient()
        {
            Debug.Assert(_meta == null, "EnableMetaClient has already been called.");
            _meta = new SMTPMetaCallingClient(this, _parser, _session, _reader, _writer);
            _parser.EnableMetaCommands();
        }

        internal void EnableAdminCommands()
        {
            _parser.EnableAdminCommands();
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

        public bool HasMeta
        {
            get { return _meta != null; }
        }

        public SMTPMetaCallingClient Meta
        {
            get { return _meta; }
        }

        private void CreateDictionnaryErrors()
        {
            _errors.Add(ErrorCode.Ok, "OK");
            _errors.Add(ErrorCode.ShutDown, "Server shutting down");
            _errors.Add(ErrorCode.Help, "Help message");
            _errors.Add(ErrorCode.Ready, "<SpamStalker> Service ready");
            _errors.Add(ErrorCode.Closing, "<SpamStalker> Service closing transmission channel");
            _errors.Add(ErrorCode.Start, "Start mail input; end with <CRLF>.<CRLF>");
            _errors.Add(ErrorCode.NotAvailable, "<SpamStalker> Service not available, closing transmission channel");
            _errors.Add(ErrorCode.Unrecognized, "Syntax error, command unrecognized");
            _errors.Add(ErrorCode.ArgumentError, "Syntax error in parameters or arguments");
            _errors.Add(ErrorCode.NotImplemented, "Command not implemented");
            _errors.Add(ErrorCode.BadSequence, "Bad sequence of commands");
            _errors.Add(ErrorCode.AddressUnknown, "No such user here");
            _errors.Add(ErrorCode.AddressUsed, "Address already used");
            _errors.Add(ErrorCode.AccountExist, "Account already exist");
            _errors.Add(ErrorCode.AddressDoesNotExist, "Address does not exist or does not belong to your account");
            _errors.Add(ErrorCode.InformationSend, "End of information transmission");
            _errors.Add(ErrorCode.Abort, "Command aborted");
            _errors.Add(ErrorCode.NotAllowed, "You are not allowed to modify or delete this user.");
            _errors.Add(ErrorCode.AddressAlreadyBlacklisted, "This address is already blacklisted by the user's address");
            _errors.Add(ErrorCode.AddressNotBlacklisted, "This address is not blacklisted by the user's address");                
        }




    }
}
