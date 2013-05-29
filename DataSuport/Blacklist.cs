using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace DataSuport
{
    class Blacklist
    {
        List<MailAddress> _list;

        public Blacklist(List<MailAddress> list)
        {
            _list = list;
        }

        public List<MailAddress> list
        {
            get { return _list; }
            set { _list = value; }
        }
    }
}
