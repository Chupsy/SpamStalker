using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class EHLOCommandToExecute : SMTPCommandToExecute
    {
        string _domainName;

        public EHLOCommandToExecute( string domainName )
        {
            _domainName = domainName;
        }

        public override void Execute( SMTPSession session )
        {
            throw new NotImplementedException();
        }
    }

}
