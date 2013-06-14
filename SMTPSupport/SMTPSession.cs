using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Diagnostics;
using DataSupport;

namespace SMTPSupport
{
    public class SMTPSession
    {
        readonly IMetaCommandAPI _metaAPI;
        string _domainName;
        public MailMessage mail = new MailMessage();
        bool _ready = false;
        MailAddress _sender;
        bool _knownAdress;
        SMTPMetaSession _meta;
        MailAddressCollection _mailAdressBlacklist;

        public SMTPSession( IMetaCommandAPI metaAPI )
        {
            _metaAPI = metaAPI;
            _knownAdress = false;
        }

        public IMetaCommandAPI MetaAPI
        {
            get { return _metaAPI; }
        }

        public bool IsInitialized { get { return _domainName != null; } }

        public bool KnownAdress
        {
            get { return _knownAdress; }
            set { _knownAdress = value; }
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
            mail.To.Add(mailAddress);
        }

        public void AddSender(string mailAddress)
        {
            _sender = new MailAddress(mailAddress);
            mail.Sender = _sender;
        }

        public void SpamReact(MailAddressCollection mailAdressBlacklist)
        {
            _mailAdressBlacklist = mailAdressBlacklist;
        }
    
        public void Clear()
        {
           
            mail.To.Clear();
            _sender = null;
        }
        
        public bool IsReady()
        {
            return _ready;
        }

        public void SetReadyToSend()
        {
            if (_knownAdress == true)
            {
                mail.From = _sender;
                mail.ReplyToList.Add(_sender);
                _ready = true;
                if (_mailAdressBlacklist != null)
                {
                    foreach (MailAddress address in _mailAdressBlacklist)
                    {
                        mail.To.Remove(address);
                    }
                }
            }
            else
            {
                _ready = false;
            }
        }

        internal void EnableMetaSession()
        {
            Debug.Assert( _meta == null, "EnableMetaSession has already been called." );
            _meta = new SMTPMetaSession(this, _metaAPI);
        }

        public bool HasMetaSession
        {
            get { return _meta != null; }
        }

        public SMTPMetaSession MetaSession
        {
            get { return _meta; }
        }

        internal bool IsBlacklisted(User user, string receptionAddress, string mailFrom)
        {
            foreach (Address b in user.Addresses)
            {
                if (b.SubscriptionAddress == receptionAddress)
                {
                    foreach (BlackEmailAddress blackMail in b.Blacklist)
                    {
                        if (blackMail.Address == mailFrom) return true;
                        else return false;
                    }
                }
                else return false;
            }
            return false;
        }

        internal bool IsFucked(User user, string receptionAddress, string mailFrom)
        {
            foreach (Address b in user.Addresses)
            {
                if (b.SubscriptionAddress == receptionAddress)
                {
                    foreach (BlackEmailAddress blackMail in b.Blacklist)
                    {
                        if (blackMail.IsFucking == true) return true;
                        else if (blackMail.IsFucking == false) return false;
                    }
                }
                else return false;
            }
            return false;
        }

    }
}
