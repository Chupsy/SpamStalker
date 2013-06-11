using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaGETACommandToExecute : SMTPCommandToExecute
    {
        string _sendInformation;

        public MetaGETACommandToExecute()
        {
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            client.Meta.SendInformations(session.MetaSession.User);

            client.SendError(ErrorCode.InformationSend);
        }

    }

}
