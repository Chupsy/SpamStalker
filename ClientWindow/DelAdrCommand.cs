﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;

namespace ClientWindow
{
    public class DelAdrCommand
    {
        Session _session;
        Client _client;
        String _adress;
        string _message;

        public DelAdrCommand(Session session, Client client, string adress)
        {
            _session = session;
            _client = client;
            _adress = adress;

            _message = "!RMVA " + _adress;
        }

        public void Execute()
        {
            
            string data;
            _client.Connect(_session.Username, _session.Password);
            _client.Send(_message);
            string response = _client.Waitresponse();
            if (response == "250 OK")
            {
                data = _client.GetData();
                //_session.Data = data;
            }
            _client.CloseStream();
        }


    }


}