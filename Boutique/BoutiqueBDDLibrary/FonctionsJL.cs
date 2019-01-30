using System;
using System.Collections.Generic;
using System.Text;
using BoutiqueBDDLibrary;
using MySql.Data.MySqlClient;


namespace BoutiqueBDDLibrary
{

    public static class FonctionsJL
    {
        #region DisplayOneProduct
        public static void DisplayOneProduct(string nom)
        {
            BoutiqueBDDLibrary.Produit produit = BoutiqueBDDLibrary.Produit.GetOneProduct(nom);

            if (produit.Nom_Produit != "Rien")
            {
                Console.WriteLine("Nous avons trouvé votre produit : ");

                Console.WriteLine("ID = " + produit.Id_Produit + "; Nom = " + produit.Nom_Produit + "; TVA = " + produit.TVA + "; Prix = " + produit.Prix_Produit + "; Remise = " + produit.Remise_Produit + "; Description = " + produit.Description_Produit + "; Valeur nutritionnelle = " + produit.Val_Nutrition_Produit + "; FK_Id_Categorie = " + produit.FK_Id_Categorie + "; FK_Id_Origine = " + produit.FK_Id_Origine + "; FK_Id_Unite = " + produit.FK_Id_Unite);

                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Nous n'avons pas trouvé votre produit.");
            }

        }
        #endregion

        #region DisplayProduct
        public static void DisplayProduct()
        {
            List<Produit> produits = Produit.GetAllProducts();
            Console.WriteLine("Nous avons trouvé {0} produit(s) :", produits.Count);
            foreach (var produit in produits)
            {
                Console.WriteLine("ID = " + produit.Id_Produit + "; Nom = " + produit.Nom_Produit + "; TVA = " + produit.TVA + "; Prix = " + produit.Prix_Produit + "; Remise = " + produit.Remise_Produit + "; Description = " + produit.Description_Produit + "; Valeur nutritionnelle = " + produit.Val_Nutrition_Produit + "; FK_Id_Categorie = " + produit.FK_Id_Categorie + "; FK_Id_Origine = " + produit.FK_Id_Origine + "; FK_Id_Unite = " + produit.FK_Id_Unite);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        #endregion

        
        #region Display10Product
        public static void Display10Product(int start, string group)
        {
            List<Produit> produits = Produit.Get10Products(start, group);
            //Console.WriteLine("Voici nos produit(s) : Page : "+ page +" sur " + , produits.Count);
            foreach (var produit in produits)
            {
                Console.WriteLine(produit.Id_Produit +".  Nom = " + produit.Nom_Produit + "; Categorie = " + produit.Nom_categorie + "; Origine = " + produit.Nom_origine + "; Prix = " + produit.Prix_Produit +"; Unite = "+ produit.Libelle_unite + "; Description = " + produit.Description_Produit + "; Valeur nutritionnelle = " + produit.Val_Nutrition_Produit);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        #endregion

        #region Supprimer Produit
        public static void SupprimerProduit()
        {
            BoutiqueBDDLibrary.Produit p = new BoutiqueBDDLibrary.Produit();
            Console.WriteLine("MODE ADMIN");
            Console.WriteLine("SUPPRIMER UN PRODUIT DANS LA BASE DE DONNEES");

            Console.Write("Nom du produit à supprimer : ");
            p.Nom_Produit = Console.ReadLine();
            Console.WriteLine();

            BoutiqueBDDLibrary.Produit produit = BoutiqueBDDLibrary.Produit.GetOneProduct(p.Nom_Produit);
            if (produit.Nom_Produit != "Rien")
            {
                BoutiqueBDDLibrary.Produit.DeleteOneProduct(p.Nom_Produit);
            }
            else
            {
                Console.WriteLine("Votre produit n'existe pas dans la base de données.");
            }

        }
        #endregion



        #region Modifier Produit
        public static void ModifierProduit()
        {
            string Nom = "";
            string x = "";
            BoutiqueBDDLibrary.Produit p = new BoutiqueBDDLibrary.Produit();
            Console.WriteLine("MODE ADMIN");
            Console.WriteLine("MODIFIER UN PRODUIT DANS LA BASE DE DONNEES");

            Console.Write("Nom du produit à modifier : ");
            Nom = Console.ReadLine();
            BoutiqueBDDLibrary.Produit produit = BoutiqueBDDLibrary.Produit.GetOneProduct(Nom);
            if (produit.Nom_Produit != "Rien")
            {
                Console.WriteLine();
                PaternProduit(x, p);

                BoutiqueBDDLibrary.Produit.ModifyOneProduct(Nom, p);
                Console.WriteLine("Votre produit a été modifier avec succès.");
            }
            else
            {
                Console.WriteLine("Votre produit n'existe pas dans la base de données.");
            }


        }
        #endregion

        #region Afficher un produit
        public static void AfficherUnProduit()
        {
            BoutiqueBDDLibrary.Produit p = new BoutiqueBDDLibrary.Produit();
            Console.WriteLine("MODE ADMIN");
            Console.WriteLine("RECHERCHE D'UN PRODUIT DANS LA BASE DE DONNEES");

            Console.Write("Nom du produit : ");
            p.Nom_Produit = Console.ReadLine();

            FonctionsJL.DisplayOneProduct(p.Nom_Produit);

        }

        #endregion

        #region AfficherLesProduits
        public static void AfficherLesProduits()
        {
            Console.Clear();
            FonctionsJL.DisplayProduct();
            Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
            }
        }
        #endregion

        #region Patern Produit
        public static void PaternProduit(string x, BoutiqueBDDLibrary.Produit p)
        {
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
                x = Console.ReadLine();
                try
                {
                    p.TVA = Convert.ToDecimal(x);
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
                    
                    Console.Write("Prix du produit : ");
                    x = Console.ReadLine();
                    p.Prix_Produit = Convert.ToDecimal(x);
                    MesTests = true;
                }
                catch (MonMessageErreur e)
                {
                    Console.WriteLine(e.errorMessage);
                }

            }
            

            Console.Write("Remise du produit : ");
            x = Console.ReadLine();
            if (x == "")
            {
                p.Remise_Produit = 0;
            }
            else
            {
                p.Remise_Produit = Convert.ToDecimal(x);
            }

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
                    Console.Write("Categorie du produit : ");
                    Categorie categorie = new Categorie();
                    categorie.Nom_categorie = Console.ReadLine();
                    IdTrouve testCategorie = Categorie.VerificationCategorie(categorie.Nom_categorie);

                    if (!testCategorie.Trouve)
                    {
                        Categorie.AddCategorie(categorie);
                        p.FK_Id_Categorie = Categorie.VerificationCategorie(categorie.Nom_categorie).Id;
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
                    IdTrouve testOrigine = Origine.VerificationOrigine(origine.Nom_Origine);

                    if (!testOrigine.Trouve)
                    {
                        Origine.AddOrigine(origine);
                        p.FK_Id_Origine = Origine.VerificationOrigine(origine.Nom_Origine).Id;
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
                    IdTrouve testUnite = Unite.VerificationUnite(unite.Libelle_unite);

                    if (!testUnite.Trouve)
                    {
                        Unite.AddUnite(unite);
                        p.FK_Id_Unite = Unite.VerificationUnite(unite.Libelle_unite).Id;
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

        #region AJOUTER PRODUIT
        public static void AjouterProduit()
        {
            Console.Clear();
            string x = "";
            BoutiqueBDDLibrary.Produit p = new BoutiqueBDDLibrary.Produit();
            PaternProduit(x, p);

            BoutiqueBDDLibrary.Produit.AddProduct(p);
            Console.WriteLine("Votre produit a bien été ajouté !");

            #region COMMENTAIRES
            //CpVille.IdTrouve test = CpVille.VerificationVille(y.Nom_ville);
            //Console.WriteLine(test);
            //if (!test.Trouve)
            //{
            //    CpVille.InsererVilleEnBDD(y.Code_postal_ville, y.Nom_ville);
            //    x.Id_CpVille = CpVille.VerificationVille(y.Nom_ville).Id;
            //}
            //else
            //{
            //    x.Id_CpVille = test.Id;
            //}


            //p.FK_Id_Categorie = Convert.ToInt32(x);

            //Console.Write("Origine du produit : ");
            //x = Console.ReadLine();
            //p.FK_Id_Origine = Convert.ToInt32(x);

            //Console.Write("Unite du produit : ");
            //x = Console.ReadLine();
            //p.FK_Id_Unite = Convert.ToInt32(x);
            #endregion
        }
        #endregion

                
        #region AfficherMenuAcheter

        public static void AfficherMenuAcheter()
        {
            Console.Clear();
            Console.WriteLine("UTILISER LES FLECHES DE DROITE ET DE GAUCHE POUR AFFICHER LES PRODUITS");
            Console.WriteLine("APPUYER SUR P POUR FAIRE VOTRE PANIER");
            Console.WriteLine("APPUYER SUR V POUR VALIDEZ VOS ACHATS");
            Console.WriteLine();
            Console.WriteLine("Voici nos produit(s) : ");
            Console.WriteLine();
        }
        #endregion


        #region EndMenu
        public static void EndMenu()
        {
            Console.WriteLine("                         ¶¶¶¶¶¶¶¶¶¶¶¶ ");
            Console.WriteLine("                         ¶¶            ¶¶ ");
            Console.WriteLine("           ¶¶¶¶¶        ¶¶                ¶¶ ");
            Console.WriteLine("           ¶     ¶     ¶¶      ¶¶    ¶¶     ¶¶ ");
            Console.WriteLine("            ¶     ¶    ¶¶       ¶¶    ¶¶      ¶¶ ");
            Console.WriteLine("             ¶    ¶   ¶¶        ¶¶    ¶¶      ¶¶ ");
            Console.WriteLine("              ¶   ¶   ¶                         ¶¶ ");
            Console.WriteLine("            ¶¶¶¶¶¶¶¶¶¶¶¶                         ¶¶ ");
            Console.WriteLine("           ¶            ¶    ¶¶            ¶¶    ¶¶ ");
            Console.WriteLine("          ¶¶            ¶    ¶¶            ¶¶    ¶¶ ");
            Console.WriteLine("         ¶¶   ¶¶¶¶¶¶¶¶¶¶¶      ¶¶        ¶¶     ¶¶ ");
            Console.WriteLine("          ¶               ¶       ¶¶¶¶¶¶¶       ¶¶ ");
            Console.WriteLine("         ¶¶              ¶                    ¶¶ ");
            Console.WriteLine("          ¶   ¶¶¶¶¶¶¶¶¶¶¶¶                   ¶¶ ");
            Console.WriteLine("          ¶¶           ¶  ¶¶                ¶¶ ");
            Console.WriteLine("          ¶¶¶¶¶¶¶¶¶¶¶¶    ¶¶            ¶¶");
            Console.WriteLine("                          ¶¶¶¶¶¶¶¶¶¶¶");

            Console.ReadKey();
        }
        #endregion

        #region AcheterProduit
        public static void AcheterProduit()
        {
            AfficherMenuAcheter();
            List<Produit> Panier = new List<Produit>();
            List<Commande> ListeCommande = new List<Commande>();
            decimal PrixPanier = 0;
            int start = 0;
            Display10Product(start, "Nom_Produit");
            List<Produit> AllProduits = new List<Produit>();
            AllProduits = Produit.GetAllProducts();
            decimal taille = AllProduits.Count;
            int pageActuel = 1;
            decimal calcul = (taille / 5m);
            decimal pagesTotal = Math.Ceiling(calcul);
            Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);
            bool display = false;
            while (display == false)

            {

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        AfficherMenuAcheter();
                        start = 0;
                        Display10Product(start, "Nom_Produit");
                        AllProduits = Produit.GetAllProducts();
                        taille = AllProduits.Count;
                        pageActuel = 1;
                        calcul = taille / 5;
                        pagesTotal = Math.Ceiling(calcul);
                        Console.WriteLine(pagesTotal);
                        Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);
                        break;
                    case ConsoleKey.RightArrow:
                        AfficherMenuAcheter();

                        //Console.WriteLine(taille);
                        if (start < taille - 5)
                        {
                            start = start + 5;
                            pageActuel = pageActuel + 1;
                        }

                        Display10Product(start, "Nom_Produit");
                        Console.WriteLine("\n Page {0} sur {1}", pageActuel, pagesTotal);
                        //Console.WriteLine("Fleche de droite");
                        break;
                    case ConsoleKey.P: // PANIER
                        Console.Clear();
                        Console.WriteLine("Voici nos produit(s) : \n");
                        Display10Product(start, "Nom_Produit");
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
                                produit = Produit.GetOneProductById(numero);
                                commande.FK_Id_Produit = numero;
                                Console.WriteLine("Combien ?");
                                message = Console.ReadLine();
                                numero = Convert.ToInt32(message);
                                PrixPanier = PrixPanier + (produit.Prix_Produit * numero);
                                commande.Qtite_Produit = numero;
                                int numfacture = Facture.GetLastNumFacture();
                                commande.FK_Id_Facture = numfacture;
                                ListeCommande.Add(commande);
                                for (int i = 0; i < numero; i++)
                                {
                                    Panier.Add(produit);
                                }

                                Console.WriteLine("Votre produit a été ajouter au panier !");
                            }


                        }
                        Console.WriteLine("Voici votre commande : ");
                        for (int i = 0; i < ListeCommande.Count; i++)
                        {
                            Produit produitCommander = new Produit();
                            produitCommander = Produit.GetOneProductById(ListeCommande[i].FK_Id_Produit);
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

                                //Commande commande = new Commande();

                                for (int i = 0; i < ListeCommande.Count; i++)
                                {
                                    Commande.AddCommande(ListeCommande[i]);
                                }

                                int numfacture = Facture.GetLastNumFacture();
                                numfacture = numfacture + 1;
                                int IdClient = 3;
                                DateTime DateDuJour = DateTime.Today;
                                facture.Num_facture = numfacture;
                                facture.Date_facture = DateDuJour;
                                facture.Montant_total = PrixPanier;
                                facture.Fk_Id_Client = IdClient;
                                Facture.AddFacture(facture);
                                while (PrixPanier != 0)
                                {
                                    optionPaiements = OptionPaiement.GetAllPayement();
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
                                    ifp.FK_Id_Facture = numfacture;

                                    IFP.AddIFP(ifp);



                                }
                                display = true;

                                break;
                            case ConsoleKey.N:
                                Console.WriteLine("Appuyez sur entrée pour continuer");
                                break;

                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        AfficherMenuAcheter();

                        if (start >= 5)
                        {
                            start = start - 5;
                            pageActuel = pageActuel - 1;
                        }
                        Display10Product(start, "Nom_Produit");
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
                                    Commande.AddCommande(ListeCommande[i]);
                                }
                                int numfacture = Facture.GetLastNumFacture();
                                numfacture = numfacture + 1;
                                int IdClient = 3;
                                DateTime DateDuJour = DateTime.Today;
                                facture.Num_facture = numfacture;
                                facture.Date_facture = DateDuJour;
                                facture.Montant_total = PrixPanier;
                                facture.Fk_Id_Client = IdClient;
                                Facture.AddFacture(facture);

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



        #region MENU PRINCIPAL
        public static void MenuPrincipalAdmin()
        {
            while (true)
            {
                
                Console.Clear();
                Console.WriteLine("MODE ADMIN");
                Console.Write("\nMenu principal" +
                    "\n\t - [1] CREER UN PRODUIT" +
                    "\n\t - [2] MODIFIER UN PRODUIT" +
                    "\n\t - [3] SUPPRIMER UN PRODUIT" +
                    "\n\t - [4] AFFICHER LES PRODUITS" +
                    "\n\t - [5] AFFICHER UN PRODUIT" +
                    "\n\t - [6] ACHETER UN PRODUIT" +
                    "\n\t - [x] QUITTEZ LE PROGRAMME");
                Console.WriteLine();
                //bool sortie = false;
                //string choixEffectue = Console.ReadLine();
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.NumPad1: // CREER UN PRODUIT
                        Console.Clear();
                        Console.WriteLine("MODE ADMIN");
                        Console.WriteLine("AJOUT D'UN PRODUIT DANS LA BASE DE DONNEES");
                        FonctionsJL.AjouterProduit();
                        Console.WriteLine();
                        Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter)
                        {
                            Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");

                        }
                        break;
                    case ConsoleKey.NumPad2: // MODIFIER UN PRODUIT
                        Console.Clear();
                        FonctionsJL.ModifierProduit();
                        Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter)
                        {
                            Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");

                        }
                        break;
                    case ConsoleKey.NumPad3: // SUPPRIMER UN PRODUIT
                        Console.Clear();
                        FonctionsJL.SupprimerProduit();
                        Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter)
                        {
                            Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");

                        }
                        break;
                    case ConsoleKey.NumPad4: // AFFICHER LES PRODUITS
                        Console.Clear();
                        FonctionsJL.DisplayProduct();
                        Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter)
                        {
                            Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
                        }

                        break;
                    case ConsoleKey.NumPad5: // AFFICHER UN PRODUIT
                        Console.Clear();
                        FonctionsJL.AfficherUnProduit();
                        Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter)
                        {
                            Console.WriteLine("APPUYER SUR ENTRER POUR RETOURNER AU MENU PRINCIPAL");
                        }


                        break;
                    case ConsoleKey.NumPad7:
                        Console.Clear();
                        Console.WriteLine("ON TEST");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad6: // ACHETER UN PRODUIT
                        AcheterProduit();
                        break;
                    case ConsoleKey.X:
                        Console.Clear();
                        Console.WriteLine("Merci, et à bientôt !");

                        #region trolololo
                        //Console.WriteLine("                                                .....'',;;::cccllllllllllllcccc:::;;,,,''...'',,'.. ");
                        //Console.WriteLine("                                        ..';cldkO00KXNNNNXXXKK000OOkkkkkxxxxxddoooddddddxxxxkkkkOO0XXKx:.");
                        //Console.WriteLine("                                  .':ok0KXXXNXK0kxolc:;;,,,,,,,,,,,;;,,,''''''',,''..              .'lOXKd'");
                        //Console.WriteLine("                             .,lx00Oxl:,'............''''''...................    ...,;;'.             .oKXd.");
                        //Console.WriteLine("                          .ckKKkc'...'',:::;,'.........'',;;::::;,'..........'',;;;,'.. .';;'.           'kNKc.");
                        //Console.WriteLine("                       .:kXXk:.    ..       ..................          .............,:c:'...;:'.         .dNNx.");
                        //Console.WriteLine("                      :0NKd,          .....''',,,,''..               ',...........',,,'',,::,...,,.        .dNNx.");
                        //Console.WriteLine("                     .xXd.         .:;'..         ..,'             .;,.               ...,,'';;'. ...       .oNNo");
                        //Console.WriteLine("                     .0K.         .;.              ;'              ';                      .'...'.           .oXX:");
                        //Console.WriteLine("                    .oNO.         .                 ,.              .     ..',::ccc:;,..     ..                lXX:");
                        //Console.WriteLine("                   .dNX:               ......       ;.                'cxOKK0OXWWWWWWWNX0kc.                    :KXd.");
                        //Console.WriteLine("                 .l0N0;             ;d0KKKKKXK0ko:...              .l0X0xc,...lXWWWWWWWWKO0Kx'                   ,ONKo.");
                        //Console.WriteLine("               .lKNKl...'......'. .dXWN0kkk0NWWWWWN0o.            :KN0;.  .,cokXWWNNNNWNKkxONK: .,:c:.      .';;;;:lk0XXx;");
                        //Console.WriteLine("              :KN0l';ll:'.         .,:lodxxkO00KXNWWWX000k.       oXNx;:okKX0kdl:::;'',;coxkkd, ...'. ...'''.......',:lxKO:.");
                        //Console.WriteLine("             oNNk,;c,'',.                      ...;xNNOc,.         ,d0X0xc,.     .dOd,           ..;dOKXK00000Ox:.   ..''dKO,");
                        //Console.WriteLine("            'KW0,:,.,:..,oxkkkdl;'.                'KK'              ..           .dXX0o:'....,:oOXNN0d;.'. ..,lOKd.   .. ;KXl.");
                        //Console.WriteLine("            ; XNd,;  ;. l00kxoooxKXKx:..ld:         ;KK'                             .:dkO000000Okxl;.   c0;      :KK;   .  ;XXc");
                        //Console.WriteLine("            'XXdc.  :. ..    '' 'kNNNKKKk,      .,dKNO.                                   ....       .'c0NO'      :X0.  ,.  xN0.");
                        //Console.WriteLine("            .kNOc'  ,.      .00. ..''...      .l0X0d;.             'dOkxo;...                    .;okKXK0KNXx;.   .0X:  ,.  lNX'");
                        //Console.WriteLine("             , KKdl  .c,    .dNK,            .;xXWKc.                .;:coOXO,,'.......       .,lx0XXOo;...oNWNXKk:.'KX;  '   dNX.");
                        //Console.WriteLine("              :XXkc'....  .dNWXl        .';l0NXNKl.          ,lxkkkxo' .cK0.          ..;lx0XNX0xc.     ,0Nx'.','.kXo  .,  ,KNx.");
                        //Console.WriteLine("               cXXd,,;:, .oXWNNKo'    .'..  .'.'dKk;        .cooollox;.xXXl     ..,cdOKXXX00NXc.      'oKWK'     ;k:  .l. ,0Nk.");
                        //Console.WriteLine("                cXNx.  . ,KWX0NNNXOl'.           .o0Ooldk;            .:c;.':lxOKKK0xo:,.. ;XX:   .,lOXWWXd.      . .':,.lKXd.");
                        //Console.WriteLine("                 lXNo    cXWWWXooNWNXKko;'..       .lk0x;       ...,:ldk0KXNNOo:,..       ,OWNOxO0KXXNWNO,        ....'l0Xk,");
                        //Console.WriteLine("                 .dNK.   oNWWNo.cXK;;oOXNNXK0kxdolllllooooddxk00KKKK0kdoc:c0No        .'ckXWWWNXkc,;kNKl.          .,kXXk,");
                        //Console.WriteLine("                  'KXc  .dNWWX;.xNk.  .kNO::lodxkOXWN0OkxdlcxNKl,..        oN0'..,:ox0XNWWNNWXo.  ,ONO'           .o0Xk;");
                        //Console.WriteLine("                  .ONo    oNWWN0xXWK, .oNKc       .ONx.      ;X0.          .:XNKKNNWWWWNKkl;kNk. .cKXo.           .ON0;");
                        //Console.WriteLine("                  .xNd   cNWWWWWWWWKOkKNXxl:,'...;0Xo'.....'lXK;...',:lxk0KNWWWWNNKOd:..   lXKclON0:            .xNk.");
                        //Console.WriteLine("                  .dXd   ;XWWWWWWWWWWWWWWWWWWNNNNNWWNNNNNNNNNWWNNNNNNWWWWWNXKNNk;..        .dNWWXd.             cXO.");
                        //Console.WriteLine("                  .xXo   .ONWNWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWNNK0ko:'..OXo          'l0NXx,              :KK,");
                        //Console.WriteLine("                  .OXc    :XNk0NWXKNWWWWWWWWWWWWWWWWWWWWWNNNX00NNx:'..       lXKc.     'lONN0l.              .oXK:");
                        //Console.WriteLine("                  .KX;    .dNKoON0;lXNkcld0NXo::cd0NNO:;,,'.. .0Xc            lXXo..'l0NNKd,.              .c0Nk,");
                        //Console.WriteLine("                  :XK.     .xNX0NKc.cXXl  ;KXl    .dN0.       .0No            .xNXOKNXOo,.               .l0Xk;.");
                        //Console.WriteLine("                 .dXk.      .lKWN0d::OWK;  lXXc    .OX:       .ONx.     . .,cdk0XNXOd;.   .'''....;c:'..;xKXx,");
                        //Console.WriteLine("                 .0No         .:dOKNNNWNKOxkXWXo:,,;ONk;,,,,,;c0NXOxxkO0XXNXKOdc,.  ..;::,...;lol;..:xKXOl.");
                        //Console.WriteLine("                 , XX:             ..';cldxkOO0KKKXXXXXXXXXXKKKKK00Okxdol:;'..   .';::,..':llc,..'lkKXkc.");
                        //Console.WriteLine("                 :NX'    .     ''            ..................             .,;:;,',;ccc;'..'lkKX0d;.");
                        //Console.WriteLine("                 lNK.   .;      ,lc,.         ................        ..,,;;;;;;:::,....,lkKX0d:.");
                        //Console.WriteLine("                .oN0.    .'.      .;ccc;,'....              ....'',;;;;;;;;;;'..   .;oOXX0d:.");
                        //Console.WriteLine("                .dN0.      .;;,..       ....                ..''''''''....     .:dOKKko;.");
                        //Console.WriteLine("                 lNK'         ..,;::;;,'.........................           .;d0X0kc'.");
                        //Console.WriteLine("                 .xXO'                                                 .;oOK0x:.");
                        //Console.WriteLine("                  .cKKo.                                    .,:oxkkkxk0K0xc'.");
                        //Console.WriteLine("                    .oKKkc,.                         .';cok0XNNNX0Oxoc,.");
                        //Console.WriteLine("                      .;d0XX0kdlc:;,,,',,,;;:clodkO0KK0Okdl:,'..");
                        //Console.WriteLine("                          .,coxO0KXXXXXXXKK0OOxdoc:,..");
                        //Console.WriteLine("                                    ...");
                        #endregion



                        EndMenu();
                        return;


                }
            }

        }
        #endregion



    }
}
