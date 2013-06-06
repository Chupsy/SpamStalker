﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaRMVAddressCommand : SMTPCommand
    {
        string _rmvAddress;
        public MetaRMVAddressCommand()
            : base("!REMA", "Shut down transmission server")
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if (!firstLine.StartsWith("!REMA")) throw new ArgumentException("Must start with !REMA.");


            if (firstLine.Substring(5).Trim().Length > 0)
            {
                _rmvAddress = firstLine.Substring(5).Trim();
                return new SMTPCommandParseResult(new MetaRMVAddressCommandToExecute(_rmvAddress));
            }
            else
            {
                return new SMTPCommandParseResult(ErrorCode.BadSequence);
            }
        }
    }

}
