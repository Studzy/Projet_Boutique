using System;
using System.Collections.Generic;

namespace BoutiqueBDDLibrary
{
    public class Fonctions
    {
        //-------------------------------------------------//
        //               [PARTIE QUENTIN]                  //
        //-------------------------------------------------//

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
                        string a  ="Merci, et à bientôt !\n\n";
                        string b ="                         ¶¶¶¶¶¶¶¶¶¶¶¶ ";
                        string c ="                         ¶¶            ¶¶ ";
                        string d ="           ¶¶¶¶¶        ¶¶                ¶¶ ";
                        string e ="           ¶     ¶     ¶¶      ¶¶    ¶¶     ¶¶ ";
                        string f ="            ¶     ¶    ¶¶       ¶¶    ¶¶      ¶¶ ";
                        string g ="             ¶    ¶   ¶¶        ¶¶    ¶¶      ¶¶ ";
                        string h ="              ¶   ¶   ¶                         ¶¶ ";
                        string i ="            ¶¶¶¶¶¶¶¶¶¶¶¶                         ¶¶ ";
                        string j ="           ¶            ¶    ¶¶            ¶¶    ¶¶ ";
                        string k ="          ¶¶            ¶    ¶¶            ¶¶    ¶¶ ";
                        string l ="         ¶¶   ¶¶¶¶¶¶¶¶¶¶¶      ¶¶        ¶¶     ¶¶ ";
                        string m ="          ¶               ¶       ¶¶¶¶¶¶¶       ¶¶ ";
                        string n ="         ¶¶              ¶                    ¶¶ ";
                        string o ="          ¶   ¶¶¶¶¶¶¶¶¶¶¶¶                   ¶¶ ";
                        string p ="          ¶¶           ¶  ¶¶                ¶¶ ";
                        string q ="          ¶¶¶¶¶¶¶¶¶¶¶¶    ¶¶            ¶¶";
                        string r = "                          ¶¶¶¶¶¶¶¶¶¶¶";

                        #region Affiche toutes les variables
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n");
                        Console.SetCursorPosition((Console.WindowWidth - a.Length) / 2, Console.CursorTop);
                        Console.WriteLine(a);
                        Console.SetCursorPosition((Console.WindowWidth - b.Length) / 2, Console.CursorTop);
                        Console.WriteLine(b);
                        Console.SetCursorPosition((Console.WindowWidth - c.Length) / 2, Console.CursorTop);
                        Console.WriteLine(c);
                        Console.SetCursorPosition((Console.WindowWidth - d.Length) / 2, Console.CursorTop);
                        Console.WriteLine(d);
                        Console.SetCursorPosition((Console.WindowWidth - e.Length) / 2, Console.CursorTop);
                        Console.WriteLine(e);
                        Console.SetCursorPosition((Console.WindowWidth - f.Length) / 2, Console.CursorTop);
                        Console.WriteLine(f);
                        Console.SetCursorPosition((Console.WindowWidth - g.Length) / 2, Console.CursorTop);
                        Console.WriteLine(g);
                        Console.SetCursorPosition((Console.WindowWidth - h.Length) / 2, Console.CursorTop);
                        Console.WriteLine(h);
                        Console.SetCursorPosition((Console.WindowWidth - i.Length) / 2, Console.CursorTop);
                        Console.WriteLine(i);
                        Console.SetCursorPosition((Console.WindowWidth - j.Length) / 2, Console.CursorTop);
                        Console.WriteLine(j);
                        Console.SetCursorPosition((Console.WindowWidth - k.Length) / 2, Console.CursorTop);
                        Console.WriteLine(k);
                        Console.SetCursorPosition((Console.WindowWidth - l.Length) / 2, Console.CursorTop);
                        Console.WriteLine(l);
                        Console.SetCursorPosition((Console.WindowWidth - m.Length) / 2, Console.CursorTop);
                        Console.WriteLine(m);
                        Console.SetCursorPosition((Console.WindowWidth - n.Length) / 2, Console.CursorTop);
                        Console.WriteLine(n);
                        Console.SetCursorPosition((Console.WindowWidth - o.Length) / 2, Console.CursorTop);
                        Console.WriteLine(o);
                        Console.SetCursorPosition((Console.WindowWidth - p.Length) / 2, Console.CursorTop);
                        Console.WriteLine(p);
                        Console.SetCursorPosition((Console.WindowWidth - q.Length) / 2, Console.CursorTop);
                        Console.WriteLine(q);
                        Console.SetCursorPosition((Console.WindowWidth - r.Length) / 2, Console.CursorTop);
                        Console.WriteLine(r);
                        #endregion

                        Console.ReadKey();
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
                    FonctionsConsole.AcheterProduit();
                    break;
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    FonctionsConsole.AfficherLesProduits();
                    Console.WriteLine("APPUYER SUR UNE TOUCHE POUR RETOURNER AU MENU CLIENT");
                    Console.ReadKey();
                    Console.Clear();
                    InterfaceClient();
                    break;
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    ModifierSonProfil();
                    Console.Clear();
                    InterfaceClient();
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
                            MenuPrincipal();
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
                     "\n\t - [1] CREER UN PRODUIT\t\t\t - [5] AFFICHER LES CLIENTS " +
                     "\n\t - [2] MODIFIER UN PRODUIT\t\t - [6] AFFICHER UN CLIENT " +
                     "\n\t - [3] AFFICHER LES PRODUITS\t\t - [7] MODIFIER UN CLIENT " +
                     "\n\t - [4] AFFICHER UN PRODUIT\t\t - [8] SUPPRIMER UN CLIENT" +
                     "\n\t - [x] RETOUR\n ");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    FonctionsConsole.AjouterProduit();
                    Console.WriteLine("APPUYER SUR UNE TOUCHE POUR RETOURNER AU MENU ADMIN");
                    Console.ReadKey();
                    Console.Clear();
                    InterfaceAdmin();
                    break;
                case ConsoleKey.NumPad2:
                    //ModifierUnProduit();
                    Console.Clear();
                    Fonctions.ModifierProduit();
                    Console.WriteLine("APPUYER SUR UNE TOUCHE POUR RETOURNER AU MENU ADMIN");
                    Console.ReadKey();
                    Console.Clear();
                    InterfaceAdmin();
                    break;
                case ConsoleKey.NumPad3:
                    //AfficherLesProduits();
                    Console.Clear();
                    FonctionsConsole.AfficherLesProduits();
                    Console.WriteLine("APPUYER SUR UNE TOUCHE POUR RETOURNER AU MENU ADMIN");
                    Console.ReadKey();
                    Console.Clear();
                    InterfaceAdmin();
                    break;
                case ConsoleKey.NumPad4:
                    //AfficherUnProduit();
                    Console.Clear();
                    AfficherUnProduit();
                    Console.WriteLine("APPUYER SUR UNE TOUCHE POUR RETOURNER AU MENU ADMIN");
                    Console.ReadKey();
                    Console.Clear();
                    InterfaceAdmin();
                    break;
                case ConsoleKey.NumPad5:
                    //Affiche tout les clients
                    Console.Clear();
                    afficheToutLesClients();
                    Console.WriteLine("APPUYER SUR UNE TOUCHE POUR RETOURNER AU MENU ADMIN");
                    Console.ReadKey();
                    Console.Clear();
                    InterfaceAdmin();
                    break;
                case ConsoleKey.NumPad6:
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
                case ConsoleKey.NumPad7:
                    //Modifie un client
                    Console.Clear();
                    ModifierUnClient();
                    Console.WriteLine("APPUYER SUR UNE TOUCHE POUR RETOURNER AU MENU ADMIN");
                    Console.ReadKey();
                    Console.Clear();
                    InterfaceAdmin();
                    break;
                case ConsoleKey.NumPad8:
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
                client = DataAccess.AfficheIDEmailClient(Mail);
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
            List<Client> toutclients = DataAccess.GetAllClients();
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


        //-------------------------------------------------//
        //               [PARTIE JEREMY]                   //
        //-------------------------------------------------//

        //Menu d'actions sur les produits
        #region [Interface] Afficher un produit
        /// <summary>
        /// Fonction qui affiche le produit en fonction du "string nom".
        /// </summary>
        public static void DisplayOneProduct(string nom)
        {
            Produit produit = DataAccess.GetOneProduct(nom);

            if (produit.Nom_Produit != "Rien")
            {
                Console.WriteLine("Nous avons trouvé votre produit : ");
                Console.WriteLine("ID = " + produit.Id_Produit + "\nNom = " + produit.Nom_Produit + "\nTVA = " + produit.TVA + "\nPrix = " + produit.Prix_Produit + "\nRemise = " + produit.Remise_Produit + "\nDescription = " + produit.Description_Produit + "\nValeur nutritionnelle = " + produit.Val_Nutrition_Produit + "\nFK_Id_Categorie = " + produit.FK_Id_Categorie + "\nFK_Id_Origine = " + produit.FK_Id_Origine + "\nFK_Id_Unite = " + produit.FK_Id_Unite + "\n\n");
            }
            else
            {
                Console.WriteLine("Nous n'avons pas trouvé votre produit.");
            }
        }
        #endregion

        #region [Interface] Afficher tout les produits
        /// <summary>
        /// Affiche tout les produits. Affiche le nombre de produits trouver ainsi que tout les détails lié à chaque produit.
        /// </summary>
        public static void DisplayProduct()
        {
            List<Produit> produits = DataAccess.GetAllProducts();
            Console.WriteLine("Nous avons {0} produit(s) :", produits.Count);
            foreach (var produit in produits)
            {
                Console.WriteLine("ID = " + produit.Id_Produit + "; Nom = " + produit.Nom_Produit + "; TVA = " + produit.TVA + "; Prix = " + produit.Prix_Produit + "; Remise = " + produit.Remise_Produit + "; Description = " + produit.Description_Produit + "; Valeur nutritionnelle = " + produit.Val_Nutrition_Produit + "; Categorie = " + produit.Nom_categorie + "; Origine = " + produit.Nom_origine + "; Unite = " + produit.Libelle_unite + "\n\n");
            }
        }
        #endregion

        #region [Interface] Affiche Limit produits
        /// <summary>
        /// Fonction qui affiche des produits en fonction d'un tri 'group' qui commence au produit 'start' et affiche une nombre 'limit'.
        /// </summary>
        public static void DisplayLimitProduct(int start, string group, int limit)
        {
            List<Produit> produits = DataAccess.GetLimitProducts(start, group, limit);
            foreach (var produit in produits)
            {
                Console.WriteLine(produit.Id_Produit + ".  Nom = " + produit.Nom_Produit + "; Categorie = " + produit.Nom_categorie + "; Origine = " + produit.Nom_origine + "; Prix = " + produit.Prix_Produit + "; Unite = " + produit.Libelle_unite + "; Description = " + produit.Description_Produit + "; Valeur nutritionnelle = " + produit.Val_Nutrition_Produit +"\n\n");
            }
        }
        #endregion

        #region [Interface] Modifier un produit
        /// <summary>
        /// Menu de modification d'un produit.
        /// </summary>
        public static void ModifierProduit()
        {
            string Nom = "";
            Produit p = new Produit();
            Console.WriteLine("MODE ADMIN");
            Console.WriteLine("MODIFIER UN PRODUIT DANS LA BASE DE DONNEES");
            Console.Write("Nom du produit à modifier : ");
            Nom = Console.ReadLine();
            Produit produit = DataAccess.GetOneProduct(Nom);
            if (produit.Nom_Produit != "Rien")
            {
                Console.WriteLine();
                FonctionsConsole.PaternProduit(p);
                DataAccess.ModifyOneProduct(Nom, p);
                Console.WriteLine("Votre produit a été modifier avec succès.");
            }
            else
            {
                Console.WriteLine("Votre produit n'existe pas dans la base de données.");
            }
        }
        #endregion

        #region [Interface] Afficher un produit
        /// <summary>
        /// Menu affichage d'un produit.
        /// </summary>
        public static void AfficherUnProduit()
        {
            Produit p = new Produit();
            Console.WriteLine("MODE ADMIN");
            Console.WriteLine("RECHERCHE D'UN PRODUIT DANS LA BASE DE DONNEES");
            Console.Write("Nom du produit : ");
            p.Nom_Produit = Console.ReadLine();
            //p.Nom_Produit = FonctionsConsole.premiereLettreMajuscule(p.Nom_Produit);
            DisplayOneProduct(p.Nom_Produit);
        }
        #endregion

        //Menu Panier
        #region [Interface] Affiche le menu acheter
        /// <summary>
        /// Permet d'afficher le menu acheter.
        /// </summary>
        public static void AfficherMenuAcheter()
        {
            Console.Clear();
            Console.WriteLine("UTILISER LES FLECHES DE DROITE ET DE GAUCHE POUR AFFICHER LES PRODUITS");
            Console.WriteLine("APPUYER SUR P POUR FAIRE VOTRE PANIER");
            Console.WriteLine("APPUYER SUR A POUR DEFINIR LE NOMBRE DE PRODUIT AFFICHER EN UNE PAGE");
            Console.WriteLine("APPUYER SUR T POUR TRIER LES PRODUITS");
            Console.WriteLine("APPUYER SUR Q POUR QUITTEZ\n");
            Console.WriteLine("Voici nos produit(s) : \n");
        }
        #endregion

        #region [Interface] Affiche le panier
        /// <summary>
        /// Affiche le panier, cette fonction prend une list en paramètre
        /// </summary>
        /// <param name="list"></param>
        public static void AffichePanier(List<Commande> list)
        {
            Console.WriteLine("Voici votre Panier : ");
            for (int i = 0; i < list.Count; i++)
            {
                Produit produitCommander = new Produit();
                produitCommander = DataAccess.GetOneProductById(list[i].FK_Id_Produit);
                Console.WriteLine((i + 1) + ". {0} : x {1} = {2}e", produitCommander.Nom_Produit, list[i].Qtite_Produit, (produitCommander.Prix_Produit * list[i].Qtite_Produit));
            }
        }
        #endregion

        #region Calcul Le nombre de page total
        /// <summary>
        /// Calcul le nombre de page maximum qu'on peut afficher
        /// </summary>
        /// <param name="taille"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static decimal CalculPageMax(decimal taille, int limit)
        {
            decimal calcul = (taille / limit);
            return calcul;
        }

        #endregion

        #region Calcul le prix du panier
        /// <summary>
        /// Calcul le prix du panier en fonction d'une list donnée en paramètre
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static decimal CalculPanier(List<Commande> list)
        {
            decimal resultat = 0;
            for (int i = 0; i < list.Count; i++)
            {
                Produit produitCommander = new Produit();
                produitCommander = DataAccess.GetOneProductById(list[i].FK_Id_Produit);
                resultat = resultat + (produitCommander.Prix_Produit * list[i].Qtite_Produit);
            }
            return resultat;
        }

        #endregion
    }
}