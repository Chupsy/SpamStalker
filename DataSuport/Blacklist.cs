using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace DataSupport
{
    public class Blacklist
    {
        List<BlackEmailAddress> _list;

        public Blacklist(List<BlackEmailAddress> list)
        {
            _list = list;
        }

        public List<BlackEmailAddress> list
        {
            get { return _list; }
            set { _list = value; }
        }
    }
}
