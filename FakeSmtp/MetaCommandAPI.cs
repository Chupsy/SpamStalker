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


        public string Identify(string username, string password)
        {
            string identify = _server.Identify(username, password);
            if (identify != null)
            {
                user = _server.SetUser(username);
            }
            return identify;
        }

        public bool CheckUser(string username)
        {
            return _server.CheckUser(username);
        }

        public void CreateUser(string username, string password, string mainAddress, string typeOfAccount)
        {
            _server.CreateUser(username, password, mainAddress, typeOfAccount);
        }

        public void DeleteUser(string username)
        {
            _server.DeleteUser(username);
        }

        public void ModifyType(string username, string value)
        {
            _server.ModifyType(username, value);
        }

        public void ModifyPassword(string username, string value)
        {
            _server.ModifyPassword(username, value);
        }

        public void AddAddress(string username, string newAddress, string relayAddress, string description) 
        {
            _server.AddAddress(username, newAddress, relayAddress, description);      
        }

        public void RemoveAddress(string address, string username) 
        {
             _server.RemoveAddress(address, username);
        }

        public string GetAllInformations(string username) 
        {
            return _server.GetAllInformations(username); 
        }

        public bool CheckAddress(string address) { return _server.CheckAddress(address); }

        public bool CheckAddressBelonging(string belongAddress, string username) { return _server.CheckAddressBelonging(username, belongAddress); }

        public MailAddressCollection CheckAllSpammer(MailAddressCollection recipientCollection, string sender) { return _server.CheckAllSpammer(recipientCollection, sender); }


        public bool CheckSpammer(string username, string userAddress, string blacklistedAddress) { return _server.CheckSpammer(username, userAddress, blacklistedAddress); }

        public void AddBlacklistAddress(string username, string referenceAddress, string blacklistedAddress) 
        {
            _server.AddBlacklistAddress(username, referenceAddress, blacklistedAddress);
        }

        public void RmvBlacklistAddress(string username, string referenceAddress, string blacklistedAddress) { }

        public void ModBlacklistAddress(string username, string referenceAddress, string blacklistedAddress, string blacklistMod) { }


        public ServerStatus Status
        {
            get {
                return _server.Status;
            }
        }
    }
}
