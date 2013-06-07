using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaCREUserCommand : SMTPCommand
    {
        public MetaCREUserCommand()
            : base( "!CREU", "Creates a new user [Admin Command]" )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "!CREU" )) throw new ArgumentException( "Must start with !CREU." );
            
            string username = null;
            string password = null;
            string typeOfAccount = null;
            string mainAddress = null;

            if (firstLine.Substring(5).Trim() != null && firstLine.Substring(5).Trim() != "")
            {
                if (firstLine.Substring(5).Trim().Contains(" "))
                {
                    username = firstLine.Substring(5).Trim().Substring(0, firstLine.Substring(5).Trim().IndexOf(" "));

                    password = firstLine.Substring(5).Trim().Substring(firstLine.IndexOf(" ")).Trim();
                    password = password.Substring(0, password.IndexOf(" ")).Trim();

                    mainAddress = firstLine.Substring(5).Trim().Substring(0, firstLine.LastIndexOf(" ")).Trim();
                    mainAddress = mainAddress.Substring(mainAddress.LastIndexOf(" ") + 1).Trim();

                    typeOfAccount = firstLine.Substring(5).Trim().Substring(firstLine.Substring(5).Trim().LastIndexOf(" ") + 1);

                    return new SMTPCommandParseResult(new MetaCREUserCommandToExecute(username, password, mainAddress, typeOfAccount));

                }
            }


            return new SMTPCommandParseResult(ErrorCode.Unrecognized);
        }
    }

}
