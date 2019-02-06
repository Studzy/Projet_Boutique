using System;

namespace BoutiqueBDDLibrary
{
    public class Facture
    {
        //Déclare des variables
        private int id_Facture;
        private int num_facture;
        private DateTime date_facture;
        private decimal montant_total;
        private int fk_Id_Client;

        //Constructeur
        public Facture()
        {
        }

        //Get;Set; Vérifications
        #region Num_Facture
        public int Num_facture { get => num_facture; set => num_facture = value; }
        #endregion

        #region Date_Facture
        /// <summary>
        /// Vérifie la date de la facture dans le set, si ce n'est pas bon une exeption est afficher
        /// </summary>
        public DateTime Date_facture
        {
            get => date_facture;
            set
            {
                FonctionsConsole.verifDateDeNaissance(value);
                date_facture = value;
            }
        }
        #endregion

        #region Montant_Total
        public decimal Montant_total { get => montant_total; set => montant_total = value; }
        #endregion

        #region Id_Facture
        public int Id_Facture { get => id_Facture; set => id_Facture = value; }
        #endregion

        #region FK_Id_Client
        public int Fk_Id_Client { get => fk_Id_Client; set => fk_Id_Client = value; }
        #endregion
    }
}
