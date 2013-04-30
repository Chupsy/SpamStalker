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

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "EHLO" ) && !firstLine.StartsWith( "HELO" ) ) throw new ArgumentException( "Must start with EHLO." );
            
            string senderDomainName = null;

            if (firstLine.Substring(4).Trim() != null && firstLine.Substring(4).Trim() != "")
            {
                senderDomainName = firstLine.Substring(4).Trim();
            }

            if( senderDomainName == null )
            {
                return new SMTPCommandParseResult( 500, "Missing domain name." );
            }
            return new SMTPCommandParseResult( new EHLOCommandToExecute( senderDomainName ) );
        }
    }

}
