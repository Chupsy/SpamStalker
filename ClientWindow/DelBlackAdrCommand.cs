using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSupport;

namespace ClientWindow
{
    public class DelBlackAdrCommand
    {
        User _session;
        Client _client;
        string _adress;
        string _message;

        public DelBlackAdrCommand(User session, Client client, string adress)
        {
            _session = session;
            _client = client;
            _adress = adress;

            _message = "!RMVB " + _adress;
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
                _session = User.ParseInfos(data);
               
            }
            _client.CloseStream();
        }


    }


}
