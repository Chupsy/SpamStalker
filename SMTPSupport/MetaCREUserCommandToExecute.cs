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
        string _mainAddress;

        public MetaCREUserCommandToExecute( string username, string password, string mainAddress, string typeOfAccount )
        {
            _username = username;
            _password = password;
            _typeOfAccount = typeOfAccount;
            _mainAddress = mainAddress;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client)
        {
            if (session.MetaSession.MetaAPI.CheckUser(_username) == true)
            {
                client.SendError(ErrorCode.AccountExist);
            }
            else
            {
                session.MetaSession.MetaAPI.CreateUser(_username, _password, _mainAddress, _typeOfAccount);
                client.SendError(ErrorCode.Ok);
            }
        }

    }

}
