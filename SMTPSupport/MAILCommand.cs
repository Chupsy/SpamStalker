using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MAILCommand : SMTPCommand
    {
        public MAILCommand()
            : base( "MAIL", "Specifies sender mail adress." )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if (!firstLine.StartsWith("MAIL")) throw new ArgumentException("Must start with MAIL.");
            string senderAddress = null; 

            if(firstLine.StartsWith("MAIL FROM:<") && firstLine.EndsWith(">"))
            {
                senderAddress = firstLine.Substring("MAIL FROM:<".Length).Trim();
                senderAddress = senderAddress.Remove(senderAddress.Length - 1);
                if (senderAddress.Length <= 0)
                {
                    return new SMTPCommandParseResult(ErrorCode.ArgumentError);
                }
            }
            else
            {
                return new SMTPCommandParseResult(ErrorCode.Unrecognized);
            }

            if (senderAddress != null && senderAddress != "")
            {

                return new SMTPCommandParseResult(new MAILCommandToExecute(senderAddress));

            }
            else
            {
                return new SMTPCommandParseResult(ErrorCode.ArgumentError);
            }
            
        }

    }
}
