using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class MAILCommand : SMTPCommand
    {
        public MAILCommand()
            : base( "MAIL", "Specifies sender mail adress." )
        {
        }

        public override SMTPCommandParseResult Parse( string firstLine )
        {
            if (!firstLine.StartsWith("MAIL") && !firstLine.StartsWith("MAIL")) throw new ArgumentException("Must start with MAIL.");
            string senderAdress = null; // or one valid domain name.

            if( senderAdress == null )
            {
                return new SMTPCommandParseResult( 500, "Missing adress." );
            }
            return new SMTPCommandParseResult( new MAILCommandToExecute( senderAdress ) );
        }
    }

}
