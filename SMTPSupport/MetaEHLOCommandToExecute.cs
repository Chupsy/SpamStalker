using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaEHLOCommandToExecute : SMTPCommandToExecute
    {
        string _username;
        string _password;
        ErrorCode _errorCode;

        public MetaEHLOCommandToExecute( string username, string password )
        {
            _username = username;
            _password = password;
        }

        public MetaEHLOCommandToExecute(ErrorCode errorCode)
        {
            _errorCode = errorCode;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client)
        {
            if (_errorCode == ErrorCode.Unrecognized)
            {
                client.SendError(ErrorCode.Unrecognized);
                client.Close();
                return;
            }
            if (session.HasMetaSession == false && client.HasMeta == false)
            {
                session.EnableMetaSession();
                client.EnableMetaClient();
            }
            session.MetaSession.User = session.MetaAPI.FindUser(_username);
            if( session.MetaSession.User != null && session.MetaSession.User.Username == _username && session.MetaSession.User.Password == _password)
            {
                client.SendError(ErrorCode.Ok);
                session.Initialize("FakeSMTP");
                if (session.MetaSession.User.AccountType == "admin")
                {
                    client.EnableAdminCommands();
                    
                }
            }
            else
            {
                client.SendError(ErrorCode.AddressUnknown);
                client.Close();
            }
        }

    }

}
