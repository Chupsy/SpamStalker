using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaRMVBlacklistCommand : SMTPCommand
    {
        string _blacklistedAddress;
        string _referenceAddress;
        public MetaRMVBlacklistCommand()
            : base("!RMVB", "Removes an adress from the blacklist")
        {
        }

        internal override SMTPCommandParseResult Parse(string firstLine)
        {
            if (!firstLine.StartsWith("!RMVB")) throw new ArgumentException("Must start with !RMVB.");


            if (firstLine.Trim().Length>5 && firstLine.Substring(5).Trim() != null && firstLine.Substring(5).Trim() != "")
            {
                if (firstLine.Substring(5).Trim().Contains(" "))
                {
                    _blacklistedAddress = firstLine.Substring(5).Trim().Substring(0, firstLine.Substring(5).Trim().LastIndexOf(" "));
                    _referenceAddress = firstLine.Substring(5).Trim().Substring(firstLine.Substring(5).Trim().LastIndexOf(" ") + 1);
                    return new SMTPCommandParseResult(new MetaRMVBlacklistCommandToExecute(_blacklistedAddress, _referenceAddress));
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
