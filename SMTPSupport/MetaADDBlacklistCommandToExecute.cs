using DataSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaADDBlacklistCommandToExecute : SMTPCommandToExecute
    {
        string _blacklistedAddress;
        string _referenceAddress;

        public MetaADDBlacklistCommandToExecute(string blacklistedAddress, string referenceAddress)
        {
            _blacklistedAddress = blacklistedAddress;
            _referenceAddress = referenceAddress;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            Address a = session.MetaSession.MetaAPI.FindUserAddress(_referenceAddress);
            if (a != null && a.User.Username == session.MetaSession.User.Username && a.User.Password == session.MetaSession.User.Password)
            {
                if(session.MetaSession.User.CheckSpammer(_referenceAddress, _blacklistedAddress))
                {
                    client.SendError(ErrorCode.AddressAlreadyBlacklisted);
                    return;
                }
                session.MetaSession.User.AddBlacklistAddress( _referenceAddress, _blacklistedAddress);
                session.MetaSession.MetaAPI.WriteUser(session.MetaSession.User);
                client.SendError(ErrorCode.Ok);
                session.MetaAPI.ResetUsers();
                return;
            }
            client.SendError(ErrorCode.AddressDoesNotExist);
            return;
        }

    }

}
