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
        MailAddressCollection _recipients;
        public MailMessage mail = new MailMessage();
        bool _ready = false;

        public bool IsInitialized { get { return _domainName != null; } }

        public MailAddressCollection Recipients
        {
            get { return _recipients; }
        }


        public void Initialize( string domainName )
        {
            if (domainName == null) throw new ArgumentNullException("domainName");
            _domainName = domainName;
        }

        public void AddRecipient( string mailAddress )
        {
            _recipients.Add(new MailAddress(mailAddress));
        }

        public void AddSender(string mailAddress)
        {
            mail.Sender = new MailAddress(mailAddress);
            mail.From = new MailAddress(mailAddress);
        }
    

        public void Clear()
        {
        }

        public bool IsReady()
        {
            return _ready;
        }
        public void SetReadyToSend()
        {
            _ready = true;
        }

    }
}
