using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaDELUserCommand : SMTPCommand
    {
        public MetaDELUserCommand()
            : base( "!DELU", "Delete an user [Admin Command]" )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "!DELU" )) throw new ArgumentException( "Must start with !DELU." );
            
            string username = null;

            if (firstLine.Substring(5).Trim() != null && firstLine.Substring(5).Trim() != "")
            {
                    username = firstLine.Substring(5).Trim();
                    return new SMTPCommandParseResult(new MetaDELUserCommandToExecute( username));
            }

            return new SMTPCommandParseResult(ErrorCode.Unrecognized);
        }
    }

}
