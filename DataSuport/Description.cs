using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSuport
{
    public class Description
    {
        string _content;

        Description(string content)
        {
            _content = content;
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

    }
}
