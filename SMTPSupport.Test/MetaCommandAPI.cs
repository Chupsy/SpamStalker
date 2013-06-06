using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTPSupport;

namespace SMTPSupport.Test
{
    class MetaCommandAPI : IMetaCommandAPI
    {

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }


        public ServerStatus Status
        {
            get { throw new NotImplementedException(); }
        }
    }
}
