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
        bool _alertSpamer = false;

        public MAILCommandToExecute( string senderAdress )
        {
            _senderAdress = senderAdress;
        }

        public MAILCommandToExecute(string senderAdress, bool alertSpamer)
        {
            _senderAdress = senderAdress;
            _alertSpamer = alertSpamer;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if (session.IsInitialized)
            {
                if (_alertSpamer == true)
                {
                session.spamReact(_senderAdress);
                }
                else
                {
                session.AddSender(_senderAdress);
                client.SendSuccess();
                }
            }
            else
            {
                client.SendError(500);
            }
        }
    }
}
