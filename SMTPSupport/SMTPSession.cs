using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace SMTPSupport
{
    public class SMTPSession
    {
        string _domainName;
        List<string> _recipients = new List<string>();
        MailAddress _sender;

        public bool IsInitialized { get { return _domainName != null; } }

        public List<string> Recipients
        {
            get { return _recipients; }
        }

        public MailAddress Sender
        {
            get { return _sender; }
        }

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
            _sender = new MailAddress(mailAddress);
        }
    

        public void Clear()
        {
            _recipients.Clear();
            _sender = null;
        }

        public void SetReadyToSend()
        {
        }

    }
}
