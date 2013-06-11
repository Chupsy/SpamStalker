using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using DataSupport;

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
        /// <summary>
        /// Shutdown du serveur, fin de la boucle while dans le programme.cs
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Pause du serveur, on met en pause la boucle while et on doit pouvoir la relancer
        /// </summary>
        void Pause();

        /// <summary>
        /// Relancement du serveur, on doit verifier que le serveur etait bien en pause
        /// </summary>
        void Resume();


 User FindUser(string username);

        Address FindUserAddress(string subscriptionAddress);

        void WriteUser( User u );


        /// <summary>
        /// Supprime le compte avec ce username.
        /// Verifier qu'il existe avec un debug.Assert.
        /// </summary>
        void DeleteUser(string username);

        /// <summary>
        /// Vérifie si une des adresses a le sender dans sa blacklist. return une liste des adresse qui ont ce sender dans leur blacklist si oui, null sinon.
        /// </summary>
        MailAddressCollection CheckAllSpammer(MailAddressCollection recipientCollection, string sender);

        bool CheckUserExist(string username);

        ServerStatus Status { get; }

    }
}
