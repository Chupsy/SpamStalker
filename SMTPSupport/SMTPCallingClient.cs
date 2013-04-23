using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class SMTPCallingClient
    {
        public virtual void SendError( int errorNumber )
        {
        }

        public virtual void SendSuccess()
        {
        }


        public virtual void Close()
        {
      
        }

        public virtual void SendError(int p1, string p2)
        {
            
        }
    }
}
