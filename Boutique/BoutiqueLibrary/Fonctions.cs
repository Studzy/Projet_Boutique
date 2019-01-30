using System;
using System.Collections.Generic;

namespace BoutiqueBDDLibrary
{
    public class Fonctions
    {
        //Sauvegarde l'ID et l'Email du client connecter
        public static int UtilisateurActuelID;
        public static string UtilisateurActuelEmail = "";

        //Menus
        #region [Interface] Menu Principal
        /// <summary>
        /// Affiche le menu principal -> Se connecter | S'inscrire | Quitter le programme
        /// </summary>
        public static void MenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bonjour, veuillez vous identifiez ou vous inscrire:" +
                    "\n\t [1] - SE CONNECTER" +
                    "\n\t [2] - S'INSCRIRE" +
                    "\n\t [x] - QUITTER LE PROGRAMME");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        SeConnecter();
                        break;
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        Fonctions.Inscription();
                        break;
                    case ConsoleKey.Divide:
                        Console.Clear();
                        Fonctions.InscriptionAdministrateur();
                        break;
                    case ConsoleKey.X:
                        Console.Clear();
                        Console.WriteLine("Merci, et à bientôt !");
                        FonctionsJL.EndMenu();
                        return;
                }
            }
        }
        #endregion

        #region [Interface] Menu Client
        /// <summary>
        /// Affiche le menu Client -> Acheter un produit | Afficher les produits| Modifier les infos de son compte | Retour au menu principal
        /// </summary>
        public static void InterfaceClient()
        {
            Console.Clear();
            Console.Write("Vous êtes connecter avec l'email " + UtilisateurActuelEmail + "\n\n");
            Console.Write("[MODE CLIENT] Menu principal: \n" +
                     "\n\t - [1] ACHETER UN PRODUIT" +
                     "\n\t - [2] AFFICHER LES PRODUITS" +
                     "\n\t - [3] MODIFIER INFOS PERSONNELLE" +
                     "\n\t - [4] SUPPRIMER SON COMPTE" +
                     "\n\t - [x] RETOUR AU MENU PRINCIPAL\n");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    //AcheterProduit();
                    FonctionsJL.AcheterProduit();
                    break;
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    //AfficherLesProduit();
                    FonctionsJL.AfficherLesProduits();
                    break;
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    ModifierSonProfil();
                    break;
                case ConsoleKey.NumPad4:
                    Console.Clear();

                    FonctionsConsole.SupprimeClient(UtilisateurActuelEmail);
                    Console.WriteLine("Etes-vous sur de supprimer votre compte client ? (O/N)\n");
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.N:
                            Console.Clear();
                            InterfaceClient();
                            break;
                        case ConsoleKey.O:
                            Console.Clear();
                            Console.Write("Votre compte à été supprimer avec succès.");
                            Console.ReadKey();
                            MenuPrincipal();
                            break;
                    }
                    break;
                case ConsoleKey.X:
                    Console.Clear();
                    Console.WriteLine("vous allez être déconnecter de votre compte client, voulez-vous continuez ? (O/N)\n");
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.N:
                            Console.Clear();
                            InterfaceClient();
                            break;
                        case ConsoleKey.O:
                            Console.Clear();
                            return;
                    }
                    return;
            }
        }
        #endregion

        #region [Interface] Menu Administrateur
        /// <summary>
        /// Affiche le menu Administrateur -> 
        /// Crée un produit| Modifier un produit| Supprimer un produit | Afficher les produits | Afficher un produit |
        /// Afficher les clients | Afficher un client | Modifier un client | Supprimer un client | Déconnecxion
        /// </summary>
        public static void InterfaceAdmin()
        {
            Console.Clear();
            Console.Write("\t\t\t\t    [MENU ADMIN]\n" +
                     "\n\t           PRODUITS:\t\t\t            CLIENTS:\n" +
                     "\n\t - [1] CREER UN PRODUIT\t\t\t - [6] AFFICHER LES CLIENTS " +
                     "\n\t - [2] MODIFIER UN PRODUIT\t\t - [7] AFFICHER UN CLIENT " +
                     "\n\t - [3] SUPPRIMER UN PRODUIT\t\t - [8] MODIFIER UN CLIENT " +
                     "\n\t - [4] AFFICHER LES PRODUITS\t\t - [9] SUPPRIMER UN CLIENT" +
                     "\n\t - [5] AFFICHER UN PRODUIT\t\t " +
                     "\n\t - [x] RETOUR\n ");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                    FonctionsJL.AjouterProduit();
                    break;
                case ConsoleKey.NumPad2:
                    //ModifierUnProduit();
                    FonctionsJL.ModifierProduit();
                    break;
                case ConsoleKey.NumPad3:
                    //SupprimerUnProduit();
                    FonctionsJL.SupprimerProduit();
                    break;
                case ConsoleKey.NumPad4:
                    //AfficherLesProduits();
                    FonctionsJL.AfficherLesProduits();
                    break;
                case ConsoleKey.NumPad5:
                    //AfficherUnProduit();
                    FonctionsJL.AfficherUnProduit();
                    break;
                case ConsoleKey.NumPad6:
                    //Affiche tout les clients
                    Console.Clear();
                    afficheToutLesClients();
                    Console.WriteLine("APPUYER SUR UNE TOUCHE POUR RETOURNER AU MENU ADMIN");
                    Console.ReadKey();
                    Console.Clear();
                    InterfaceAdmin();
                    break;
                case ConsoleKey.NumPad7:
                    //Affiche un Client
                    Console.Clear();
                    Console.WriteLine("Renseigner l'email du client à afficher: ");
                    string email = Console.ReadLine();
                    afficheUnClient(email);
                    Console.WriteLine("APPUYER SUR UNE TOUCHE POUR RETOURNER AU MENU ADMIN");
                    Console.ReadKey();
                    Console.Clear();
                    InterfaceAdmin();
                    break;
                case ConsoleKey.NumPad8:
                    //Modifie un client
                    Console.Clear();
                    ModifierUnClient();
                    Console.WriteLine("APPUYER SUR UNE TOUCHE POUR RETOURNER AU MENU ADMIN");
                    Console.ReadKey();
                    Console.Clear();
                    InterfaceAdmin();
                    break;
                case ConsoleKey.NumPad9:
                    //Supprime un Client
                    Console.Clear();
                    Console.WriteLine("Renseigner l'email du client à supprimer (ATTENTION Factures et Commandes supprimer avec) : ");
                    string emailsuppr = Console.ReadLine();
                    FonctionsConsole.SupprimeClient(emailsuppr);
                    Console.Clear();
                    InterfaceAdmin();
                    break;

                case ConsoleKey.X:
                    Console.Clear();
                    Console.WriteLine("vous allez être déconnecter de votre compte client, voulez-vous continuez ? (O/N)\n");
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.N:
                            InterfaceClient();
                            break;
                        case ConsoleKey.O:
                            Console.Clear();
                            return;
                    }
                    return;
            }
        }
        #endregion

        //Inscriptions
        #region [Interface] Menu Inscription Clients
        /// <summary>
        /// Menu inscription du client
        /// </summary>
        public static void Inscription()
        {
            bool veriftest = false;
            Client x = new Client();
            CpVille y = new CpVille();
            Console.WriteLine("[INSCRIPTION CLIENT] Renseigner les champs suivant:");

            #region Chant Nom
            while (veriftest == false)
            {
                try
                {
                    Console.Write("Nom: ");
                    x.Nom_Client = Console.ReadLine();
                    veriftest = true;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            veriftest = false;
            #endregion

            #region Chant Prénom
            while (veriftest == false)
            {
                try
                {
                    Console.Write("Prénom: ");
                    x.Prenom_client = Console.ReadLine();
                    x.Prenom_client = FonctionsConsole.premiereLettreMajuscule(x.Prenom_client);
                    veriftest = true;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            veriftest = false;
            #endregion

            #region Chant Adresse
            while (veriftest == false)
            {
                try
                {
                    Console.Write("N° + Rue: ");
                    x.Adresse_client = Console.ReadLine();
                    veriftest = true;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            veriftest = false;
            #endregion

            #region Code Postal
            while (veriftest == false)
            {
                try
                {
                    Console.Write("Code postal: ");
                    y.Code_postal_ville = Console.ReadLine();
                    veriftest = true;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            veriftest = false;
            #endregion

            #region Chant Nom Ville
            while (veriftest == false)
            {
                try
                {
                    Console.Write("Ville: ");
                    y.Nom_ville = Console.ReadLine();
                    FonctionsConsole.IdTrouve test = DataAccess.VerificationVille(y.Nom_ville);

                    if (!test.Trouve)
                    {
                        DataAccess.InsererVilleEnBDD(y.Code_postal_ville, y.Nom_ville);
                        x.Id_CpVille = DataAccess.VerificationVille(y.Nom_ville).Id;
                    }
                    else
                    {
                        x.Id_CpVille = test.Id;
                    }
                    veriftest = true;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            veriftest = false;
            #endregion

            #region Chant Email
            while (veriftest == false)
            {
                try
                {
                    Console.Write("Email: ");
                    string value = Console.ReadLine();
                    FonctionsConsole.ValiderNouvelEmail(value);
                    x.Mail_client = value;
                    veriftest = true;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            veriftest = false;
            #endregion

            #region Chant Téléphone
            while (veriftest == false)
            {
                try
                {
                    Console.Write("Téléphone: ");
                    x.Numtel_Client = Console.ReadLine();
                    veriftest = true;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            veriftest = false;
            #endregion

            #region Chant date de naissance
            while (veriftest == false)
            {
                try
                {
                    Console.Write("Date de naissance: ");
                    x.Date_naissance_client = Convert.ToDateTime(Console.ReadLine());
                    veriftest = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            veriftest = false;
            #endregion

            #region Chant Mot De Passe
            while (veriftest == false)
            {
                try
                {
                    Console.Write("Mot de passe:");
                    x.Mdp_client = FonctionsConsole.MaskPassword();
                    Console.WriteLine();
                    veriftest = true;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            veriftest = false;
            #endregion

            DataAccess.InsererClientEnBDD(x);
        }
        #endregion

        #region [Interface] Menu Inscription Administrateur
        /// <summary>
        /// Menu inscription de l'administrateur
        /// </summary>
        public static void InscriptionAdministrateur()
        {
            Console.WriteLine("[INSCRIPTION ADMIN] Renseigner les champs suivant :");
            bool veriftest = false;
            Admin x = new Admin();

            //Gère le chant Email
            #region Chant Email
            while (veriftest == false)
            {
                try
                {
                    Console.Write("Email: ");
                    x.Mail_Admin = Console.ReadLine();
                    veriftest = true;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            veriftest = false;
            #endregion

            //Gère le mot de passe
            #region Chant Mot De Passe

            while (veriftest == false)
            {
                try
                {
                    Console.Write("Mot de passe:");
                    x.Mot_De_Passe = FonctionsConsole.MaskPassword();
                    Console.WriteLine();
                    veriftest = true;
                }
                catch (FonctionsConsole.MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            veriftest = false;
            #endregion

            DataAccess.InsererAdministrateurEnBDD(x);
        }
        #endregion

        //Connexions
        #region [Interface] Menu Des Connexions
        /// <summary>
        /// Affiche le menu de connexions -> Compte client | Compte admin| Retour menu principal
        /// </summary>
        public static void SeConnecter()
        {
            Console.Clear();
            Console.WriteLine("[SE CONNECTER] Choisir l'option voulu" +
                "\n\t [1] - COMPTE CLIENT " +
                "\n\t [2] - COMPTE ADMINISTRATEUR " +
                "\n\t [x] - RETOUR MENU PRINCIPAL");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                    ConnexionClient();
                    break;
                case ConsoleKey.NumPad2:
                    ConnexionAdministrateur();
                    break;
                case ConsoleKey.X:
                    Console.Clear();
                    return;
            }
        }
        #endregion

        #region [Interface] Menu Connexion Client
        /// <summary>
        /// Menu de connexion du client --> Vérification de l'émail et du mot de passe.
        /// </summary>
        public static void ConnexionClient()
        {
            Console.Clear();
            string Mail = "";
            string MotDePasse = "";
            Console.WriteLine("[CONNEXION CLIENT] Renseigner les champs suivant :");
            Console.Write("Email: ");
            Mail = Console.ReadLine();
            Console.Write("Mot de passe:");
            MotDePasse = FonctionsConsole.MaskPassword();
            Console.WriteLine();

            if (DataAccess.VerifieMailMdpClient(Mail, MotDePasse))
            {
                Console.Clear();
                Client client = new Client();
                client = DataAccess.afficheIDEmailClient(Mail);
                UtilisateurActuelEmail = Mail;
                UtilisateurActuelID = client.Id_client;
                InterfaceClient();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Email ou mot de passe incorrect !\n");
                return;
            }
        }
        #endregion

        #region  [Interface] Menu Connexion Administrateur
        /// <summary>
        /// Menu de connexion de l'admin --> Vérification de l'émail et du mot de passe.
        /// </summary>
        public static void ConnexionAdministrateur()
        {
            Console.Clear();
            string Mail = "";
            string MotDePasse = "";
            Console.WriteLine("[CONNEXION ADMINISTRATEUR] Renseigner les champs suivant :");
            Console.Write("Email: ");
            Mail = Console.ReadLine();
            Console.Write("Mot de passe:");
            MotDePasse = FonctionsConsole.MaskPassword();
            Console.WriteLine();

            if (DataAccess.VerifieMailMdpAdministrateur(Mail, MotDePasse))
            {
                Console.Clear();
                InterfaceAdmin();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Email ou mot de passe incorrect !\n");
                return;
            }
        }
        #endregion

        //Actions du menu Administrateur
        #region [Interface] Supprimer un client
        /// <summary>
        /// Menu de suppression d'un client -> Vérifie si l'email existe, si il existe supprime le client sinon renvoie un message d'erreur.
        /// </summary>
        public static void supprimeClientEnBDD(string email)
        {
            Client clientx = DataAccess.verifieSiClientExiste(email);
            if (clientx.Mail_client != "Rien")
            {
                DataAccess.supprimerUnClient(email);
            }
            else
            {
                Console.WriteLine("ERREUR: Cet email n'existe pas dans la base de données.\n");
            }
        }
        #endregion

        #region [Interface] Afficher un client
        /// <summary>
        /// Menu afficher un client -> Vérifie si l'email existe, si il existe affiche le client sinon renvoie un message d'erreur.
        /// </summary>
        public static void afficheUnClient(string email)

        {
            Client client = DataAccess.verifieSiClientExiste(email);
            if (client.Mail_client != "Rien")
            {
                Console.Clear();
                Console.WriteLine("Nous avons trouvé le client:\n\n ");
                Console.WriteLine("Nom: " + client.Nom_Client + "\nPrénom: " + client.Prenom_client + "\nAdresse: " + client.Adresse_client + "\nMail: " + client.Mail_client + "\nTéléphone: " + client.Numtel_Client + "\nDate de naissance: " + client.Date_naissance_client.ToString("d/M/yyyy") + "\nCode postal: " + client.codePostal_Ville + "\nNom ville: " + client.nom_Ville + "\n\n");
            }
            else
            {
                Console.WriteLine("ERREUR: Cet email appartient à aucun client.\n");
            }
        }
        #endregion

        #region [Interface] Affiche tout les clients
        /// <summary>
        /// Affiche tout les clients à l'écran et renvoi le nombre de client(s) trouvé.
        /// </summary>
        public static void afficheToutLesClients()
        {
            List<Client> toutclients = DataAccess.GetAllProducts();
            Console.Clear();
            Console.WriteLine("Nous avons trouvé {0} client(s) :\n", toutclients.Count);
            foreach (var toutclient in toutclients)
            {
                Console.WriteLine("| Nom: " + toutclient.Nom_Client + " | Prenom: " + toutclient.Prenom_client + " | Email: " + toutclient.Mail_client + " | Téléphone: " + toutclient.Numtel_Client + " | Date de naissance: " + toutclient.Date_naissance_client.ToString("d/M/yyyy") + " | Adresse: " + toutclient.Adresse_client + " | Code postal: " + toutclient.codePostal_Ville + " | Ville: " + toutclient.nom_Ville);
            }
            Console.WriteLine();
        }
        #endregion

        #region [Interface] Modifier un client
        /// <summary>
        /// Modifie un client -> Vérifie l'émail du client, si il valide continu et sinon renvoi un message d'erreur.
        /// </summary>
        public static void ModifierUnClient()
        {
            string email = "";
            string x = "";
            Client p = new Client();
            CpVille v = new CpVille();
            Console.Write("Email du client à modifier: ");
            email = Console.ReadLine();
            Console.WriteLine();
            Client client = DataAccess.verifieSiClientExiste(email);
            if (client.Mail_client != "Rien")
            {
                Console.WriteLine();
                FonctionsConsole.PaternClient(x, p, v);
                DataAccess.modifieUnClientenBDD(client.Mail_client, p);
                Console.WriteLine("\nLe profil client viens d'être mis à jours.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("ERREUR: Ce client n'existe pas dans la base de données.\n");
            }
        }
        #endregion

        //Actions du menu client
        #region [Interface] Modifier son profil personnel
        /// <summary>
        /// Modifie son profil personnel
        /// </summary>
        public static void ModifierSonProfil()
        {
            string x = "";
            Client p = new Client();
            CpVille v = new CpVille();
            FonctionsConsole.PaternClient(x, p, v);
            DataAccess.modifieUnClientenBDD(UtilisateurActuelEmail, p);
            Console.Write("\nVotre profil a été mis à jours.");
            Console.ReadKey();
        }
        #endregion
    }
}