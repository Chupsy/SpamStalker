﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class NOOPCommand : SMTPCommand
    {
        public NOOPCommand()
            : base( "NOOP", "Require 250 OK")
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "NOOP" )) throw new ArgumentException( "Must start with EHLO." );
            
            return new SMTPCommandParseResult( new NOOPCommandToExecute() );
        }
    }

}