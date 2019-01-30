using System;
using System.Collections.Generic;
using System.Text;
using BoutiqueBDDLibrary;
using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class IFP
    {
        private int id_IFP;
        private int fK_Id_Facture;
        private int fK_Id_Paiement;
        private decimal montant_Paiement;

        public IFP()
        {
        }

        public IFP(int fK_Id_Facture, int fK_Id_Paiement, decimal montant_Paiement)
        {
            FK_Id_Facture = fK_Id_Facture;
            FK_Id_Paiement = fK_Id_Paiement;
            Montant_Paiement = montant_Paiement;
        }

        public int Id_IFP { get => id_IFP; set => id_IFP = value; }
        public int FK_Id_Facture { get => fK_Id_Facture; set => fK_Id_Facture = value; }
        public int FK_Id_Paiement { get => fK_Id_Paiement; set => fK_Id_Paiement = value; }
        public decimal Montant_Paiement { get => montant_Paiement; set => montant_Paiement = value; }


        public static void AddIFP(IFP ifp)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO inter_facture_paiement (FK_Id_Facture, FK_Id_Paiement, Montant_Paiement) VALUES (@FK_Id_Facture, @FK_Id_Paiement, @Montant_paiement)";


                insertCommand.Parameters.AddWithValue("@FK_Id_Facture", ifp.FK_Id_Facture);
                insertCommand.Parameters.AddWithValue("@FK_Id_Paiement", ifp.FK_Id_Paiement);
                insertCommand.Parameters.AddWithValue("@Montant_paiement", ifp.Montant_Paiement);
                insertCommand.ExecuteReader();

                db.Close();
            }

        }

    }
}
