using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class MAILCommandToExecute : SMTPCommandToExecute
    {
        string _senderAdress;

        public MAILCommandToExecute( string senderAdress )
        {
            _senderAdress = senderAdress;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if (session.IsInitialized)
            {
                session.AddSender(_senderAdress);
            }
            else
            {
                client.SendError(ErrorCode.Unrecognized);
            }
           
        }

    }

}
