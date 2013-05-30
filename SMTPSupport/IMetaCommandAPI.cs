using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPSupport
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

        void AddAddress(string user, string newAddress, string relayAddress, string description);

        void RemoveAddress(string user, string Address);

        bool Identify(string user, string password);

        ServerStatus Status { get; }
    }
}
