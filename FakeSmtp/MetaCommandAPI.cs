﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTPSupport;
using System.Net.Mail;

namespace FakeSmtp
{
    public class MetaCommandAPI : IMetaCommandAPI
    {
        SMTPServer _server;

        public void Shutdown()
        {
            _server.ShutDown = true;
        }

        public void Pause()
        {
            _server.Pause = true;
        }

        public void Resume()
        {
            _server.Pause = false;
        }


        public string Identify(string user, string password)
        {
            return _server.Identify(user, password);
        }

        public bool CheckUser(string username)
        {
            return _server.CheckUser(username);
        }

        public void CreateUser(string username, string password, string typeOfAccount)
        {
            _server.CreateUser(username, password, typeOfAccount);
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

        public bool AddAddress(string username, string newAddress, string relayAddress, string description) 
        {
            return _server.AddAddress(username, newAddress, relayAddress, description);      
        }

        public bool RemoveAddress(string address, string username) 
        {
            return _server.RemoveAddress(address, username);
        }

        public List<string> GetAllInformations(string username) { return null; }

        public bool CheckAddress(string address) { return false; }

 
        public bool CheckAddressBelonging(string rmvAddress, string username) { return false; }

        public MailAddressCollection CheckSpammer(MailAddressCollection recipientCollection, string sender) { return null; }



        public ServerStatus Status
        {
            get 
            {
                if (_server.Pause == true && _server.ShutDown == false)
                {
                    return ServerStatus.Paused;
                }
                else if (_server.ShutDown == true)
                {
                    return ServerStatus.ShuttingDown;
                }
                else
                {
                    return ServerStatus.Running;
                }
            }
        }
    }
}
