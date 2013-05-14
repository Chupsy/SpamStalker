using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string serverIP = "localhost";
            string message = "Helloa";
            client.Connect(serverIP, message);

            Application.Run(new Form1());

           
        }
    }
}
