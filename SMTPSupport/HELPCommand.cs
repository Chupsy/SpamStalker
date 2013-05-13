using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class HELPCommand : SMTPCommand
    {
        public HELPCommand(SMTPParser parser)
            : base( "HELP", "Shows SMTP commands help." )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if (!firstLine.StartsWith("HELP")) throw new ArgumentException("Must start with HELP.");

            if (firstLine.Trim().Length > 4)
            {
                if (firstLine.Substring(4).Length < 4)
                {
                    return new SMTPCommandParseResult(ErrorCode.Unrecognized);
                }
                else
                {
                    return new SMTPCommandParseResult(new HELPCommandToExecute(firstLine.Substring(4).ToUpper()));
                }
            }
            else
            {
                return new SMTPCommandParseResult(new HELPCommandToExecute());
            }
        }
    }

}
