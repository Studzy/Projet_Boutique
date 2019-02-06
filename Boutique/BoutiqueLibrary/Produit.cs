namespace BoutiqueBDDLibrary
{
    public class Produit
    {
        //Déclaration des variables
        private int id_Produit;
        private string nom_produit;
        private decimal tva;
        private decimal prix_produit;
        private decimal remise_produit;
        private string description_produit;
        private int val_nutrition_produit;
        private int fK_Id_Categorie;
        private int fK_Id_Origine;
        private int fK_Id_Unite;
        private string nom_categorie;
        private string nom_origine;
        private string libelle_unite;

        //Constructeur
        public Produit()
        {
        }

        //Get;Set; Vérifications

        #region Id_Produit
        public int Id_Produit
        {
            get => id_Produit;
            set
            {

                id_Produit = value;

            }
        }
        #endregion

        #region Nom_Produit
        /// <summary>
        /// Vérifie le nom du produit dans le set. Si ce n'est pas bon, une exception est affichée.
        /// </summary>
        public string Nom_Produit
        {
            get => nom_produit;
            set
            {
                if (value.Length < 1 || value.Length > 50 || !FonctionsConsole.VerifieSiQueDesLettres(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("ERREUR: Le nom n'est pas valable");
                }
                else
                {
                    FonctionsConsole.premiereLettreMajuscule(value);
                    nom_produit = value;
                }
            }
        }
        #endregion

        #region TVA
        /// <summary>
        /// Vérifie la tva dans le set. Si ce n'est pas bon, une exception est affichée.
        /// </summary>
        public decimal TVA
        {
            get => tva;
            set
            {

                FonctionsConsole.verifDecimal(value);
                if (value > 100 || value < 0)
                {
                    throw new FonctionsConsole.MonMessageErreur("Error: La valeur est incorrect.");
                }
                else
                {
                    tva = value;
                }
                

            }
        }
        #endregion

        #region Prix_Produit
        /// <summary>
        /// Vérifie le pris du produit dans le set. Si ce n'est pas bon, une exception est affichée.
        /// </summary>
        public decimal Prix_Produit
        {
            get => prix_produit;
            set
            {
                FonctionsConsole.verifDecimal(value);
                prix_produit = value;

            }
        }
        #endregion

        #region Remise_Produit
        /// <summary>
        /// Vérifie la remise de produit dans le set. Si ce n'est pas bon, une exception est affichée.
        /// </summary>
        public decimal Remise_Produit
        {
            get => remise_produit;
            set
            {

                FonctionsConsole.verifDecimal(value);
                if (value > 100 || value < 0)
                {
                    throw new FonctionsConsole.MonMessageErreur("Error: La valeur est incorrect.");
                }
                else
                {
                    remise_produit = value;
                }
                
            }
        }
        #endregion

        #region Description_Produit
        /// <summary>
        /// Vérifie la description du produit dans le set. Si ce n'est pas bon, une exception est affichée.
        /// </summary>
        public string Description_Produit
        {
            get => description_produit;
            set
            {
                if (value.Length > 65535)
                {
                    throw new FonctionsConsole.MonMessageErreur("ERREUR: La taille est invalide");
                }
                else
                {
                    description_produit = value;
                }
            }
        }
        #endregion

        #region Val_Nutrition_Produit
        /// <summary>
        /// Vérifie la valeur nutritionnelle dans le set. Si ce n'est pas bon, une exception est affichée.
        /// </summary>
        public int Val_Nutrition_Produit
        {
            get => val_nutrition_produit;
            set
            {
                if (value < 0)
                {
                    throw new FonctionsConsole.MonMessageErreur("Cette valeur n'est pas valide");
                }
                else
                {
                    val_nutrition_produit = value;
                }

            }
        }
        #endregion

        #region FK_Id_Categorie
        public int FK_Id_Categorie
        {
            get => fK_Id_Categorie;
            set
            {
                try
                {
                    fK_Id_Categorie = value;
                }
                catch (FonctionsConsole.MonMessageErreur)
                {
                    throw new FonctionsConsole.MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region FK_Id_Origine
        public int FK_Id_Origine
        {
            get => fK_Id_Origine;
            set
            {
                try
                {
                    fK_Id_Origine = value;
                }
                catch (FonctionsConsole.MonMessageErreur)
                {
                    throw new FonctionsConsole.MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region  FK_Id_Unite
        public int FK_Id_Unite
        {
            get => fK_Id_Unite;
            set
            {
                try
                {
                    fK_Id_Unite = value;
                }
                catch (FonctionsConsole.MonMessageErreur)
                {
                    throw new FonctionsConsole.MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion



        #region Nom_Categorie
        public string Nom_categorie { get => nom_categorie; set => nom_categorie = value; }
        #endregion

        #region Nom_Origine
        public string Nom_origine { get => nom_origine; set => nom_origine = value; }
        #endregion

        #region Libelle_Unite
        public string Libelle_unite { get => libelle_unite; set => libelle_unite = value; }
        #endregion
    }
}
