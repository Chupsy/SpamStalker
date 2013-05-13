using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class NOOPCommandToExecute : SMTPCommandToExecute
    {

        public NOOPCommandToExecute()
        {
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if( session.IsInitialized )
            {
                client.SendSuccess(); 
            }
            else
            {
                client.SendError(ErrorCode.Unrecognized);
                client.Close();
            }
        }

    }

}
