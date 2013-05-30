using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace SMTPSupport
{
    public class SMTPMetaSession
    {
        readonly SMTPSession _session;
        readonly IMetaCommandAPI _metaAPI;
        string _userName;

        internal SMTPMetaSession( SMTPSession session, IMetaCommandAPI metaAPI )
        {
            _session = session;
            _metaAPI = metaAPI;
        }

        public SMTPSession Session { get { return _session; } }

        public bool IsInitialized { get { return _userName != null; } }

        public void Initialize(string userName)
        {
            if (userName == null) throw new ArgumentNullException("userName");
            _userName = userName;
        }

        public IMetaCommandAPI MetaAPI { get { return _metaAPI; } }


    }
}
