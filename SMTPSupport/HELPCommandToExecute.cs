using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class HELPCommandToExecute : SMTPCommandToExecute
    {
        string _parameter;
        public HELPCommandToExecute( )
        {
            _parameter = null;
        }

        public HELPCommandToExecute(string parameter)
        {
            _parameter = parameter;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if (session.IsInitialized)
            {
                if (_parameter == null || _parameter == "")
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
                client.SendError(500);
            }
        }

    }

}
