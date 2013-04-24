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

            if( cmdExecute.ErrorMessage != null )
            {
                client.SendError( cmdExecute.ErrorCode, cmdExecute.ErrorMessage );
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
