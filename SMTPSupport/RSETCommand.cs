using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class RSETCommand : SMTPCommand
    {
        public RSETCommand()
            : base( "RSET", "Clear all session" )
        {
        }

        public override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "RSET" )) throw new ArgumentException( "Must start with RSET." );

           
            return new SMTPCommandParseResult( new RSETCommandToExecute( ) );
        }
    }

}
