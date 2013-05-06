using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal abstract class SMTPCommand
    {
        public string Name { get; private set; }
        
        /// <summary>
        /// Gets the help for the command. 
        /// Never null (but can be empty).
        /// </summary>
        public string HelpText { get; private set; }

        protected SMTPCommand( string name, string helpText )
        {
            if( name == null || name.Length != 4 ) throw new ArgumentException( "A SMTP command must be 4 characters long." );
            Name = name;
            HelpText = helpText ?? String.Empty;
        }

        internal abstract SMTPCommandParseResult Parse( string firstLine );
    }


}
