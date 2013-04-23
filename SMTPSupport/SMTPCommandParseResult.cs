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
        internal SMTPCommandParseResult( string errorMessage )
        {
            Debug.Assert( errorMessage != null && errorMessage.Length > 0 );
            ErrorMessage = errorMessage;
            Command = null;
        }

        internal SMTPCommandParseResult( SMTPCommandToExecute command )
        {
            Debug.Assert( command != null );
            ErrorMessage = null;
            Command = command;
        }

        public string ErrorMessage { get; private set; }

        public SMTPCommandToExecute Command { get; private set; }
    }
}
