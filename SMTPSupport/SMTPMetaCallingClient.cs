using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace SMTPSupport
{

    public class SMTPMetaCallingClient : SMTPCallingClient
    {
        System.IO.StreamReader _reader;
        System.IO.StreamWriter _writer;
        SMTPSession _session;
        Dictionary<ErrorCode, string> _errors;
        TcpClient _clientTcp;
        SMTPParser _parser;


        public SMTPMetaCallingClient(System.IO.StreamReader reader, System.IO.StreamWriter writer, SMTPSession session, TcpClient client, SMTPParser parser)
        :base(reader, writer, session, client)
        {
            _reader = reader;
            _writer = writer;
            _session = session;
            _errors = new Dictionary<ErrorCode, string>();
            CreateDictionnaryErrors();
            _clientTcp = client;
            _parser = parser;
            ActivateMetaSession();
        }

        public void ActivateMetaSession()
        {
            _parser.EnableMetaCommands();
        }

        public override void SendError(ErrorCode errorCode)
        {
            _writer.WriteLine(GetError(errorCode));

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
            _errors.Add(ErrorCode.AddressUnknown, "Adress Unknow");
        }


    }
}
