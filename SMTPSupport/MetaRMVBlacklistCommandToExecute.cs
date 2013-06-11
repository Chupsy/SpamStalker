using DataSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaRMVBlacklistCommandToExecute : SMTPCommandToExecute
    {
        string _blacklistedAddress;
        string _referenceAddress;

        public MetaRMVBlacklistCommandToExecute(string blacklistedAddress, string referenceAddress)
        {
            _blacklistedAddress = blacklistedAddress;
            _referenceAddress = referenceAddress;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            Address a = session.MetaSession.MetaAPI.FindUserAddress(_referenceAddress);
            if (a != null && a.User.Username == session.MetaSession.User.Username && a.User.Password == session.MetaSession.User.Password)
            {
                if(session.MetaSession.User.CheckSpammer( _referenceAddress, _blacklistedAddress))
                {
                    session.MetaSession.User.RemoveBlacklistedAddress( _referenceAddress, _blacklistedAddress);
                    session.MetaAPI.WriteUser(session.MetaSession.User);
                    client.SendError(ErrorCode.Ok);
                    session.MetaAPI.ResetUsers();
                    return;
                }
                client.SendError(ErrorCode.AddressNotBlacklisted);
                return;
            }
            client.SendError(ErrorCode.AddressDoesNotExist);
            return;
        }

    }

}
