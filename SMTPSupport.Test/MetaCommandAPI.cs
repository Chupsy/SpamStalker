using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTPSupport;
using System.Net.Mail;

namespace SMTPSupport.Test
{
    class MetaCommandAPI : IMetaCommandAPI
    {

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void AddAddress(string user, string newAddress, string relayAddress, string description)
        {
            throw new NotImplementedException();
        }


        public void RemoveAddress(string address, string username)
        {
            throw new NotImplementedException();
        }


        public string Identify(string user, string password)
        {
            throw new NotImplementedException();
        }


        public List<string> GetAllInformations(string username)
        {
            throw new NotImplementedException();
        }


        public bool CheckAddress(string address)
        {
            throw new NotImplementedException();
        }


        public bool CheckAddressBelonging(string rmvAddress, string username)
        {
            throw new NotImplementedException();
        }


        public bool CheckUser(string username)
        {
            throw new NotImplementedException();
        }


        public void CreateUser(string username, string password, string typeOfAccount)
        {
            throw new NotImplementedException();
        }


        public void DeleteUser(string username)
        {
            throw new NotImplementedException();
        }


        public void ModifyType(string username, string value)
        {
            throw new NotImplementedException();
        }

        public void ModifyPassword(string username, string value)
        {
            throw new NotImplementedException();
        }


        public MailAddressCollection CheckSpammer(MailAddressCollection recipientCollection, string sender)
        {
            throw new NotImplementedException();
        }


        public ServerStatus Status
        {
            get { throw new NotImplementedException(); }
        }
    }
}
