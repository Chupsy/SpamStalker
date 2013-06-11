using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;

namespace DataSupport
{
    public class Blacklist : List<BlackEmailAddress>
    {
        public Blacklist()
        {
        }

        public BlackEmailAddress Find(string address)
        {
            foreach (var a in this)
                if (a.Address == address) return a;
            return null;
        }

        public BlackEmailAddress Assume(string address, bool fuck)
        {
            BlackEmailAddress a = FindOrCreate(address);
            a.IsFucking = fuck;
            return a;
        }

        public BlackEmailAddress FindOrCreate(string address)
        {
            BlackEmailAddress a = Find(address);
            if (a == null)
            {
                a = new BlackEmailAddress(address);
                Add(a);
            }
            return a;
        }

        public void Write( TextWriter stream)
        {
            foreach (BlackEmailAddress a in this) a.Write(stream);
        }
    }
}
