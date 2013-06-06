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
            if (session.MetaSession.MetaAPI.CheckAddressBelonging(_rmvAddress, session.MetaSession.UserName) == true)
            {
                session.MetaSession.MetaAPI.RemoveAddress(_rmvAddress, session.MetaSession.UserName);
            }
            else
            {
                client.SendError(ErrorCode.AddressDoesNotExist);
            }
        }

    }

}
