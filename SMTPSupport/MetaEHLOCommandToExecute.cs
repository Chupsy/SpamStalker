using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaEHLOCommandToExecute : SMTPCommandToExecute
    {
        string _display;

        public MetaEHLOCommandToExecute( string toDisplay )
        {
            _display = toDisplay;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client)
        {
            client.WriteThis(_display);
            client.SetMetaSession();
        }

    }

}
