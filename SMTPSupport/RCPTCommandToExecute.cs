using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class RCPTCommandToExecute : SMTPCommandToExecute
    {
        string _mailAdress;

        public RCPTCommandToExecute( string mailAdress )
        {
            _mailAdress = mailAdress;
        }


        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if (session.IsInitialized)
            {

                if (session.MetaAPI.FindUserAddress(_mailAdress) != null)
                {
                    session.AddRecipient(_mailAdress);
                    session.KnownAdress = true;
                    client.SendSuccess();
                }
                else
                {
                    session.AddRecipient(_mailAdress);
                    client.SendError(ErrorCode.AddressUnknown);
                }
            }
            else
            {
                client.SendError(ErrorCode.Unrecognized);
                client.Close();
            }
        }

    }

}
