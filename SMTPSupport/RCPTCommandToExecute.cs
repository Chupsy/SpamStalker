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
        int _error;

        public RCPTCommandToExecute( string mailAdress )
        {
            _mailAdress = mailAdress;
        }

        public RCPTCommandToExecute(string mailAdress, int error)
        {
            _mailAdress = mailAdress;
            _error = error;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if (session.IsInitialized)
            {
                if (_error == 0)
                {
                    session.AddRecipient(_mailAdress);
                    session.KnownAdress = true;
                    client.SendSuccess();
                }
                else
                {
                    session.AddRecipient(_mailAdress);
                    client.SendError(ErrorCode.Unrecognized);
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
