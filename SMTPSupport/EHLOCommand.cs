using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class EHLOCommand : SMTPCommand
    {
        public EHLOCommand()
            : base( "EHLO", "Initialize a stream." )
        {
        }

        public override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "EHLO" ) || !firstLine.StartsWith( "HELO" ) ) throw new ArgumentException( "Must start with EHLO." );
            string senderDomainName = null; // or one valid domain name.

            if( senderDomainName == null )
            {
                return new SMTPCommandParseResult( 500, "Missing domain name." );
            }
            return new SMTPCommandParseResult( new EHLOCommandToExecute( senderDomainName ) );
        }
    }

}
