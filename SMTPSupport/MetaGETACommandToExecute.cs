using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaGETACommandToExecute : SMTPCommandToExecute
    {
        List<string> _sendInformation;

        public MetaGETACommandToExecute()
        {
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            _sendInformation = session.MetaSession.MetaAPI.GetAllInformations(session.MetaSession.UserName);
            foreach (string information in _sendInformation)
            {
                client.WriteThis(information);
            }
            client.SendError(ErrorCode.InformationSend);
        }

    }

}
