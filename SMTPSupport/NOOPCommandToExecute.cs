using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class NOOPCommandToExecute : SMTPCommandToExecute
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
                client.SendError(500);
                client.Close();
            }
        }

    }

}
