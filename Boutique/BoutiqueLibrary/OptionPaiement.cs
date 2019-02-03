namespace BoutiqueBDDLibrary
{
    public class OptionPaiement
    {
        //Déclaration des variables
        private int id_Paiement;
        private string libelle_paiement;

        //Constructeur
        public OptionPaiement()
        {
        }

        //Get;Set; Vérifications
        #region Id_Paiement
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public int Id_Paiement { get => id_Paiement; set => id_Paiement = value; }
        #endregion

        #region Libelle_Paiement
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public string Libelle_paiement
        {
            get => libelle_paiement;
            set => libelle_paiement = value;
        }
        #endregion
    }
}
