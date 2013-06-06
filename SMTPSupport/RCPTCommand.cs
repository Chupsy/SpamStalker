using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class RCPTCommand : SMTPCommand
    {
        public RCPTCommand()
            : base( "RCPT", "Adds a recipient mail address." )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "RCPT" ) ) throw new ArgumentException( "Must start with RCPT." );

            string extractedMail = null;

            if (firstLine.StartsWith("RCPT TO:<") && firstLine.EndsWith(">"))
            {
                extractedMail = firstLine.Substring("RCPT TO:<".Length).Trim();
                extractedMail = extractedMail.Remove(extractedMail.Length - 1);
            }
            else
            {
                return new SMTPCommandParseResult(ErrorCode.Unrecognized);
            }

            if (extractedMail != "" && extractedMail != null)
            {

                    return new SMTPCommandParseResult(new RCPTCommandToExecute(extractedMail));
            }
            else
            {
                return new SMTPCommandParseResult(ErrorCode.ArgumentError);
            }

        }

    }

}
