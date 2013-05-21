using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeSmtp
{

    public enum ServerStatus
    {
        Running,
        ShuttingDown,
        Paused
    }

    public interface IMetaCommandAPI 
    {
        void Shutdown();

        void Pause();

        void Resume();

        ServerStatus Status { get; }
    }
}
