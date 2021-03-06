﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaMODBlacklistCommand : SMTPCommand
    {
        string _blacklistedAddress;
        string _referenceAddress;
        string _blacklistMod;

        public MetaMODBlacklistCommand()
            : base("!MODB", "Modify Blacklist Type")
        {
        }

        internal override SMTPCommandParseResult Parse(string firstLine)
        {
            if (!firstLine.StartsWith("!MODB")) throw new ArgumentException("Must start with !MODB.");


            if (firstLine.Trim().Length>5 && firstLine.Substring(5).Trim() != null && firstLine.Substring(5).Trim() != "")
            {
                if (firstLine.Substring(5).Trim().Contains(" ") && firstLine.Substring(5).Trim().Substring(firstLine.Substring(5).Trim().IndexOf(" ")).Trim().Contains(" "))
                {
                    _referenceAddress = firstLine.Substring(5).Trim().Substring(0, firstLine.Substring(5).Trim().IndexOf(" "));
                    _blacklistedAddress = firstLine.Substring(5).Trim().Substring(firstLine.Substring(5).Trim().IndexOf(" ") + 1);
                    _blacklistedAddress = _blacklistedAddress.Substring(0, _blacklistedAddress.Trim().LastIndexOf(" "));
                    _blacklistMod = firstLine.Substring(5).Trim().Substring( firstLine.Substring(5).Trim().LastIndexOf(" ")+1);
                    return new SMTPCommandParseResult(new MetaMODBlacklistCommandToExecute(_blacklistedAddress, _referenceAddress, _blacklistMod));
                }
                return new SMTPCommandParseResult(ErrorCode.BadSequence);

            }

            else
            {
                return new SMTPCommandParseResult(ErrorCode.BadSequence);
            }
        }
    }

}
