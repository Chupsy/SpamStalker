﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class RCPTCommandToExecute : SMTPCommandToExecute
    {
        string _mailAdress;

        public RCPTCommandToExecute( string mailAdress )
        {
            _mailAdress = mailAdress;
        }

        public override void Execute( SMTPSession session )
        {
            session.AddRecipient( _mailAdress );
        }

    }

}
