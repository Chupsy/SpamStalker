using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaRMVAddressCommand : SMTPCommand
    {
        public MetaRMVAddressCommand()
            : base("!REMA", "Shut down transmission server")
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if (!firstLine.StartsWith("!REMA")) throw new ArgumentException("Must start with !REMA.");


            return new SMTPCommandParseResult(new MetaRMVAddressCommandToExecute());
        }
    }

}
