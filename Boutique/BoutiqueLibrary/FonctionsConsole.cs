using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

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
        //-------------------------------------------------//
        //               [PARTIE QUENTIN]                  //
        //-------------------------------------------------//

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
        /// <summary>
        /// Supprime un client, si IDMultiple est "0" alors on Supprime directement le client sinon on va chercher les ID facture et commande pour les supprimer avant de supprimer le client afin de supprimer les liens.
        /// </summary>
        public static void SupprimeClient(string valeur)
        {
            List<int> IDMultiple = new List<int>();
            IDMultiple = DataAccess.recupeIdFacture(valeur);
            if (IDMultiple[0] == 0)
            {
                DataAccess.supprimerUnClient(valeur);
            }
            else
            {
                DataAccess.recupeIdFacture(valeur);
                DataAccess.supprimerUneCommande(IDMultiple[0]);
                DataAccess.supprimerInterFacturePaiement(IDMultiple[0]);
                DataAccess.supprimerUneFacture(IDMultiple[0]);
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

        //-------------------------------------------------//
        //               [PARTIE JEREMY]                   //
        //-------------------------------------------------//

        //Produits
        #region AfficherLesProduits
            /// <summary>
            /// Affiche les differents produits
            /// </summary>
        public static void AfficherLesProduits()
        {
            Console.Clear();
            Fonctions.DisplayProduct();
            Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
            }
        }
        #endregion

        #region Patern Produit
        /// <summary>
        /// Modèle qui permet de modifier un produit.
        /// </summary>
        public static void PaternProduit(Produit p)
        {
            string x = "";
            decimal NbTest;
            bool MesTests = false;
            while (!MesTests)
            {
                try
                {
                    Console.Write("Nom du produit : ");
                    p.Nom_Produit = Console.ReadLine();
                    p.Nom_Produit = premiereLettreMajuscule(p.Nom_Produit);
                    MesTests = true;
                }
                catch (MonMessageErreur error)
                {
                    Console.WriteLine(error.errorMessage);
                }
            }
            MesTests = false;

            while (!MesTests)
            {

                try
                {
                    Console.Write("TVA du produit : ");
                    x = Console.ReadLine();
                    x = x.Replace('.', ',');
                    NbTest = Convert.ToDecimal(x);
                    NbTest = (Math.Round(NbTest, 1));
                    p.TVA = NbTest;
                    MesTests = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("ERREUR: Cette valeur n'est pas decimal");
                }
            }
            MesTests = false;

            while (!MesTests)
            {
                try
                {

                    Console.Write("Prix du produit : ");
                    x = Console.ReadLine();
                    x = x.Replace('.', ',');
                    NbTest = Convert.ToDecimal(x);
                    NbTest = (Math.Round(NbTest, 3));
                    p.Prix_Produit = NbTest;
                    MesTests = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("ERREUR: Cette valeur n'est pas decimal");
                }
            }
            MesTests = false;
            while (!MesTests)
            {
                try
                {
                    Console.Write("Remise du produit : ");
                    x = Console.ReadLine();
                    x = x.Replace('.', ',');
                    if (x == "")
                    {
                        x = "0";
                    }
                    NbTest = Convert.ToDecimal(x);
                    NbTest = (Math.Round(NbTest, 1));
                    p.Remise_Produit = NbTest;
                    MesTests = true;
                }
                catch (FormatException)
                {

                    Console.WriteLine("ERREUR: Cette valeur n'est pas valide");
                }
            }

            MesTests = false;

            while (!MesTests)
            {
                try
                {
                    Console.Write("Description du produit : ");
                    p.Description_Produit = Console.ReadLine();
                    MesTests = true;
                }
                catch (MonMessageErreur e)
                {
                    Console.WriteLine(e.errorMessage);
                }
            }
            MesTests = false;

            while (!MesTests)
            {
                try
                {
                    Console.Write("Valeur nutritionnelle du produit : ");
                    p.Val_Nutrition_Produit = Convert.ToInt32(Console.ReadLine());
                    MesTests = true;
                }
                catch (FormatException)
                {

                    Console.WriteLine("Cette valeur n'est pas une valeur entière");
                }
            }
            MesTests = false;

            while (!MesTests)
            {
                try
                {
                    Console.Write("Categorie du produit : ");
                    Categorie categorie = new Categorie();
                    categorie.Nom_categorie = Console.ReadLine();
                    categorie.Nom_categorie = premiereLettreMajuscule(categorie.Nom_categorie);
                    IdTrouve testCategorie = DataAccess.VerificationCategorie(categorie.Nom_categorie);
                    MesTests = true;
                }
                catch (MonMessageErreur e)
                {
                    Console.WriteLine(e.errorMessage);
                }
            }
            MesTests = false;


            while (!MesTests)
            {
                try
                {
                    Console.Write("Origine du produit : ");
                    Origine origine = new Origine();
                    origine.Nom_Origine = Console.ReadLine();
                    IdTrouve testOrigine = DataAccess.VerificationOrigine(origine.Nom_Origine);
                    MesTests = true;
                }
                catch (MonMessageErreur e)
                {
                    Console.WriteLine(e.errorMessage);
                }
            }
            MesTests = false;
            while (!MesTests)
            {
                try
                {
                    Console.Write("Unite du produit : ");
                    Unite unite = new Unite();
                    unite.Libelle_unite = Console.ReadLine();
                    IdTrouve testUnite = DataAccess.VerificationUnite(unite.Libelle_unite);
                    MesTests = true;
                }
                catch (MonMessageErreur e)
                {
                    Console.WriteLine(e.errorMessage);
                }
            }
            MesTests = false;
        }
        #endregion

        #region Ajoute un produit
        /// <summary>
        /// Ajoute un produit 
        /// </summary>
        public static void AjouterProduit()
        {
            Console.Clear();
            Produit p = new Produit();
            PaternProduit(p);
            DataAccess.AddProduct(p);
            Console.WriteLine("Votre produit a bien été ajouté !");
        }
        #endregion


        #region Acheter un produits
        /// <summary>
        /// Interface du client pour faire ses achats
        /// </summary>
        public static void AcheterProduit()
        {
            #region Initialisation des variables principales
            Fonctions.AfficherMenuAcheter();
            List<Produit> Panier = new List<Produit>();
            List<Commande> ListeCommande = new List<Commande>();
            decimal PrixPanier = 0;
            bool MesTest = false;
            bool MesTest2 = false;
            bool MesTest3 = false;
            string message;
            int IDClientActuel = Fonctions.UtilisateurActuelID; // <------------
            int start = 0;
            int limit = 10; // <---------------
            string TrierPar = "Nom_Produit";
            Fonctions.DisplayLimitProduct(start, TrierPar, limit); //<-------------
            List<Produit> AllProduits = new List<Produit>();
            AllProduits = DataAccess.GetAllProducts();
            decimal taille = AllProduits.Count;
            int pageActuel = 1;
            decimal pagesTotal = Math.Ceiling(Fonctions.CalculPageMax(taille, limit));
            Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);
            bool display = false;
            #endregion


            while (display == false)

            {

                switch (Console.ReadKey(true).Key)
                {
                    #region Fonctionnalité : Trier les produits
                    case ConsoleKey.T: //<-------------
                        Console.Write("Trier par [1] Prix(Croissant), [2] Nom(Ordre Alphabetique) ou [3] Categorie(Ordre Alphabetique) : ");
                        
                        while (!MesTest)
                        {
                            String tri = Console.ReadLine();
                            if (tri == "1")
                            {
                                TrierPar = "Prix_Produit";
                                MesTest = false;
                                goto case ConsoleKey.Enter;
                            }
                            else if (tri == "2")
                            {
                                TrierPar = "Nom_Produit";
                                MesTest = false;
                                goto case ConsoleKey.Enter;
                            }
                            else if (tri == "3")
                            {
                                TrierPar = "Nom_Categorie";
                                MesTest = false;
                                goto case ConsoleKey.Enter;
                            }
                            else
                            {
                                Console.WriteLine("Choisir un nombre valable");
                            }
                        }
                        MesTest = false;
                        break;
                    #endregion

                    #region Fonctionnalité : Nombre de produit a affiché par pages
                    case ConsoleKey.A: //<-------------

                        while (!MesTest)
                        {
                            try
                            {
                                Console.Write("Combien de produit souhaitez vous afficher par pages ? ");//<-------------
                                limit = Convert.ToInt32(Console.ReadLine());
                                MesTest = true;
                            }
                            catch (FormatException e)
                            {

                                Console.WriteLine(e.Message);

                            }
                        }

                        MesTest = false;
                        pagesTotal = Math.Ceiling(Fonctions.CalculPageMax(taille, limit));
                        Console.Clear();
                        Fonctions.AfficherMenuAcheter();
                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);
                        Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);
                        break;
                    #endregion

                    #region Fonctionnalité : Quittez le programme
                    case ConsoleKey.Q:
                        return;
                    #endregion

                    #region Fonctionnalité : Reafficher les produits
                    case ConsoleKey.Enter:
                        Fonctions.AfficherMenuAcheter();
                        start = 0;
                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);
                        AllProduits = DataAccess.GetAllProducts();
                        taille = AllProduits.Count;
                        pageActuel = 1;
                        pagesTotal = Math.Ceiling(Fonctions.CalculPageMax(taille, limit));
                        Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);
                        break;
                    #endregion

                    #region Fonctionnalité : Parcourir les differentes pages avec la fleche de droite
                    case ConsoleKey.RightArrow:
                        Fonctions.AfficherMenuAcheter();
                        if (start < taille - limit)
                        {
                            start = start + limit;
                            pageActuel = pageActuel + 1;
                        }

                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);
                        Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);
                        break;
                    #endregion

                    #region Fonctionnalité : Le panier
                    case ConsoleKey.P: // PANIER
                        #region Creation du panier
                        Console.Clear();
                        Console.WriteLine("Voici nos produit(s) : \n");
                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);
                        Console.WriteLine("\n Page {0} sur {1} \n\n", pageActuel, pagesTotal);

                        Console.WriteLine("Selectionnez vos produits en choississant leur numero");
                        Console.WriteLine("Validez en appuyant sur V");
                        Console.WriteLine("Quittez en appuyant sur Q");
                        while (!MesTest)
                        {

                            Commande commande = new Commande();
                            Produit produit = new Produit();
                            message = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(message))
                            {
                                Console.WriteLine("Veuillez renseigner un champs");
                            }
                            else if (message == "Q" || message == "q")
                            {
                                return;

                            }
                            else if (message == "V" || message == "v")
                            {
                                MesTest = true;
                            }
                            else if (verifieSiQueDesChiffres(message))
                            {
                                int numero = 0;
                                while (!MesTest2)
                                {
                                    try
                                    {
                                        numero = Convert.ToInt32(message);
                                        MesTest2 = true;

                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("La valeur n'est pas une valeur entiere");
                                    }
                                }
                                MesTest2 = false;


                                produit = DataAccess.GetOneProductById(numero);
                                if (produit.Nom_Produit == "Rien")
                                {
                                    Console.WriteLine("Votre choix ne fait pas parti de nos produits");
                                }
                                else
                                {
                                    commande.FK_Id_Produit = numero;
                                    Console.WriteLine("Combien ?");
                                    numero = 0;
                                    while (!MesTest2)
                                    {
                                        try
                                        {
                                            numero = Convert.ToInt32(Console.ReadLine());
                                            MesTest2 = true;
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("La valeur n'est pas une valeur entiere");
                                        }
                                    }
                                    MesTest2 = false;

                                    PrixPanier = PrixPanier + (produit.Prix_Produit * numero);
                                    commande.Qtite_Produit = numero;
                                    ListeCommande.Add(commande);
                                    for (int i = 0; i < numero; i++)
                                    {
                                        Panier.Add(produit);
                                    }

                                    Console.WriteLine("Votre produit a été ajouter au panier !\n");
                                }

                            }
                            else
                            {
                                Console.WriteLine("Veuillez renseigner un champs valide");
                            }


                        }
                        MesTest = false;
                        #endregion

                        #region Validation du panier
                        Fonctions.AffichePanier(ListeCommande);
                        Console.WriteLine("Le prix de votre panier est de : " + PrixPanier + "e \n");
                        Console.WriteLine("Supprimer des éléments du panier [S]");
                        Console.WriteLine("Confirmez vos achats [O/N]");

                        
                        switch (Console.ReadKey(true).Key)
                        {
                            
                            case ConsoleKey.O:
                                Facture facture = new Facture();
                                OptionPaiement paiement = new OptionPaiement();
                                IFP ifp = new IFP();
                                List<OptionPaiement> optionPaiements = new List<OptionPaiement>();
                                int numfacture = DataAccess.GetLastNumFacture();
                                numfacture = numfacture + 1;
                                //int IdClient = 3;
                                DateTime DateDuJour = DateTime.Today;
                                facture.Num_facture = numfacture;
                                facture.Date_facture = DateDuJour;
                                facture.Montant_total = PrixPanier;
                                facture.Fk_Id_Client = IDClientActuel; // <------------
                                DataAccess.AddFacture(facture);
                                int DernierIDFacture = DataAccess.GetLastIdFacture();
                                for (int i = 0; i < ListeCommande.Count; i++)
                                {
                                    ListeCommande[i].FK_Id_Facture = DernierIDFacture;
                                    DataAccess.AddCommande(ListeCommande[i]);
                                }

                                while (PrixPanier != 0)
                                {
                                    optionPaiements = DataAccess.GetAllPayement();
                                    Console.WriteLine("Voici nos differents moyens de paiements : ");

                                    for (int i = 0; i < optionPaiements.Count; i++)
                                    {
                                        Console.WriteLine("\n\t - {0} {1} \n", optionPaiements[i].Id_Paiement, optionPaiements[i].Libelle_paiement);
                                    }
                                    Console.Write("Choisissez votre moyens de paiements en indiquant le numero correspondants : ");
                                    //string ChoixUser = Console.ReadLine();
                                    int numeroPaiement = 0;
                                    while (!MesTest2)
                                    {
                                        try
                                        {
                                            numeroPaiement = Convert.ToInt32(Console.ReadLine());
                                            if (numeroPaiement <= 0 || numeroPaiement > 3)
                                            {
                                                Console.WriteLine("Veuillez choisir un moyen de paiement valide");
                                            }
                                            else
                                            {
                                                MesTest2 = true;
                                            }

                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("La valeur n'est pas une valeur entiere");
                                        }
                                    }
                                    MesTest2 = false;
                                    ifp.FK_Id_Paiement = numeroPaiement;
                                    Console.Write("Il vous reste {0}e a payé, combien souhaitez vous regler ?", PrixPanier);
                                    decimal Payer = 0;
                                    while (!MesTest2)
                                    {
                                        try
                                        {
                                            Payer = Convert.ToDecimal(Console.ReadLine());
                                            MesTest2 = true;
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Cette valeur n'est pas decimal");
                                        }
                                    }
                                    MesTest2 = false;

                                    ifp.Montant_Paiement = Payer;
                                    PrixPanier = PrixPanier - Payer;
                                    ifp.FK_Id_Facture = DernierIDFacture;

                                    DataAccess.AddIFP(ifp);


                                }
                                display = true;

                                break;
                            case ConsoleKey.N:
                                break;
                            case ConsoleKey.S:

                                Console.WriteLine("Tappez le numero correspondant au produit dans votre panier pour le supprimer");
                                Console.WriteLine("Confirmez vos suppression [V]\n");
                                while (!MesTest)
                                {
                                    int ProduitASuppr = 0;
                                    message = Console.ReadLine();
                                    if (string.IsNullOrWhiteSpace(message))
                                    {
                                        Console.WriteLine("Veuillez renseigner un champs");
                                    }
                                    else if (message == "V" || message == "v")
                                    {
                                        MesTest = true;
                                    }
                                    else if (verifieSiQueDesChiffres(message))
                                    {

                                        ProduitASuppr = Convert.ToInt32(message);
                                        if (ProduitASuppr <= 0 || ProduitASuppr > ListeCommande.Count)
                                        {
                                            Console.WriteLine("Veuillez renseigner un nombre valide");
                                        }
                                        else
                                        {
                                            ListeCommande.RemoveAt(ProduitASuppr - 1);
                                            Fonctions.AffichePanier(ListeCommande);
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Veuillez renseigner un champs valide");
                                    }

                                }
                                MesTest = false;
                                Fonctions.AffichePanier(ListeCommande);
                                PrixPanier = Fonctions.CalculPanier(ListeCommande);
                                Console.WriteLine("Le prix de votre panier est de : " + PrixPanier + "e \n");
                                Console.WriteLine("Confirmez vos achats [O/N]");
                                while (!MesTest)
                                {
                                    message = Console.ReadLine();
                                    if (message == "o" || message == "O")
                                    {
                                        MesTest = true;
                                        goto case ConsoleKey.O;
                                    }
                                    else if (message == "n" || message == "N")
                                    {
                                        MesTest = true;
                                        goto case ConsoleKey.N;
                                    }
                                    else
                                    {
                                        MesTest = false;
                                        Console.WriteLine("Veuillez renseigner un champs valide");
                                    }
                                }
                                MesTest = false;
                                break;

                        }
                        goto case ConsoleKey.Enter;
                    #endregion

                    #endregion

                    #region Fonctionnalité : Parcourir les differentes pages avec la fleche de gauche
                    case ConsoleKey.LeftArrow:
                        Fonctions.AfficherMenuAcheter();

                        if (start >= limit)
                        {
                            start = start - limit;
                            pageActuel = pageActuel - 1;
                        }
                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);
                        Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);

                        break;
                    #endregion

                    #region Fonctionnalité : Validation achats
                    case ConsoleKey.V:
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.O:
                                Facture facture = new Facture();
                                //Commande commande = new Commande();
                                for (int i = 0; i < ListeCommande.Count; i++)
                                {
                                    DataAccess.AddCommande(ListeCommande[i]);
                                }
                                int numfacture = DataAccess.GetLastNumFacture();
                                numfacture = numfacture + 1;
                                int IdClient = 3;
                                DateTime DateDuJour = DateTime.Today;
                                facture.Num_facture = numfacture;
                                facture.Date_facture = DateDuJour;
                                facture.Montant_total = PrixPanier;
                                facture.Fk_Id_Client = IdClient;
                                DataAccess.AddFacture(facture);

                                display = true;

                                break;
                            case ConsoleKey.N:
                                Console.WriteLine("Appuyez sur entrée pour continuer");
                                break;

                        }
                        goto case ConsoleKey.Enter;
                        //break;
                        #endregion

                }

            }
        }
        #endregion

        //-------------------------------------------------//
        //              [PARTIE COMMUNE]                   //
        //-------------------------------------------------//

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

        public static void verifDecimal(decimal NbDecimal)
        {
            if (NbDecimal < 0)
            {
                throw new FormatException("Cette valeur n'est pas valide");
            }
        }
        public static void verifInt(int NbInt)
        {
            if (NbInt < 0)
            {
                throw new FormatException("Cette valeur n'est pas valide");
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