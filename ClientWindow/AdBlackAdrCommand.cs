﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;

namespace ClientWindow
{
    public class AdBlackAdrCommand
    {
        User _session;
        Client _client;
        string _address;
        string _message;
        string _blackAddress;

        public AdBlackAdrCommand(User session, Client client, string address, string blackAddress)
        {
            _session = session;
            _client = client;
            _address = address;
            _blackAddress = blackAddress;

            _message = "!ADDB " + _address +" " +_blackAddress;
        }

        public void Execute()
        {
            _client.Connect(_session.Username, _session.Password);
            _client.Send(_message);
            string response = _client.Waitresponse();
            if (response == "250 OK")
            {
                _session = _client.GetData();               
            }
            _client.CloseStream();
        }


    }


}