using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class RSETCommandToExecute : SMTPCommandToExecute
    {
        
        public RSETCommandToExecute()
        {
           
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if( session.IsInitialized )
            {
                session.Clear();
                client.SendSuccess();
            }
            else
            {
                client.SendError(ErrorCode.Unrecognized);
            }
        }

    }

}
