﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;

namespace ClientWindow
{
    public class UnFuckCommand
    {
        User _session;
        Client _client;
        string _adress;
        string _message;
        string _blackAddress;
        bool _fuck;

        public UnFuckCommand(User session, Client client, string adress, string blackAddress)
        {
            _session = session;
            _client = client;
            _adress = adress;
            _blackAddress = blackAddress;
            _fuck = false;

            _message = "!MODB " + _adress +" "+_blackAddress + " " + _fuck;
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
