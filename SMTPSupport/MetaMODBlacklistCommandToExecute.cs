using DataSupport;
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
            Address a = session.MetaSession.MetaAPI.FindUserAddress(_referenceAddress);
            if (a.User.Username == session.MetaSession.User.Username && a.User.Password == session.MetaSession.User.Password && (_blacklistMod == "fuck" || _blacklistMod == "ignore"))
            {
                if(session.MetaSession.User.CheckSpammer( _referenceAddress, _blacklistedAddress))
                {
                    session.MetaSession.User.ModBlacklistAddress(a , _blacklistedAddress, _blacklistMod);
                    session.MetaSession.MetaAPI.WriteUser(a.User);
                    session.MetaSession.User = a.User;
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
