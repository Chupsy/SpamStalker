using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class SMTPParser
    {
        Dictionary<string,SMTPCommand> _commands;

        public SMTPParser()
        {
            _commands = new Dictionary<string, SMTPCommand>();
            RegisterCommand( new RCPTCommand() );
            SMTPCommand helloCmd = RegisterCommand( new EHLOCommand() );
            _commands.Add( "HELO", helloCmd );
        }

        SMTPCommand RegisterCommand( SMTPCommand cmd )
        {
            _commands.Add( cmd.Name, cmd );
            return cmd;
        }


    }
}
