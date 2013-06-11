using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTPSupport;
using System.Net.Mail;
using DataSupport;

namespace FakeSmtp
{
    public class MetaCommandAPI : IMetaCommandAPI
    {
        SMTPServer _server;
        User user;

        public MetaCommandAPI(SMTPServer server)
        {
            _server = server;
        }
        public void Shutdown()
        {
            _server.ShutDown();
        }

        public void Pause()
        {
            
        }

        public void Resume()
        {
           
        }

        public User FindUser(string username)
        {
            User u = _server.FindUser(username);
            if(u != null)u.Username = username;
            return u;
        }

        public Address FindUserAddress(string subscriptionAddress)
        {
            return _server.FindUserAddress(subscriptionAddress);
        }

        public void WriteUser(User u)
        {
            _server.Write(u);
        }

        public void DeleteUser(string username)
        {
            _server.DeleteUser(username);
        }

        public MailAddressCollection CheckAllSpammer(MailAddressCollection recipientCollection, string sender) { return _server.CheckAllSpammer(recipientCollection, sender); }

        public bool CheckUserExist(string username) { return _server.CheckUserExist(username); }

        public ServerStatus Status
        {
            get {
                return _server.Status;
            }
        }
    }
}
