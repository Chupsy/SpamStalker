using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace SMTPSupport
{
    public class SMTPMetaSession : SMTPSession
    {
        string _userName;

        public SMTPMetaSession()
        {
        }

        new public bool IsInitialized { get { return _userName != null; } }

        new public void Initialize(string userName)
        {
            if (userName == null) throw new ArgumentNullException("userName");
            _userName = userName;
        }
    }
}
