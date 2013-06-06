using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTPSupport;

namespace FakeSmtp
{
    class MetaCommandAPI : IMetaCommandAPI
    {
        SMTPServer _server;

        public void Shutdown()
        {
            _server.ShutDown = true;
        }

        public void Pause()
        {
            _server.Pause = true;
        }

        public void Resume()
        {
            _server.Pause = false;
        }


        public ServerStatus Status
        {
            get 
            {
                if (_server.Pause == true && _server.ShutDown == false)
                {
                    return ServerStatus.Paused;
                }
                else if (_server.ShutDown == true)
                {
                    return ServerStatus.ShuttingDown;
                }
                else
                {
                    return ServerStatus.Running;
                }
            }
        }
    }
}
