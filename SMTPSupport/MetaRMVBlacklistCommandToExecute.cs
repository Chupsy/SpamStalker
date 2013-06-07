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
            if (session.MetaSession.MetaAPI.CheckAddressBelonging(_referenceAddress, session.MetaSession.UserName) == true)
            {
                if(session.MetaSession.MetaAPI.CheckSpammer(session.MetaSession.UserName, _referenceAddress, _blacklistedAddress))
                {
                    session.MetaSession.MetaAPI.RmvBlacklistAddress(session.MetaSession.UserName, _referenceAddress, _blacklistedAddress);
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
