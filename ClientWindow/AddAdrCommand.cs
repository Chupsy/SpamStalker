using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;

namespace ClientWindow
{
    public class AddAdrCommand
    {
        User _session;
        Client _client;
        string _adress;
        string _description;
        string _relayAdress;
        string _message;

        public AddAdrCommand(User session, Client client, string adress, string description, string relayAdress)
        {
            _session = session;
            _client = client;
            _adress = adress;
            _description = description;
            _relayAdress = relayAdress;
            _message = "!ADDA " + _adress + " " + _description;

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
