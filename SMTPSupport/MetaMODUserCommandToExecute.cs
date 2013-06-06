using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaMODUserCommandToExecute : SMTPCommandToExecute
    {
        string _modify;
        string _value;
        string _username;

        public MetaMODUserCommandToExecute( string modify, string user, string value)
        {
            _modify = modify;
            _username = user;
            _value = value;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client)
        {
            if (session.MetaSession.MetaAPI.CheckUser(_username) == true)
            {
                if (_username == "System")
                {
                    client.SendError(ErrorCode.NotAllowed);
                    return;
                }
                if (client.Meta.ValidateModification(_username, _modify, _value) == true)
                {
                    if (_modify.ToUpper() == "TYPE")
                    {
                        session.MetaSession.MetaAPI.ModifyType(_username, _value);
                        client.SendError(ErrorCode.Ok);
                        return;
                    }
                    else if (_modify.ToUpper() == "PASSWORD")
                    {
                        session.MetaSession.MetaAPI.ModifyPassword(_username, _value);
                        client.SendError(ErrorCode.Ok);
                        return;
                    }
                    else
                    {
                        client.SendError(ErrorCode.ArgumentError);
                    }
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
            }
        }

    }

}
