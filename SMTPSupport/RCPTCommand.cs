using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class RCPTCommand : SMTPCommand
    {
        public RCPTCommand()
            : base( "RCPT", "Adds a recipient mail address." )
        {
        }

        internal override SMTPCommandParseResult Parse( string firstLine )
        {
            if( !firstLine.StartsWith( "RCPT" ) ) throw new ArgumentException( "Must start with RCPT." );

            string extractedMail = null;

            if (firstLine.StartsWith("RCPT TO:<") && firstLine.EndsWith(">"))
            {
                extractedMail = firstLine.Substring("RCPT TO:<".Length).Trim();
                extractedMail = extractedMail.Remove(extractedMail.Length - 1);
            }
            else
            {
                return new SMTPCommandParseResult(500);
            }

            if (extractedMail != "" && extractedMail != null)
            {
                if (!CheckAdress(extractedMail))
                {
                    return new SMTPCommandParseResult(new RCPTCommandToExecute(extractedMail,550));
                }
            }
            else
            {
                return new SMTPCommandParseResult( 501 );
            }

            return new SMTPCommandParseResult(new RCPTCommandToExecute(extractedMail));
        }

        /// <summary>
        /// Verify if the extracted mail exists in the adresses in adress.txt
        /// </summary>
        /// <param name="extractedMail"> extracted mail u.u </param>
        /// <returns></returns>
        private bool CheckAdress(string extractedMail)
        {
            string[] addresses = System.IO.File.ReadAllLines(@"..\..\..\FakeSmtp\adresses.txt");
            foreach (string address in addresses)
            {
                if (address == extractedMail)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
