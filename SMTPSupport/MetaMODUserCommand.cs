﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaMODUserCommand : SMTPCommand
    {
        public MetaMODUserCommand()
            : base( "!MODU", "Modify an user's type of account or password (first argument : \"type\" or \"password\", second argument : user, third : value) [Admin Command]" )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "!MODU" )) throw new ArgumentException( "Must start with !MODU." );
            
            string value = null;
            string username = null;
            string modify = null;

            if (firstLine.Substring(5).Trim() != null && firstLine.Substring(5).Trim() != "")
            {
                if (firstLine.Substring(5).Trim().Contains(" ") && firstLine.Substring(5).Trim().Substring(firstLine.Substring(5).Trim().IndexOf(" ")).Trim().Contains(" "))
                {                   
                    modify = firstLine.Substring(5).Trim().Substring(0, firstLine.Substring(5).Trim().IndexOf(" ")).Trim();

                    username = firstLine.Substring(5).Trim().Substring(firstLine.Substring(5).Trim().IndexOf(" ")).Trim();
                    username = username.Substring(0, username.IndexOf(" ")).Trim();

                    value = firstLine.Substring(5).Trim().Substring(firstLine.Substring(5).Trim().LastIndexOf(" ") + 1).Trim();

                    return new SMTPCommandParseResult(new MetaMODUserCommandToExecute(modify, username, value));
                }
                else
                {
                    return new SMTPCommandParseResult(ErrorCode.Unrecognized);
                }
            }

            return new SMTPCommandParseResult(ErrorCode.Unrecognized);
        }
    }

}
