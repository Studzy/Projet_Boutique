using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using BoutiqueBDDLibrary;

namespace BoutiqueBDDLibrary
{
    #region Masque l'entrée lors de la saisi
    static class WIN32
    {
        [DllImport("kernel32.dll")]
        public extern static IntPtr GetStdHandle(int nHandle);
        [DllImport("kernel32.dll")]
        public extern static int SetConsoleMode(IntPtr handle, uint mode);
        [DllImport("kernel32.dll")]
        public extern static int GetConsoleMode(IntPtr handle, out uint mode);
        public const int INPUT_HANDLE = -10;
        public static readonly IntPtr INVALID_HANDLE_VALUE = (IntPtr)(-1);
        public const uint ENABLE_ECHO_INPUT = 0x0004;
    }
    /// <summary>
    /// Cache l'entrée utilisateur dans un block using.
    /// </summary>
    /// <example>
    /// using (new ConsoleInputHider())
    ///     ligne = Console.ReadLine();
    /// </example>
    class CacheurDentreeUtilisateur : IDisposable
    {
        //On stocke les options console pour les restaurer après.
        uint _oldMode;
        //On stocke la console pour la prochaine fois.
        static IntPtr? _consoleHandle = null;
        public CacheurDentreeUtilisateur()
        {
            if (_consoleHandle == null)
            {
                //Obtient l'objet console.
                _consoleHandle = WIN32.GetStdHandle(WIN32.INPUT_HANDLE);
                if (_consoleHandle == WIN32.INVALID_HANDLE_VALUE)
                    throw new Win32Exception();
            }
            //Obtient les options actuelles de la console.
            int err = WIN32.GetConsoleMode(_consoleHandle.Value, out this._oldMode);
            if (err == 0)
                new Win32Exception();
            //Applique les options en mettant "montre l'entrée untilisateur" à false.
            err = WIN32.SetConsoleMode(_consoleHandle.Value, this._oldMode & (~WIN32.ENABLE_ECHO_INPUT));
            if (err == 0)
                new Win32Exception();
        }
        bool _disposed = false;
        public void Dispose()
        {
            if (this._disposed)
                return;
            this._disposed = true;

            //Restaure les options console.
            int err = WIN32.SetConsoleMode(_consoleHandle.Value, this._oldMode);
            if (err == 0)
                new Win32Exception();
        }
    }
    #endregion
    public class FonctionsConsole
    {
        //Autres
        #region Masque le mot de passe
        /// <summary>
        /// Efface l'entrée saisi pour masquer un mot de passe
        /// </summary>
        /// <returns></returns>
        public static string MaskPassword()
        {
            using (new CacheurDentreeUtilisateur())
            {
                return Console.ReadLine();
            }
        }
        #endregion

        #region Change la première lettre en Majuscule
        /// <summary>
        /// Change la première lettre du string en Majuscule 
        /// </summary>
        /// <param name="caractere"></param>
        /// <returns></returns>
        public static string premiereLettreMajuscule(string caractere)
        {
            caractere = caractere[0].ToString().ToUpper() + caractere.Substring(1).ToLower();

            return caractere;
        }
        #endregion

        #region Recherche un ID true or false si trouver 
        /// <summary>
        /// Le resultat d'une recherche d'id.
        /// <see cref="Trouve"/> est true si un id a été trouvé.
        /// L'id est alors stocké dans <see cref="Id"/>.
        /// </summary>
        /// 
        public class IdTrouve
        {
            int id;
            public bool Trouve { get; }
            public int Id
            {
                get
                {
                    if (!this.Trouve)
                        throw new InvalidOperationException();
                    return this.id;
                }
            }
            /// <summary>
            /// L'id n'a pas été trouvé.
            /// </summary>
            public IdTrouve()
            {
                this.Trouve = false;
            }
            /// <summary>
            /// id est l'id qui a été trouvé.
            /// </summary>
            public IdTrouve(int id)
            {
                this.Trouve = true;
                this.id = id;
            }
        }
        #endregion

        //Admin et Client
        #region Vérifie le mot de passe ADMIN  ou CLIENT
        /// <summary>
        /// Vérifie si le mot de passe à au moins 1 chiffre 1 majuscule et 7 caractères minimum
        /// Si le mot de passe n'est pas bon il y a une exeption sinon il n'y en a pas 
        /// </summary>
        /// <param name="value"></param>
        public static void VerifMdp(string value)
        {
            if (value.Length < 1 || value.Length > 100)
                throw new FonctionsConsole.MonMessageErreur("ERREUR: Le mot de passe n'est pas valide (7 caractères mini | 1 chiffre | 1 Maj)");

            bool possedeUneLettre = false;
            foreach (char c in value)
            {
                bool aUneLettre = char.IsLetter(c);
                if (aUneLettre == true)
                {
                    possedeUneLettre = true;
                    break;
                }
            }
            if (!possedeUneLettre)
                throw new FonctionsConsole.MonMessageErreur("ERREUR: Le mot de passe n'est pas valide (7 caractères mini | 1 chiffre | 1 Maj)");

            bool possedeUnChiffre = false;
            foreach (char c in value)
            {
                bool aUnChiffre = char.IsDigit(c);
                if (aUnChiffre == true)
                {
                    possedeUnChiffre = true;
                    break;
                }
            }
            if (!possedeUnChiffre)
                throw new FonctionsConsole.MonMessageErreur("ERREUR: Le mot de passe n'est pas valide (7 caractères mini | 1 chiffre | 1 Maj)");

            bool possedeUneMajuscule = false;
            foreach (char c in value)
            {
                bool aUneMaj = char.IsUpper(c);
                if (aUneMaj == true)
                {
                    possedeUneMajuscule = true;
                    break;
                }
            }
            if (!possedeUneMajuscule)
                throw new FonctionsConsole.MonMessageErreur("ERREUR: Le mot de passe n'est pas valide (7 caractères mini | 1 chiffre | 1 Maj)");
        }
        #endregion

        #region Supprimer un client
        public static void SupprimeClient(string valeur)
        {
            List<int> IDMultiple = new List<int>();
            IDMultiple = DataAccess.recupeIdFactureEtIdCommande(valeur);
            if (IDMultiple[0] == 0)
            {
                DataAccess.supprimerUnClient(valeur);
            }
            else
            {
                DataAccess.recupeIdFactureEtIdCommande(valeur);
                DataAccess.supprimerUneCommande(IDMultiple[0]);
                DataAccess.supprimerInterFacturePaiement(IDMultiple[1]);
                DataAccess.supprimerUneFacture(IDMultiple[1]);
                DataAccess.supprimerUnClient(valeur);
            }
        }
        #endregion

        //Client
        #region Valide le mail lors de la modification d'un client
        /// <summary>
        /// Vérifie que le nouvel Email n'existe pas dans la base de donnée.
        /// Si non présent dans la BDD ou autres renvoi une exeption.
        /// </summary>
        /// <param name="value"></param>
        public static void ValiderNouvelEmail(string value)
        {
            if (value.Length < 1 || value.Length > 80 || !FonctionsConsole.VerifEmail(value) || DataAccess.VerifierExistanceMailClient(value))
            {
                throw new FonctionsConsole.MonMessageErreur("ERREUR: L'émail existe déjà | L'émail est trop long | Le format n'est pas bon.");
            }
        }
        #endregion

        #region Patern client pour la modification d'un client
        /// <summary>
        /// Modèle utiliser pour modifier un client
        /// </summary>
        /// <param name="x"></param>
        /// <param name="p"></param>
        /// <param name="v"></param>
        public static void PaternClient(string x, Client p, CpVille v)
        {
            #region Code Postal Client
            Console.Write("Code Postal: ");
            while (true)
            {
                try
                {
                    v.Code_postal_ville = Console.ReadLine();
                    break;
                }
                catch (FonctionsConsole.MonMessageErreur e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            #endregion

            #region Nom Ville Client
            while (true)
            {
                try
                {
                    Console.Write("Ville: ");
                    v.Nom_ville = Console.ReadLine();
                    FonctionsConsole.IdTrouve test = DataAccess.VerificationVille(v.Nom_ville);

                    if (!test.Trouve)
                    {
                        DataAccess.InsererVilleEnBDD(v.Code_postal_ville, v.Nom_ville);
                        p.Id_CpVille = DataAccess.VerificationVille(v.Nom_ville).Id;
                    }
                    else
                    {
                        p.Id_CpVille = test.Id;
                    }
                    break;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            #endregion

            #region Nom Client
            while (true)
            {
                try
                {
                    Console.Write("Nom: ");
                    p.Nom_Client = Console.ReadLine();
                    break;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            #endregion

            #region Prénom Client
            while (true)
            {
                try
                {
                    Console.Write("Prénom: ");
                    p.Prenom_client = Console.ReadLine();
                    p.Prenom_client = FonctionsConsole.premiereLettreMajuscule(p.Prenom_client);
                    break;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            #endregion

            #region Adresse Client
            while (true)
            {
                try
                {
                    Console.Write("Numéro de nom de rue du client: ");
                    p.Adresse_client = Console.ReadLine();
                    break;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            #endregion

            #region Chant Téléphone
            while (true)
            {
                try
                {
                    Console.Write("Téléphone: ");
                    bool telephoneEstValide = false;
                    p.Numtel_Client = Console.ReadLine();
                    telephoneEstValide = Int32.TryParse(p.Numtel_Client, out int _);
                    int telephonelongeur = p.Numtel_Client.Length;
                    break;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            #endregion

            #region Chant date de naissance
            while (true)
            {
                try
                {
                    Console.Write("Date de naissance: ");
                    p.Date_naissance_client = Convert.ToDateTime(Console.ReadLine());
                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            #endregion

            #region Chant Mot De Passe
            while (true)
            {
                try
                {
                    Console.WriteLine("Mot de passe:");
                    p.Mdp_client = FonctionsConsole.MaskPassword();
                    break;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            #endregion
        }
        #endregion

        //Vérifications de chants
        public static bool VerifieSiQueDesLettres(string mot)
        {
            bool leMotNaQueDesLettres = true;
            bool result = true;
            for (int i = 0; i < mot.Length; i++)
            {
                leMotNaQueDesLettres = char.IsLetter(mot[i]);
                if (leMotNaQueDesLettres == false)
                {
                    result = false;
                }
            }
            return result;
        }
        public static bool verifieSiQueDesChiffres(string valeur)
        {
            foreach (char c in valeur)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool VerifEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {

                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static void verifDateDeNaissance(DateTime date)
        {
            if (date.Year > DateTime.Today.Year)
            {
                throw new FormatException("L'année de naissance est invalide.");
            }
        }
        public static bool verifMotDePasse(string valeur)
        {
            while (true)
            {
                valeur = FonctionsConsole.MaskPassword();

                bool possedeUneLettre = false;
                bool possedeUnChiffre = false;
                bool possedeUneMajuscule = false;
                if (valeur.Length <= 7)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < valeur.Length; i++)
                    {
                        bool aUneLettre = char.IsLetter(valeur[i]);

                        if (aUneLettre == true)
                        {
                            possedeUneLettre = true;
                            break;
                        }
                    }
                    for (int i = 0; i < valeur.Length; i++)
                    {
                        bool aUnChiffre = char.IsDigit(valeur[i]);
                        if (aUnChiffre == true)
                        {
                            possedeUnChiffre = true;
                            break;
                        }
                    }
                    for (int i = 0; i < valeur.Length; i++)
                    {
                        bool aUneMaj = char.IsUpper(valeur[i]);
                        if (aUneMaj == true)
                        {
                            possedeUneMajuscule = true;
                            break;
                        }
                    }
                    if (possedeUnChiffre == true && possedeUneLettre == true && possedeUneMajuscule == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }
        public class MonMessageErreur : Exception
        {
            public string errorMessage;
            public MonMessageErreur(string message)
               : base(message)
            {
                errorMessage = message;
            }
        }
    }
}