using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SMTPSupport
{
    internal class DATACommandToExecute : SMTPCommandToExecute
    {

        public DATACommandToExecute()
        {
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            client.GetData();
        }

    }

}
