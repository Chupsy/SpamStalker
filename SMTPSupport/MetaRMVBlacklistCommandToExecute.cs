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
            if (session.MetaSession.MetaAPI.FindUserAddress(_referenceAddress).User == session.MetaSession.User)
            {
                if(session.MetaSession.User.CheckSpammer( _referenceAddress, _blacklistedAddress))
                {
                    session.MetaSession.User.RemoveBlacklistedAddress( _referenceAddress, _blacklistedAddress);
                    client.SendError(ErrorCode.Ok);
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
