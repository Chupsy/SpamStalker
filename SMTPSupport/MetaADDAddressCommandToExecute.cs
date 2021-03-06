﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
{
    internal class MetaADDAddressCommandToExecute : SMTPCommandToExecute
    {
        string _newAddress;
        string _relayAddress;
        string _description;

        public MetaADDAddressCommandToExecute(string newAddress, string relayAddress)
        {
            _newAddress = newAddress;
            _relayAddress = relayAddress;
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
            if (session.MetaSession.MetaAPI.FindUserAddress(_newAddress) != null)
            {
                client.SendError(ErrorCode.AddressUsed);
                return;
            }
            client.SendError(ErrorCode.GetDescription);
            _description = client.Meta.ReadLine();
            client.SendError(ErrorCode.Ok);
            session.MetaSession.User.AddAddress( _newAddress, _relayAddress, _description);
            session.MetaSession.MetaAPI.WriteUser(session.MetaSession.User);
            session.MetaAPI.ResetUsers();
        }

    }

}
