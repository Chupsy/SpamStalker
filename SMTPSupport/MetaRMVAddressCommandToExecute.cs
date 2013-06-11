using DataSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaRMVAddressCommandToExecute : SMTPCommandToExecute
    {
        string _rmvAddress;
        public MetaRMVAddressCommandToExecute(string rmvAddress)
        {
            _rmvAddress = rmvAddress;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            Address a = session.MetaSession.MetaAPI.FindUserAddress(_rmvAddress);
            if (a != null && a.User.Username == session.MetaSession.User.Username && a.User.Password == session.MetaSession.User.Password)
            {
                session.MetaSession.User.RemoveAddress(a);
                session.MetaSession.MetaAPI.WriteUser(session.MetaSession.User);
                client.SendError(ErrorCode.Ok);
            }
            else
            {
                client.SendError(ErrorCode.AddressDoesNotExist);
            }
        }

    }

}
