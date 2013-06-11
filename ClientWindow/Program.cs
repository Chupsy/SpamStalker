using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSupport;

namespace ClientWindow
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Client client = new Client();
            Session session = new Session();

            Application.Run(new Form2(client, session));

            if (session != null && session.IsInitialized == true && session.User != null)
            {
                Application.Run(new Form1(client, session.User, session.User.Username));
            }
        }
    }
}
