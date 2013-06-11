﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;
using System.Windows.Forms;

namespace ClientWindow
{
    public class DelAdrCommand
    {
        User _user;
        Client _client;
        Session _session;
        string _adress;
        string _message;

        public DelAdrCommand(User user, Client client, string adress, Session session)
        {
            _session = session;
            _user = user;
            _client = client;
            _adress = adress;

            _message = "!RMVA " + _adress;
        }


        public bool Execute()
        {
            bool worked = false;
            _client.Connect(_user.Username, _user.Password);
            _client.Send(_message);
            string response = _client.Waitresponse();
            if (response == "250 OK")
            {
                _session.User = _client.GetData();
                worked = true;
            }
            else
            {
                MessageBox.Show(response);
            }
            _client.CloseStream();
            return worked;
        }
    }
}
