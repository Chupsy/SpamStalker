using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class EHLOCommandToExecute : SMTPCommandToExecute
    {
        string _domainName;

        public EHLOCommandToExecute( string domainName )
        {
            _domainName = domainName;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if( session.IsInitialized )
            {
                client.SendError( 500 ); 
            }
            else
            {
                session.Initialize( _domainName );
                client.SendSuccess();
            }
        }

    }

}
