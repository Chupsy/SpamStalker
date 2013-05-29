using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSuport
{
    class Description
    {
        string _description;

        Description(string description)
        {
            _description = description;
        }

        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

    }
}
