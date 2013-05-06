﻿using System;
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
        public MailMessage mail = new MailMessage();
        bool _ready = false;
        MailAddress _sender;

        public SMTPSession()
        {
        }

        public bool IsInitialized { get { return _domainName != null; } }


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
            mail.From = _sender;
            mail.ReplyToList.Add(_sender);
            _ready = true;
        }

    }
}
