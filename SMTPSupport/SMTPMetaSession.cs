using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using DataSupport;

namespace SMTPSupport
{
    public class SMTPMetaSession
    {
        readonly SMTPSession _session;
        readonly IMetaCommandAPI _metaAPI;
        string _userName;
        string _typeOfAccount;
        User _currentUser;

        internal SMTPMetaSession( SMTPSession session, IMetaCommandAPI metaAPI )
        {
            _session = session;
            _metaAPI = metaAPI;
        }

        public User User
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public SMTPSession Session { get { return _session; } }

        public bool IsInitialized { get { return _userName != null; } }

        public void Initialize(string userName)
        {
            if (userName == null) throw new ArgumentNullException("userName");
            _userName = userName;
        }

        public IMetaCommandAPI MetaAPI { get { return _metaAPI; } }

        public string TypeOfAccount
        {
            get
            {
                return _typeOfAccount;
            }
            set
            {
                _typeOfAccount = value;
            }
        }



    }
}
