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

        //-------------------------------------------------//
        //               [PARTIE JEREMY]                   //
        //-------------------------------------------------//

        //Produits
        #region AfficherLesProduits
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
        public static void PaternProduit(string x, BoutiqueBDDLibrary.Produit p)
        {
            decimal NbTest;
            bool MesTests = false;
            while (!MesTests)
            {
                try
                {
                    Console.Write("Nom du produit : ");
                    p.Nom_Produit = Console.ReadLine();
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
                Console.Write("TVA du produit : ");
                try
                {
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
                    x = Console.ReadLine();
                    p.Val_Nutrition_Produit = Convert.ToInt32(x);
                    MesTests = true;
                }
                catch (FormatException)
                {

                    Console.WriteLine("Cette valeur n'est pas valide");
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
                    IdTrouve testCategorie = DataAccess.VerificationCategorie(categorie.Nom_categorie);

                    if (!testCategorie.Trouve)
                    {
                        DataAccess.AddCategorie(categorie);
                        p.FK_Id_Categorie = DataAccess.VerificationCategorie(categorie.Nom_categorie).Id;
                    }
                    else
                    {
                        p.FK_Id_Categorie = testCategorie.Id;
                    }
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

                    if (!testOrigine.Trouve)
                    {
                        DataAccess.AddOrigine(origine);
                        p.FK_Id_Origine = DataAccess.VerificationOrigine(origine.Nom_Origine).Id;
                    }
                    else
                    {
                        p.FK_Id_Origine = testOrigine.Id;
                    }
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
                    IdTrouve testUnite =DataAccess.VerificationUnite(unite.Libelle_unite);

                    if (!testUnite.Trouve)
                    {
                        DataAccess.AddUnite(unite);
                        p.FK_Id_Unite = DataAccess.VerificationUnite(unite.Libelle_unite).Id;
                    }
                    else
                    {
                        p.FK_Id_Unite = testUnite.Id;
                    }
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
            string x = "";
            Produit p = new Produit();
            PaternProduit(x, p);
            DataAccess.AddProduct(p);
            Console.WriteLine("Votre produit a bien été ajouté !");
        }
        #endregion

        #region Acheter un produits
        /// <summary>
        /// CODE A COMMENTER DEBROUILLE TOI JEREMY AHA :p
        /// </summary>
        public static void AcheterProduit()
        {
            Fonctions.AfficherMenuAcheter();
            List<Produit> Panier = new List<Produit>();
            List<Commande> ListeCommande = new List<Commande>();
            decimal PrixPanier = 0;
            bool MesTest = false;
            int IDClientActuel = Fonctions.UtilisateurActuelID; // <------------
            int start = 0;
            int limit = 10; // <---------------
            string TrierPar = "Prix_Produit";
            Fonctions.DisplayLimitProduct(start, TrierPar, limit); //<-------------
            List<Produit> AllProduits = new List<Produit>();
            AllProduits = DataAccess.GetAllProducts();
            decimal taille = AllProduits.Count;
            int pageActuel = 1;
            decimal calcul = (taille / limit);
            decimal pagesTotal = Math.Ceiling(calcul);
            Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);
            bool display = false;
            while (display == false)

            {

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.T: //<-------------
                        Console.Write("Trier par [1] Prix(Croissant), [2] Nom(Ordre Alphabetique) ou [3] Categorie(Ordre Alphabetique) : ");
                        String tri = Console.ReadLine();

                        if (tri == "1")
                        {
                            TrierPar = "Prix_Produit";
                        }
                        if (tri == "2")
                        {
                            TrierPar = "Nom_Produit";
                        }
                        if (tri == "3")
                        {
                            TrierPar = "Nom_Categorie";
                        }

                        Console.Clear();
                        //AfficherMenuAcheter();
                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);
                        Console.ReadKey();
                        break;
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

                        Console.Clear();
                        Fonctions.AfficherMenuAcheter();
                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);

                        break;
                    case ConsoleKey.Enter:
                        Fonctions.AfficherMenuAcheter();
                        start = 0;
                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);
                        AllProduits = DataAccess.GetAllProducts();
                        taille = AllProduits.Count;
                        pageActuel = 1;
                        calcul = taille / 5;
                        pagesTotal = Math.Ceiling(calcul);
                        Console.WriteLine(pagesTotal);
                        Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);
                        break;
                    case ConsoleKey.RightArrow:
                        Fonctions.AfficherMenuAcheter();

                        //Console.WriteLine(taille);
                        if (start < taille - limit)
                        {
                            start = start + limit;
                            pageActuel = pageActuel + 1;
                        }

                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);
                        Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);
                        //Console.WriteLine("Fleche de droite");
                        break;
                    case ConsoleKey.P: // PANIER
                        Console.Clear();
                        Console.WriteLine("Voici nos produit(s) : \n");
                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);
                        Console.WriteLine("\n Page {0} sur {1} \n\n", pageActuel, pagesTotal);

                        Console.WriteLine("Selectionnez vos produtis en choississant leur numero");
                        Console.WriteLine("Validez en appuyant sur V");
                        bool test = false;
                        while (test == false)
                        {
                            Commande commande = new Commande();
                            Produit produit = new Produit();
                            string message = Console.ReadLine();
                            if (message == "V" || message == "v")
                            {
                                test = true;
                            }
                            else
                            {
                                int numero = Convert.ToInt32(message);
                                produit = DataAccess.GetOneProductById(numero);
                                commande.FK_Id_Produit = numero;
                                Console.WriteLine("Combien ?");
                                message = Console.ReadLine();
                                numero = Convert.ToInt32(message);
                                PrixPanier = PrixPanier + (produit.Prix_Produit * numero);
                                commande.Qtite_Produit = numero;
                                //int numfacture = Facture.GetLastNumFacture(); // <------------
                                //commande.FK_Id_Facture = numfacture; // <------------
                                ListeCommande.Add(commande);
                                for (int i = 0; i < numero; i++)
                                {
                                    Panier.Add(produit);
                                }

                                Console.WriteLine("Votre produit a été ajouter au panier !");
                            }


                        }
                        Console.WriteLine("Voici votre Panier : ");
                        for (int i = 0; i < ListeCommande.Count; i++)
                        {
                            Produit produitCommander = new Produit();
                            produitCommander = DataAccess.GetOneProductById(ListeCommande[i].FK_Id_Produit);
                            Console.WriteLine("{0} : x {1}", produitCommander.Nom_Produit, ListeCommande[i].Qtite_Produit);
                        }
                        Console.WriteLine("Le prix de votre panier est de : " + PrixPanier + "e \n");
                        Console.WriteLine("Confirmez vos achats O/N");

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
                                int DernierIDFacture = DataAccess.GetLastIdFacture(); // <------------
                                for (int i = 0; i < ListeCommande.Count; i++) // <------------
                                {
                                    ListeCommande[i].FK_Id_Facture = DernierIDFacture; // <------------
                                    DataAccess.AddCommande(ListeCommande[i]); // <------------
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
                                    string ChoixUser = Console.ReadLine();
                                    int numeroPaiement = Convert.ToInt32(ChoixUser);
                                    ifp.FK_Id_Paiement = numeroPaiement;
                                    Console.Write("Il vous reste {0}e a payé, combien souhaitez vous regler ?", PrixPanier);
                                    ChoixUser = Console.ReadLine();
                                    decimal Payer = Convert.ToDecimal(ChoixUser);
                                    ifp.Montant_Paiement = Payer;
                                    PrixPanier = PrixPanier - Payer;
                                    ifp.FK_Id_Facture = DernierIDFacture;

                                    DataAccess.AddIFP(ifp);



                                }
                                display = true;

                                break;
                            case ConsoleKey.N:
                                Console.WriteLine("Appuyez sur entrée pour continuer");
                                break;

                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        Fonctions.AfficherMenuAcheter();

                        if (start >= limit)
                        {
                            start = start - limit;
                            pageActuel = pageActuel - 1;
                        }
                        Fonctions.DisplayLimitProduct(start, TrierPar, limit);
                        Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);




                        //Console.WriteLine("Fleche de gauche");
                        break;
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
                        break;

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
            if (NbDecimal < 0 )
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