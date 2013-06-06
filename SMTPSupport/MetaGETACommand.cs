using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaGETACommand : SMTPCommand
    {
        public MetaGETACommand()
            : base( "!GETA", "Add new mail address" )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "!GETA" ) ) throw new ArgumentException( "Must start with !GETA." );

            return new SMTPCommandParseResult(new MetaGETACommandToExecute());
        }
    }

}
