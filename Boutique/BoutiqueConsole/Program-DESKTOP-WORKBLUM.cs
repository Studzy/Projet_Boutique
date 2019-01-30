using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessBoutique;
using BoutiqueLibrary;

namespace BoutiqueConsole
{
    class Program
    {
        //Affiche le menu Principal
        #region Menu Principal
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Bonjour, veuillez vous identifiez ou vous inscrire:" +
                    "\n\t [1] - Se connecter" +
                    "\n\t [2] - S'inscrire" +
                    "\n\t [3] - Quitter");

                string choixEffectue = Console.ReadLine();
                Console.Clear();
                switch (choixEffectue)
                {
                    case "1":
                        SeConnecter();
                        break;
                    case "2":
                        Inscription();
                        break;
                    case "3":
                        Console.WriteLine("Merci, et à bientôt !");
                        Console.ReadKey();
                        return;
                }
            }
        }
        #endregion

        //Affiche le menu d'inscription 
        #region Inscription Clients
        private static void Inscription()
        {
            Client x = new Client();
            Console.WriteLine("[INSCRIPTION] Renseigner les champs suivant :");
            CpVille y = new CpVille();

            #region Chant Nom
            //Gère le chant Nom
            Console.Write("Nom: ");
            x.Nom_Client = Console.ReadLine();

            while (x.Nom_Client == "")
            {
                Console.Write("Renseigner un nom valide: ");
                x.Nom_Client = Console.ReadLine();
            }
            #endregion

            #region Chant Prénom
            //Gère le prénom
            Console.Write("Prénom: ");
            x.Prenom_client = Console.ReadLine();
            while (x.Prenom_client == "")
            {
                Console.Write("Renseigner un prenom valide: ");
                x.Prenom_client = Console.ReadLine();
            }
            #endregion

            #region Chant Adresse
            //Gère l'adresse
            Console.Write("Adresse: ");
            x.Adresse_client = Console.ReadLine();
            while (x.Adresse_client == "")
            {
                Console.Write("Renseigner une adresse valide: ");
                x.Adresse_client = Console.ReadLine();
            }
            #endregion

            #region Code Postal
            Console.Write("Code Postal: ");
            y.Code_postal_ville = Console.ReadLine();
            #endregion

            #region Chant Nom Ville
            Console.Write("Ville: ");
            y.Nom_ville = Console.ReadLine();
            DataAccess.IdTrouve test = DataAccess.VerificationVille(y.Nom_ville);
            //Console.WriteLine(test);
            if (!test.Trouve)
            {
                DataAccess.InsererVilleEnBDD(y.Code_postal_ville, y.Nom_ville);
                x.Id_CpVille = DataAccess.VerificationVille(y.Nom_ville).Id;
            }
            else
            {
                x.Id_CpVille = test.Id;
            }
            #endregion

            #region Chant Email
            //Gère le chant EMAIL
            Console.Write("Email: ");
            bool Email = false;
            while (Email == false)
            {

                x.Mail_client = Console.ReadLine();

                foreach (char c in x.Mail_client)
                {
                    if (c == '@')
                    {
                        Email = true;
                    }
                }
                if (Email == false)
                {
                    Console.Write("Veuillez entrez un Mail correct : ");
                }
            }
            #endregion

            #region Chant Téléphone
            //Gère le téléphone
            Console.Write("Téléphone: ");
            x.Numtel_Client = Console.ReadLine();
            while (x.Numtel_Client == "")
            {
                Console.Write("Renseigner un téléphone valide: ");
                x.Numtel_Client = Console.ReadLine();
            }
            #endregion

            #region Chant Date de naissance

            Console.Write("Date de naissance (AAAA,MM,JJ): ");
            string message = "";
            bool datenaissance = false;
            while (datenaissance == false)
            {
                message = Console.ReadLine();

                foreach (char c in message)
                {
                    if (c == ',')
                    {
                        datenaissance = true;
                    }
                }
                if (datenaissance == false)
                {
                    Console.Write("Veuillez entrez une date de naissance valide: ");
                }
            }
            x.Date_naissance_client = Convert.ToDateTime(message);

            #endregion

            #region Chant Mot De Passe
            //Gère le mot de passe
            Console.Write("Mot de passe: ");
            x.Mdp_client = Console.ReadLine();
            while (x.Mdp_client == "")
            {
                Console.Write("Renseigner un mot de passe valide: ");
                x.Mdp_client = Console.ReadLine();
            }
            #endregion

            DataAccess.InsererClientEnBDD(x.Nom_Client, x.Prenom_client, x.Adresse_client, x.Mail_client, x.Numtel_Client, x.Date_naissance_client, x.Mdp_client, x.Id_CpVille);
        }
        #endregion

        //Affiche le menu des connexions
        #region Menu de connexion
        private static void SeConnecter()
        {
            Console.WriteLine("------[CONNEXION]------" +
                "\n\t [1] - Compte Client " +
                "\n\t [2] - Compte Administateur " +
                "\n\t [3] - Retourner au menu principal");

            string choixEffectue = Console.ReadLine();
            Console.Clear();
            switch (choixEffectue)
            {
                case "1":
                    ConnexionClient();
                    break;
                case "2":
                    ConnexionAdministrateur();
                    break;
                case "3":
                    Console.WriteLine("Merci, et à bientôt !");
                    Console.ReadKey();
                    return;
            }
        }
        #endregion

        //Affiche le menu de connexion client
        #region Connexion Client
        private static void ConnexionClient()
        {
            Client x = new Client();
            Console.WriteLine("[CONNEXION CLIENT] Renseigner les champs suivant :");
            Console.Write("Email: ");
            x.Mail_client = Console.ReadLine();
            Console.Write("Mot de passe: ");
            x.Mdp_client = Console.ReadLine();

            DataAccess.VerifieMailMdpClient(x.Mail_client, x.Mdp_client);
        }
        #endregion

        //Affiche le menu de connexion administrateur
        #region Connexion Administrateur
        private static void ConnexionAdministrateur()
        {
            Admin x = new Admin();
            Console.WriteLine("[CONNEXION ADMINISTRATEUR] Renseigner les champs suivant :");
            Console.Write("Email: ");
            x.Mail_Admin = Console.ReadLine();
            Console.Write("Mot de passe: ");
            x.Mot_De_Passe = Console.ReadLine();

            DataAccess.VerifieMailMdpAdministrateur(x.Mail_Admin, x.Mot_De_Passe);
        }
        #endregion

        //Affiche le menu Administrateur
        #region Menu Administrateur
        public static void MenuAdministrateur()
        {
            while (true)
            {
                Console.WriteLine("[ADMINISTRATEUR] - Choisissez ce que vous souhaitez faire:" +
                    "\n\t [0] - Afficher les produits" +
                    "\n\t [1] - Ajouter un produit" +
                    "\n\t [2] - Modifier un produit" +
                    "\n\t [3] - Supprimer un produit" +
                    "\n\t [4] - Crée un nouveau compte admin" +
                    "\n\t [5] - Modifier un compte client" +
                    "\n\t [6] - Supprimer un compte client" +
                    "\n\t [7] - Retour au menu principal");

                string choixEffectue = Console.ReadLine();
                Console.Clear();
                switch (choixEffectue)
                {
                    case "0":
                        //AfficherProduit();
                        break;
                    case "1":
                        //AjouterProduit();
                        break;
                    case "2":
                        //ModifierProduit();
                        break;
                    case "3":
                        //SupprimerProduit();
                        break;
                    case "4":
                        //AjouterCompteAdmin();
                        break;
                    case "5":
                        //ModifierCompteClient();
                        break;
                    case "6":
                        //SupprimerCompteClient();
                        break;
                    case "7":
                        //Retour menu principal
                        Console.ReadKey();
                        return;
                }
            }
        }
        #endregion

        //Affiche le menu client
        #region Menu Client
        static void MenuClient()
        {
            while (true)
            {
                Console.WriteLine("[CLIENT] - Choisissez ce que vous souhaitez faire:" +
                    "\n\t [1] - Afficher les produits" +
                    "\n\t [2] - Modifier mes informations" +
                    "\n\t [3] - Revenir au menu principal");

                string choixEffectue = Console.ReadLine();
                Console.Clear();
                switch (choixEffectue)
                {
                    case "1":
                        //AfficherProduit();
                        break;
                    case "2":
                        //ModifierMesInfos();
                        break;
                    case "3":
                        Console.ReadKey();
                        return;
                }
            }
        }
        #endregion
    }
}