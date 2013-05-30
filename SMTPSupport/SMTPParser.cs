using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class SMTPParser
    {
        static Dictionary<string, SMTPCommand> _commands;

        public SMTPParser()
        {
        }

        static SMTPParser()
        {
            _commands = CreateCommands();
        }

        static Dictionary<string, SMTPCommand> CreateCommands()
        {
            var dic = new Dictionary<string, SMTPCommand>();
            RegisterCommand(dic, new RCPTCommand());
            SMTPCommand helloCmd = RegisterCommand( dic, new EHLOCommand());
            dic.Add("HELO", helloCmd);
            RegisterCommand(dic, new MAILCommand());
            RegisterCommand(dic, new HELPCommand());
            RegisterCommand(dic, new NOOPCommand());
            RegisterCommand(dic, new QUITCommand());
            RegisterCommand(dic, new DATACommand());
            RegisterCommand(dic, new RSETCommand());
            RegisterCommand(dic, new MetaEHLOCommand());
            return dic;
        }

        static SMTPCommand RegisterCommand( Dictionary<string, SMTPCommand> dic, SMTPCommand cmd )
        {
            dic.Add( cmd.Name, cmd );
            return cmd;
        }


        static Dictionary<string, SMTPCommand> CreateMetaCommands()
        {
            Dictionary<string, SMTPCommand> metaDic = new Dictionary<string, SMTPCommand>();
            RegisterMetaCommand(metaDic, new MetaEHLOCommand());
            RegisterMetaCommand(metaDic, new MetaCLOSECommand());
            RegisterMetaCommand(metaDic, new MetaADDAddressCommand());
            RegisterMetaCommand(metaDic, new MetaRMVAddressCommand());
            return metaDic;
        }   

        static SMTPCommand RegisterMetaCommand(Dictionary<string, SMTPCommand> metaDic, SMTPCommand cmd)
        {
            metaDic.Add(cmd.Name, cmd);
            return cmd;
        }

        public void EnableMetaCommands()
        {
            _commands = CreateMetaCommands();
        }

        static public IEnumerable<SMTPCommand> Commands 
        { 
            get 
            {
                return _commands.Values; 
            } 
        }


        static public SMTPCommand FindCommand(string commandLine)
        {
            SMTPCommand result = null;
            if (commandLine.Length >= 4)
            {
                string headCommand;
                if (commandLine.Length > 4)
                {
                    headCommand = commandLine.Substring(0, 5).Trim();
                }
                else
                {
                    headCommand = commandLine.Substring(0, 4).Trim();
                }
                _commands.TryGetValue(headCommand, out result);
            }
            return result;
        }

        public void Execute( string commandLine, SMTPSession session, SMTPCallingClient client )
        {
            if( commandLine == null ) throw new ArgumentNullException( "commandLine" );

            SMTPCommand cmd = FindCommand( commandLine );
            if( cmd == null )
            {
                client.SendError(ErrorCode.Unrecognized);
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

    }
}
