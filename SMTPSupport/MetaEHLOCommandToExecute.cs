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

        public MetaEHLOCommandToExecute( string username, string password )
        {
            _username = username;
            _password = password;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client)
        {
            if (session.HasMetaSession == false && client.HasMeta == false)
            {
                session.EnableMetaSession();
                client.EnableMetaClient();
            }

            string typeOfAccount = session.MetaSession.MetaAPI.Identify(_username, _password);
            if( typeOfAccount != null)
            {
                client.SendError(ErrorCode.Ok);
                session.MetaSession.UserName = _username;
                session.MetaSession.TypeOfAccount = typeOfAccount;
            }
            else
            {
                client.SendError(ErrorCode.AddressUnknown);
                client.Close();
            }
        }

    }

}
