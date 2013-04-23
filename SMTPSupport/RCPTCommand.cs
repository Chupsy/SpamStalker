using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class RCPTCommand : SMTPCommand
    {
        public RCPTCommand()
            : base( "RCPT", "Adds a recipient mail address." )
        {
        }

        public override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "RCPT" ) ) throw new ArgumentException( "Must start with RCPT." );
            string extractedMail = null; // or one valid adress mail.

            if( extractedMail == null )
            {
                return new SMTPCommandParseResult( "Missing mail adress." );
            }
            // A valid mail exists.
            RCPTCommandToExecute toExecute = new RCPTCommandToExecute( extractedMail );
            return new SMTPCommandParseResult( toExecute );
        }
    }

}
