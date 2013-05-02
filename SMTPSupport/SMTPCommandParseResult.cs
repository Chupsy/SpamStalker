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
        internal SMTPCommandParseResult(int errorCode)
        {
            ErrorCode = errorCode;
            Command = null;
        }

        internal SMTPCommandParseResult( SMTPCommandToExecute command )
        {
            Debug.Assert( command != null );
            ErrorCode = 250;
            Command = command;
        }

        public int ErrorCode { get; private set; }


        public SMTPCommandToExecute Command { get; private set; }
    }
}
