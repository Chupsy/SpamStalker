using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace SMTPSupport
{

    public class SMTPMetaCallingClient
    {

        SMTPCallingClient _client;
        SMTPSession _session;
        SMTPParser _parser;
        System.IO.StreamReader _reader;
        System.IO.StreamWriter _writer;

        public SMTPMetaCallingClient(SMTPCallingClient client, SMTPParser parser, SMTPSession session, System.IO.StreamReader reader, System.IO.StreamWriter writer)
        {
            _client = client;
            _parser = parser;
            _session = session;
            _reader = reader;
            _writer = writer;
        }


        public string ReadLine()
        {
            return _reader.ReadLine();
            
        }



        public bool Validate(string _username)
        {
            string line = "Do you really wish to delete " + _username + " ? There is no coming back ! ( Y or YES to validate )";
            _writer.WriteLine(line);
            line = _reader.ReadLine();
            if (line.Trim().ToUpper() == "Y" || line.Trim().ToUpper() == "YES")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateModification(string username, string modify, string value)
        {
            string line = "Are you sur you want to modify " + modify + " of the user " + username + " to the value " + value + " ? (Y or YES to validate)";
            _writer.WriteLine(line);
            line = _reader.ReadLine();
            if (line.Trim().ToUpper() == "Y" || line.Trim().ToUpper() == "YES")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
