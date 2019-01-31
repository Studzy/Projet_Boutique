namespace BoutiqueBDDLibrary
{
    public class Produit
    {
        //Déclaration des variables
        private int id_Produit;
        private string Nom_produit;
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
        #region Nom_Produit
        /// <summary>
        /// Vérifie si le nom du produit est un string qui comporte au maximum 50 caractères.
        /// </summary>
        public string Nom_Produit
        {
            get => Nom_produit;
            set
            {
                if (value.Length < 1 || value.Length > 50 || !FonctionsConsole.VerifieSiQueDesLettres(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("Le nom n'est pas valable");
                }
                else
                {
                    Nom_produit = value;
                }
            }
        }
        #endregion

        #region TVA
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI TVA
        /// </summary>
        public decimal TVA
        {
            get => tva;
            set
            {

                if (typeof(decimal).IsAssignableFrom(value.GetType()))
                {
                    tva = value;
                }
                else
                {
                    throw new FonctionsConsole.MonMessageErreur("");
                }
            }
        }
        #endregion

        #region Prix_Produit
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI PRIX_PRODUIT
        /// </summary>
        public decimal Prix_Produit
        {
            get => prix_produit;
            set
            {
                try
                {
                    prix_produit = value;
                }
                catch (FonctionsConsole.MonMessageErreur)
                {
                    throw new FonctionsConsole.MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region Remise_Produit
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public decimal Remise_Produit
        {
            get => remise_produit;
            set
            {
                try
                {
                    remise_produit = value;
                }
                catch (FonctionsConsole.MonMessageErreur)
                {
                    throw new FonctionsConsole.MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region Description_Produit
        public string Description_Produit
        {
            get => description_produit;
            set
            {
                try
                {
                    description_produit = value;
                }
                catch (FonctionsConsole.MonMessageErreur)
                {
                    throw new FonctionsConsole.MonMessageErreur("La description n'est pas valable");
                }
            }
        }
        #endregion

        #region Val_Nutrition_Produit
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public int Val_Nutrition_Produit
        {
            get => val_nutrition_produit;
            set
            {
                try
                {
                    val_nutrition_produit = value;
                }
                catch (System.FormatException)
                {

                    throw new FonctionsConsole.MonMessageErreur("Cette valeur n'est pas valide");
                }
                
            }
        }
        #endregion

        #region FK_Id_Categorie
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
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
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
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
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
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

        #region Id_Produit
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public int Id_Produit
        {
            get => id_Produit;
            set
            {
                try
                {
                    id_Produit = value;
                }
                catch (FonctionsConsole.MonMessageErreur)
                {
                    throw new FonctionsConsole.MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region Nom_Categorie
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public string Nom_categorie { get => nom_categorie; set => nom_categorie = value; }
        #endregion

        #region Nom_Origine
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public string Nom_origine { get => nom_origine; set => nom_origine = value; }
        #endregion

        #region Libelle_Unite
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public string Libelle_unite { get => libelle_unite; set => libelle_unite = value; }
        #endregion
    }
}
