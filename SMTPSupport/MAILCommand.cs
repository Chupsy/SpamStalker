﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class MAILCommand : SMTPCommand
    {
        public MAILCommand()
            : base( "MAIL", "Specifies sender mail adress." )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if (!firstLine.StartsWith("MAIL")) throw new ArgumentException("Must start with MAIL.");
            string senderAddress = null; 

            if(firstLine.StartsWith("MAIL FROM:<") && firstLine.EndsWith(">"))
            {
                senderAddress = firstLine.Substring("MAIL FROM:<".Length).Trim();
                senderAddress = senderAddress.Remove(senderAddress.Length - 1);
            }
            else
            {
                return new SMTPCommandParseResult(500);
            }

            if (senderAddress != null && senderAddress != "")
            {
                if(!CheckMail(senderAddress))
                {
                    return new SMTPCommandParseResult(500);
                }
            }
            else
            {
                return new SMTPCommandParseResult(501);
            }
            
            return new SMTPCommandParseResult( new MAILCommandToExecute( senderAddress ) );
        }

        private bool CheckMail(string senderAddress)
        {
            string[] adresses = System.IO.File.ReadAllLines(@"..\..\..\FakeSmtp\senderAdresses.txt");
            foreach(string adress in adresses)
            {
                if (adress == senderAddress) return true;
            }
            return false;
        }
    }
}
