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
    public class AddAdrCommand
    {
        User _user;
        Session _session;
        Client _client;
        string _adress;
        string _description;
        string _relayAdress;
        string _message;


        public AddAdrCommand(User user, Session session, Client client, string adress, string description, string relayAdress)
        {
            _user = user;
            _session = session;
            _client = client;
            _adress = adress;
            _description = description;
            _relayAdress = relayAdress;
            _message = "!ADDA " + _adress + " " + _relayAdress;
        }

        public bool Execute()
        {
            bool worked;
            _client.Connect(_user.Username, _user.Password);
            _client.Send(_message);
            string response = _client.Waitresponse();
            if (response.Trim().StartsWith("720"))
            {
                _client.Send(_description);
                response = _client.Waitresponse();
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
