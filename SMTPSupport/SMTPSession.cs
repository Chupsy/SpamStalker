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
        List<string> _recipients = new List<string>();

        public bool IsInitialized { get { return _domainName != null; } }

        public void Initialize( string domainName )
        {
            if (domainName == null) throw new ArgumentNullException("domainName");
            _domainName = domainName;
        }

        public void AddRecipient( string mailAddress )
        {
            _recipients.Add(mailAddress);
        }

        public void AddSender(string mailAddress)
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
