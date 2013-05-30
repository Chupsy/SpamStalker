using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaADDAddressCommandToExecute : SMTPCommandToExecute
    {

        public MetaADDAddressCommandToExecute()
        {
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            client.SendError(ErrorCode.Closing);
            // Utiliser IMetaCommandAPI
        }

    }

}
