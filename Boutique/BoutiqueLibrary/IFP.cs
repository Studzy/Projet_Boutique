namespace BoutiqueBDDLibrary
{
    public class IFP
    {
        //Déclaration des variables
        private int id_IFP;
        private int fK_Id_Facture;
        private int fK_Id_Paiement;
        private decimal montant_Paiement;

        //Constructeur
        public IFP()
        {
        }

        //Get;Set; Vérifications
        #region Id_FP
        public int Id_IFP { get => id_IFP; set => id_IFP = value; }
        #endregion

        #region FK_Id_Facture
        public int FK_Id_Facture { get => fK_Id_Facture; set => fK_Id_Facture = value; }
        #endregion

        #region FK_Id_Paiement
        public int FK_Id_Paiement { get => fK_Id_Paiement; set => fK_Id_Paiement = value; }
        #endregion

        #region Montant_paiement
        /// <summary>
        /// Vérifie le montant du paiement dans le set. Si ce n'est pas bon, une exception est affichée.
        /// </summary>
        public decimal Montant_Paiement
        {
            get => montant_Paiement;
            set
            {
                FonctionsConsole.verifDecimal(value);
                montant_Paiement = value;

            }
        }
        #endregion
    }
}
