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
            _sendInformation = session.MetaSession.MetaAPI.FindUser(session.MetaSession.UserName);

                client.WriteThis(_sendInformation);

            client.SendError(ErrorCode.InformationSend);
        }

    }

}
