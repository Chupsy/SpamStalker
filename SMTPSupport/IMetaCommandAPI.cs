using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

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

        /// <summary>
        /// Ajout d'une adresse pour un utilisateur dans son fichier
        /// return true if added with success else return false
        /// </summary>
        void AddAddress(string user, string newAddress, string relayAddress, string description);

        /// <summary>
        /// Suppression d'une adresse pour un utilisateur dans son fichier
        /// return true if remove with success else return false
        /// </summary>
        void RemoveAddress( string address, string username);

        /// <summary>
        /// Identification de l'utilisateur, return son typeOfAccount si il est bien identifié, null sinon.
        /// </summary>
        string Identify(string user, string password);

        /// <summary>
        /// Liste de toutes les informations a envoyer au client
        /// foreach derriere
        /// </summary>
        string GetAllInformations(string username);

        /// <summary>
        /// Verification si l'adresse existe deja ou pas, return true si elle existe, false sinon.
        /// </summary>
        bool CheckAddress(string address);

        /// <summary>
        /// Verification si l'adresse existe et si oui, si elle appartient bien a l'utilisateur. return true si c'est le cas, false sinon
        /// </summary>
        bool CheckAddressBelonging(string belongAddress, string username);

        /// <summary>
        /// Verification de l'existance du compte.
        /// Il existe => true
        /// Il existe pas => false
        /// </summary>
        bool CheckUser(string username);

        /// <summary>
        /// Créé le compte avec ce username, password et type de compte.
        /// Vérifier avec un debug.assert que le compte n'existe pas quand meme.
        /// </summary>
        void CreateUser(string username, string password, string mainAddress, string typeOfAccount);

        /// <summary>
        /// Supprime le compte avec ce username.
        /// Verifier qu'il existe avec un debug.Assert.
        /// </summary>
        void DeleteUser(string username);

        /// <summary>
        /// Modifie le type de compte (Admin ou utilisateur normal)
        /// </summary>
        void ModifyType(string username, string value);

        /// <summary>
        /// Modifie le password de l'utilisateur à value.
        /// </summary>
        void ModifyPassword(string username, string value);

        /// <summary>
        /// Vérifie si une des adresses a le sender dans sa blacklist. return une liste des adresse qui ont ce sender dans leur blacklist si oui, null sinon.
        /// </summary>
        MailAddressCollection CheckAllSpammer(MailAddressCollection recipientCollection, string sender);

        bool CheckSpammer(string username, string userAddress, string blacklistedAddress);

        void AddBlacklistAddress(string username, string referenceAddress, string blacklistedAddress);

        void RmvBlacklistAddress(string username, string referenceAddress, string blacklistedAddress);

        void ModBlacklistAddress(string username, string referenceAddress, string blacklistedAddress, string blacklistMod);

        ServerStatus Status { get; }

    }
}
