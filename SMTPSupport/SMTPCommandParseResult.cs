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
        internal SMTPCommandParseResult( int errorCode, string errorMessage )
        {
            Debug.Assert( errorMessage != null && errorMessage.Length > 0 );
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Command = null;
        }
        internal SMTPCommandParseResult(int errorCode)
        {
            ErrorCode = errorCode;
            Command = null;
        }

        internal SMTPCommandParseResult( SMTPCommandToExecute command )
        {
            Debug.Assert( command != null );
            ErrorCode = 250;
            ErrorMessage = null;
            Command = command;
        }

        public int ErrorCode { get; private set; }

        public string ErrorMessage { get; private set; }

        public SMTPCommandToExecute Command { get; private set; }
    }
}
