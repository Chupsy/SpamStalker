using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaDELUserCommandToExecute : SMTPCommandToExecute
    {
        string _username;

        public MetaDELUserCommandToExecute( string username)
        {
            _username = username;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client)
        {
            if (session.MetaSession.MetaAPI.CheckUser(_username) == true)
            {
                if (_username.ToUpper() == "SYSTEM")
                {
                    client.SendError(ErrorCode.NotAllowed);
                    return;
                }
                if (client.Meta.Validate(_username) == true)
                {
                    session.MetaSession.MetaAPI.DeleteUser(_username);
                    client.SendError(ErrorCode.Ok);
                    return;
                }
                else
                {
                    client.SendError(ErrorCode.Abort);
                    return;
                }
            }
            else
            {
                client.SendError(ErrorCode.AddressUnknown);
                return;
            }
        }

    }

}
