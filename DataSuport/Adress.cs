using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace DataSuport
{
    public class Adress
    {
        MailAddress _userAdress;
        Blacklist _adressBlacklist;
        Description _adressDescription;
        RelayAdress _relayAdress;

        public Adress(MailAddress userAdress, Blacklist adressBlacklist, Description adressDescription, RelayAdress relayAdress)
        {
            MailAddress _userAdress = userAdress;
            Blacklist _adressBlacklist = adressBlacklist;
            Description _adressDescription = adressDescription;
            RelayAdress _relayAdress = relayAdress; 
        }
    }
}
