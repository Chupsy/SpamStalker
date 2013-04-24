using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    public class SMTPSession
    {
        string _domainName;

        public bool IsInitialized { get { return _domainName != null; } }

        public void Initialize( string domainName )
        {
            if (domainName == null) throw new ArgumentNullException("domainName");
            _domainName = domainName;
        }

        public void AddRecipient( string mailAddres )
        {
        }

        public void AddSender(string mailAddres)
        {
        }
    

        public void Clear()
        {
        }

        public void SetReadyToSend()
        {
        }

    }
}
