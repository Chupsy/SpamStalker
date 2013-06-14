using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;

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
                    User user = session.MetaAPI.FindUserAddress(_mailAdress).User;
                    if (session.IsBlacklisted(user,_mailAdress, session.Sender.Address) == true)
                    {
                        if (session.IsFucked(user,_mailAdress, session.Sender.Address) == true)
                        {
                            client.SendFuck(35000);
                        }
                        else
                        {
                            client.SendSuccess();
                        }
                    }
                    else
                    {
                        client.SendSuccess();
                    }
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
