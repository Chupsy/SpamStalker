using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaADDBlacklistCommand : SMTPCommand
    {
        string _blacklistedAddress;
        string _referenceAddress;
        public MetaADDBlacklistCommand()
            : base("!ADDB", "Add an adress to the blacklist")
        {
        }

        internal override SMTPCommandParseResult Parse(string firstLine)
        {
            if (!firstLine.StartsWith("!ADDB")) throw new ArgumentException("Must start with !ADDB.");


            if (firstLine.Trim().Length>5 && firstLine.Substring(5).Trim() != null && firstLine.Substring(5).Trim() != "")
            {
                if (firstLine.Substring(5).Trim().Contains(" "))
                {
                    _referenceAddress  = firstLine.Substring(5).Trim().Substring(0, firstLine.Substring(5).Trim().LastIndexOf(" "));
                     _blacklistedAddress = firstLine.Substring(5).Trim().Substring(firstLine.Substring(5).Trim().LastIndexOf(" ") + 1);
                    return new SMTPCommandParseResult(new MetaADDBlacklistCommandToExecute(_blacklistedAddress, _referenceAddress));
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
