using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaMODBlacklistCommandToExecute : SMTPCommandToExecute
    {
        string _blacklistedAddress;
        string _referenceAddress;
        string _blacklistMod;

        public MetaMODBlacklistCommandToExecute(string blacklistedAddress, string referenceAddress, string blacklistMod)
        {
            _blacklistedAddress = blacklistedAddress;
            _referenceAddress = referenceAddress;
            _blacklistMod = blacklistMod;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if (session.MetaSession.MetaAPI.CheckAddressBelonging(_referenceAddress, session.MetaSession.UserName) == true)
            {
                if(session.MetaSession.MetaAPI.CheckSpammer(session.MetaSession.UserName, _referenceAddress, _blacklistedAddress))
                {
                    session.MetaSession.MetaAPI.ModBlacklistAddress(session.MetaSession.UserName, _referenceAddress, _blacklistedAddress, _blacklistMod);
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
