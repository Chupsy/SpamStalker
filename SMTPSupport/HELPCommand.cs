using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class HELPCommand : SMTPCommand
    {
        public HELPCommand()
            : base( "HELP", "Shows SMTP commands help." )
        {
        }

        public override SMTPCommandParseResult Parse( string firstLine )
        {
            if (!firstLine.StartsWith("HELP") && !firstLine.StartsWith("HELP")) throw new ArgumentException("Must start with EHLO.");

            return new SMTPCommandParseResult( new HELPCommandToExecute() );
        }
    }

}
