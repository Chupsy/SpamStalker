using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSuport;

namespace ClientWindow
{
    public class AddAdrCommand
    {
        Session _session;
        Client _client;
        String _adress;
        String _description;
        string _relayAdress;

        public AddAdrCommand(Session session, Client client, string adress, string description, string relayAdress)
        {
            _session = session;
            _client = client;
            _adress = adress;
            _description = description;
            _relayAdress = relayAdress;

        }

        void Execute(Session _session, Client _client, string _adress, string _description, string _relayAdress)
        {
            string message = "!ADDA " + _adress + " " + _description;
            List<Adress> data;
            _client.Connect(_session.username, _session.password);
            _client.Send(message);
            data = _client.GetData;
            _client.CloseStream();

        }


    }


}
