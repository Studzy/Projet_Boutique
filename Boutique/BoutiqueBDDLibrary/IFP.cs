using System;
using System.Collections.Generic;
using System.Text;
using BoutiqueBDDLibrary;
using MySql.Data.MySqlClient;

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
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI Id_FP
        /// </summary>
        public int Id_IFP { get => id_IFP; set => id_IFP = value; }
        #endregion

        #region FK_Id_Facture
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI FK_Id_Facture
        /// </summary>
        public int FK_Id_Facture { get => fK_Id_Facture; set => fK_Id_Facture = value; }
        #endregion

        #region FK_Id_Paiement
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI FK_Id_Paiement
        /// </summary>
        public int FK_Id_Paiement { get => fK_Id_Paiement; set => fK_Id_Paiement = value; }
        #endregion

        #region Montant_paiement
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI Montant_Paiement
        /// </summary>
        public decimal Montant_Paiement { get => montant_Paiement; set => montant_Paiement = value; }
        #endregion

        //Base de données
        #region [BDD] Ajouter un moyen de paiement
        /// <summary>
        /// Ajoute un moyen de paiement à la table "Inter_Facture_Paiement".
        /// </summary>
        public static void AddIFP(IFP ifp)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO inter_facture_paiement (FK_Id_Facture, FK_Id_Paiement, Montant_Paiement) VALUES (@FK_Id_Facture, @FK_Id_Paiement, @Montant_paiement)";

                insertCommand.Parameters.AddWithValue("@FK_Id_Facture", ifp.FK_Id_Facture);
                insertCommand.Parameters.AddWithValue("@FK_Id_Paiement", ifp.FK_Id_Paiement);
                insertCommand.Parameters.AddWithValue("@Montant_paiement", ifp.Montant_Paiement);
                insertCommand.ExecuteReader();
            }
        }
        #endregion
    }
}
