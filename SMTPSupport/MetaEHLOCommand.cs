using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaEHLOCommand : SMTPCommand
    {
        public MetaEHLOCommand()
            : base( "!EHLO", "Initialize a mail owner - server transmission" )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "!EHLO" ) && !firstLine.StartsWith( "!HELO" ) ) throw new ArgumentException( "Must start with EHLO." );
            
            string username = null;
            string password = null;

            if (firstLine.Substring(5).Trim() != null && firstLine.Substring(5).Trim() != "")
            {
                if (firstLine.Substring(5).Trim().Contains(" "))
                    username = firstLine.Substring(5).Trim().Substring(0, firstLine.Substring(5).Trim().LastIndexOf(" "));
                    password = firstLine.Substring(5).Trim().Substring(firstLine.Substring(5).Trim().LastIndexOf(" ")+1);
                    return new SMTPCommandParseResult(new MetaEHLOCommandToExecute( password ));
            }

            return new SMTPCommandParseResult(ErrorCode.Unrecognized);
        }
    }

}
