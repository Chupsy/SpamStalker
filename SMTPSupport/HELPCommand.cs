using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class HELPCommand : SMTPCommand
    {
        public HELPCommand()
            : base( "HELP", "Shows SMTP commands help." )
        {
        }

        public override SMTPCommandParseResult Parse( string firstLine )
        {
            if (!firstLine.StartsWith("HELP") && !firstLine.StartsWith("HELP")) throw new ArgumentException("Must start with EHLO.");

            if (firstLine.Trim().Length > 4)
            {
                if (firstLine.Substring(4).Length < 4)
                {
                    return new SMTPCommandParseResult(500, "Syntax Error");
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
