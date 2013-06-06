using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaADDAddressCommand : SMTPCommand
    {
        string _newAddress;
        string _relayAddress;
        public MetaADDAddressCommand()
            : base("!ADDA", "Add new mail address")
        {
        }

        internal override SMTPCommandParseResult Parse(string firstLine)
        {
            if (!firstLine.StartsWith("!ADDA")) throw new ArgumentException("Must start with !ADDA.");


            if (firstLine.Trim().Length>5 && firstLine.Substring(5).Trim() != null && firstLine.Substring(5).Trim() != "")
            {
                if (firstLine.Substring(5).Trim().Contains(" "))
                {
                    _newAddress = firstLine.Substring(5).Trim().Substring(0, firstLine.Substring(5).Trim().LastIndexOf(" "));
                    _relayAddress = firstLine.Substring(5).Trim().Substring(firstLine.Substring(5).Trim().LastIndexOf(" ") + 1);
                    return new SMTPCommandParseResult(new MetaADDAddressCommandToExecute(_newAddress, _relayAddress));
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
