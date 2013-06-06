using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaCREUserCommandToExecute : SMTPCommandToExecute
    {
        string _username;
        string _password;
        string _typeOfAccount;

        public MetaCREUserCommandToExecute( string username, string password, string typeOfAccount )
        {
            _username = username;
            _password = password;
            _typeOfAccount = typeOfAccount;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client)
        {
            if (session.MetaSession.MetaAPI.CheckUser(_username) == true)
            {
                client.SendError(ErrorCode.AccountExist);
            }
            else
            {
                session.MetaSession.MetaAPI.CreateAccount(_username, _password, _typeOfAccount);
                client.SendError(ErrorCode.Ok);
            }
        }

    }

}
