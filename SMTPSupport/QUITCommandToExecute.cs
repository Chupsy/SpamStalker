﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SMTPSupport
{
    internal class QUITCommandToExecute : SMTPCommandToExecute
    {

        public QUITCommandToExecute()
        {
        }

        public override void Execute( SMTPSession session, SMTPCallingClient client )
        {
                client.SendError(221, "<SpamStalker> Service closing transmission channel");
                Thread.Sleep(500);
                client.Close();
        }

    }

}