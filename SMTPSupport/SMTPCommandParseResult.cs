using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class SMTPCommandParseResult
    {
        internal SMTPCommandParseResult(ErrorCode errorName)
        {
            ErrorCode = errorName;
            Command = null;
        }

        internal SMTPCommandParseResult( SMTPCommandToExecute command )
        {
            Debug.Assert( command != null );
            ErrorCode = ErrorCode.Ok;
            Command = command;
        }

        public ErrorCode ErrorCode { get; private set; }


        public SMTPCommandToExecute Command { get; private set; }
    }
}
