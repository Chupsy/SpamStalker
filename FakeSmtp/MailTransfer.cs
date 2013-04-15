using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace FakeSmtp
{
    public class MailTransfer
    {
        string _to;
        string _from;
        string _cc;
        string _subject;
        string _body;

        public MailTransfer(string to, string from, string cc, string subject, string body)
        {
            _to = to;
            _from = from;
            _cc = cc;
            _subject = subject;
            _body = body;
        
        }

        public string To
        {
            get { return _to; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _to = value;
            }
        }

        public string From
        {
            get { return _from; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _from = value;
            }
        }

        public string Cc
        {
            get { return _cc; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _cc = value;
            }
        }

        public string Subject
        {
            get { return _subject; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _subject = value;
            }
        }

        public string Body
        {
            get { return _body; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _body = value;
            }
        }
    }
}
