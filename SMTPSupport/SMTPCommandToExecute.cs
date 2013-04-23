using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public abstract class SMTPCommandToExecute
    {
        public abstract void Execute( SMTPSession session );
    }
}
