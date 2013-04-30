using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class QUITCommand : SMTPCommand
    {
        public QUITCommand()
            : base( "QUIT", "Shut down session." )
        {
        }

        public override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "QUIT" )) throw new ArgumentException( "Must start with QUIT." );
            
            return new SMTPCommandParseResult( new QUITCommandToExecute() );
        }
    }

}
