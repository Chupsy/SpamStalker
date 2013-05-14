using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class DATACommand : SMTPCommand
    {
        public DATACommand()
            : base( "DATA", "Get data from user." )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "DATA" )) throw new ArgumentException( "Must start with DATA." );

            if (firstLine.Substring(4).Trim().Length > 0)
            {
                return new SMTPCommandParseResult(ErrorCode.Unrecognized);
            }
            return new SMTPCommandParseResult( new DATACommandToExecute() );
        }
    }

}
