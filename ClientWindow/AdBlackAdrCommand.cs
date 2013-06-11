using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;
using System.Windows.Forms;

namespace ClientWindow
{
    public class AdBlackAdrCommand
    {
        User _user;
        Client _client;
        string _address;
        string _message;
        string _blackAddress;
        Session _session;

        public AdBlackAdrCommand(User user, Client client, string address, string blackAddress, Session session)
        {
            _session = session;
            _user = user;
            _client = client;
            _address = address;
            _blackAddress = blackAddress;

            _message = "!ADDB " + _address +" " +_blackAddress;
        }

        public bool Execute()
        {
            bool worked;
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
                worked = false;
            }
            _client.CloseStream();
            return worked;
        }


    }


}
