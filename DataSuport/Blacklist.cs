using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace DataSuport
{
    public class Blacklist
    {
        List<EmailAddress> _list;

        public Blacklist(List<EmailAddress> list)
        {
            _list = list;
        }

        public List<EmailAddress> list
        {
            get { return _list; }
            set { _list = value; }
        }
    }
}
