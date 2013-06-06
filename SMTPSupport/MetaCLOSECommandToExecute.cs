using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaCLOSECommandToExecute : SMTPCommandToExecute
    {

        public MetaCLOSECommandToExecute()
        {
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            client.SendError(ErrorCode.ShutDown);
            client.Close();
            session.MetaSession.MetaAPI.Shutdown();
        }

    }

}
