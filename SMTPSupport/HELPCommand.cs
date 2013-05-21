using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class HELPCommand : SMTPCommand
    {
        public HELPCommand()
            : base( "HELP", "Shows SMTP commands help." )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if (!firstLine.StartsWith("HELP")) throw new ArgumentException("Must start with HELP.");

            if (firstLine.Trim().Length > 4)
            {
                if (firstLine.Substring(4).Trim().Length < 4)
                {
                    return new SMTPCommandParseResult(new HELPCommandToExecute());
                }
                else
                {
                    return new SMTPCommandParseResult(new HELPCommandToExecute(firstLine.Substring(4).ToUpper().Trim()));
                }
            }
            else
            {
                return new SMTPCommandParseResult(new HELPCommandToExecute());
            }
        }
    }

}
