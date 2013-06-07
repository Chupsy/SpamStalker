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
            if (session.MetaSession.MetaAPI.CheckAddressBelonging(_referenceAddress, session.MetaSession.UserName) == true)
            {
                if(session.MetaSession.MetaAPI.CheckSpammer(session.MetaSession.UserName, _referenceAddress, _blacklistedAddress))
                {
                    client.SendError(ErrorCode.AddressAlreadyBlacklisted);
                    return;
                }
                session.MetaSession.MetaAPI.AddBlacklistAddress(session.MetaSession.UserName, _referenceAddress, _blacklistedAddress);
                client.SendError(ErrorCode.Ok);
                return;
            }
            client.SendError(ErrorCode.AddressDoesNotExist);
            return;
        }

    }

}
