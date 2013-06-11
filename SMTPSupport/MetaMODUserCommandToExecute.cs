using DataSupport;
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
            if(!session.MetaSession.MetaAPI.CheckUserExist(_username)) 
            {
                client.SendError(ErrorCode.Abort);
                return;
            }
                User u = session.MetaSession.MetaAPI.FindUser(_username);
            if (u != null)
            {
                if (_username == "system")
                {
                    client.SendError(ErrorCode.NotAllowed);
                    return;
                }
                if (client.Meta.ValidateModification(_username, _modify, _value) == true)
                {
                    if (_modify.ToUpper() == "TYPE")
                    {
                        u.ModifyType(_value);
                        session.MetaAPI.WriteUser(u);
                        client.SendError(ErrorCode.Ok);
                        session.MetaAPI.ResetUsers();
                        return;
                    }
                    else if (_modify.ToUpper() == "PASSWORD")
                    {
                        u.ModifyPassword(_value);
                        session.MetaAPI.WriteUser(u);
                        client.SendError(ErrorCode.Ok);
                        session.MetaAPI.ResetUsers();
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
