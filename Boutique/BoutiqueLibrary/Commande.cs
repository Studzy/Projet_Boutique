namespace BoutiqueBDDLibrary
{
    public class Commande
    {
        //Déclaration de base de données
        private int id_Commande;
        private int fK_Id_Facture;
        private int fK_Id_Produit;
        private int qtite_Produit;

        //Constructeur
        public Commande()
        {
        }

        //Get;Set; Vérifications
        #region Id_Commande
        public int Id_Commande { get => id_Commande; set => id_Commande = value; }
        #endregion

        #region FK_Id_Facture
        public int FK_Id_Facture { get => fK_Id_Facture; set => fK_Id_Facture = value; }
        #endregion

        #region FK_Id_ProduitFIER CAR NON FINI FK_Id_Produit
        public int FK_Id_Produit { get => fK_Id_Produit; set => fK_Id_Produit = value; }
        #endregion

        #region Qtite_Produit
        /// <summary>
        /// Vérifie quantité du produit dans le set, si ce n'est pas bon une exeption est afficher
        /// </summary>
        public int Qtite_Produit
        {
            get => qtite_Produit;
            set
            {
                FonctionsConsole.verifInt(value);
                qtite_Produit = value;

            }
        }
        #endregion
    }
}
