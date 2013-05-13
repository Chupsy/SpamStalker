using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class HELPCommandToExecute : SMTPCommandToExecute
    {
        string _parameter;
        public HELPCommandToExecute( )
        {
        }

        public HELPCommandToExecute(string parameter)
        {
            _parameter = parameter;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if (session.IsInitialized)
            {
                if (_parameter == string.Empty)
                {
                    client.SendHelp();
                }
                else
                {
                    client.SendHelp(_parameter);
                }
            }
            else
            {
                client.SendError(ErrorCode.Unrecognized);
            }
        }
    }
}
