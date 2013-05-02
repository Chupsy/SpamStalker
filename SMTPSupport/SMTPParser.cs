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
            RegisterCommand(new MAILCommand());
            RegisterCommand(new HELPCommand(this));
            RegisterCommand(new NOOPCommand());
            RegisterCommand(new QUITCommand());
            RegisterCommand(new DATACommand());
            RegisterCommand(new RSETCommand());
        }

        SMTPCommand RegisterCommand( SMTPCommand cmd )
        {
            _commands.Add( cmd.Name, cmd );
            return cmd;
        }

        public IEnumerable<SMTPCommand> Commands { get { return _commands.Values; } }

        public void Execute( string commandLine, SMTPSession session, SMTPCallingClient client )
        {
            if( commandLine == null ) throw new ArgumentNullException( "commandLine" );

            SMTPCommand cmd = FindCommand( commandLine );
            if( cmd == null )
            {
                client.SendError( 500 );
                if( !session.IsInitialized ) client.Close();
                return;
            }

            SMTPCommandParseResult cmdExecute = cmd.Parse( commandLine );

            if( cmdExecute.Command == null)
            {
                    client.SendError(cmdExecute.ErrorCode);
                    return;
            }

            cmdExecute.Command.Execute(session, client);
           
        }

        SMTPCommand FindCommand( string commandLine )
        {
            SMTPCommand result = null;
            if( commandLine.Length >= 4 )
            {
                string headCommand = commandLine.Substring( 0, 4 );
                _commands.TryGetValue( headCommand, out result );
            }
            return result;
        }




    }
}
