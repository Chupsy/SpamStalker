using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaCLOSECommand : SMTPCommand
    {
        public MetaCLOSECommand()
            : base( "!CLSE", "Shut down transmission server" )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "!CLSE" ) ) throw new ArgumentException( "Must start with !CLSE." );
            

            return new SMTPCommandParseResult(new MetaCLOSECommandToExecute(  ));
        }
    }

}
