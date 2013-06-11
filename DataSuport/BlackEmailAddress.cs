using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSupport
{
    public class BlackEmailAddress
    {
        readonly string _address;
        bool _isFucking;

        public BlackEmailAddress(string address, bool isFucking = false)
        {
            _address = address;
            _isFucking = isFucking;
        }

        public string Address
        {
            get { return _address; }
        }

        public bool IsFucking
        {
            get { return _isFucking; }
            set { _isFucking = value; }
        }

        public void Write( TextWriter stream)
        {
            stream.WriteLine("{0} {1}", IsFucking ? "fuck: " : "ignore: ", Address);
        }

        public static BlackEmailAddress Read(string line)
        {
            BlackEmailAddress a;
            string parse;
            if(User.ParseLine(line , "fuck", out parse))
            {
                a = new BlackEmailAddress(parse);
                a._isFucking = true;
            }
            else if (parse == null && User.ParseLine(line, "ignore", out parse))
            {
                a = new BlackEmailAddress(parse);
                a._isFucking = false;
            }
            else
            { return null; }

            return a;
        }
    }
}
