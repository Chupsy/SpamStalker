using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaADDAddressCommand : SMTPCommand
    {
        public MetaADDAddressCommand()
            : base( "!ADDA", "Add new mail address" )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "!ADDA" ) ) throw new ArgumentException( "Must start with !ADDA." );
            

            return new SMTPCommandParseResult(new MetaADDAddressCommandToExecute(  ));
        }
    }

}
