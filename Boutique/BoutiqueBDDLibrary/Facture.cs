using System;
using System.Collections.Generic;
using System.Text;
using BoutiqueBDDLibrary;
using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class Facture
    {
        private int id_Facture;
        private int num_facture;
        private DateTime date_facture;
        private decimal montant_total;
        private int fk_Id_Client;

        public Facture()
        {
        }

        public Facture(int num_facture, DateTime date_facture, decimal montant_total, int fk_Id_Client)
        {

            Num_facture = num_facture;
            Date_facture = date_facture;
            Montant_total = montant_total;
            Fk_Id_Client = fk_Id_Client;

        }




        //Public String FirstName (get; set)
        public int Num_facture { get => num_facture; set => num_facture = value; }
        public DateTime Date_facture { get => date_facture; set => date_facture = value; }
        public decimal Montant_total { get => montant_total; set => montant_total = value; }
        public int Id_Facture { get => id_Facture; set => id_Facture = value; }
        public int Fk_Id_Client { get => fk_Id_Client; set => fk_Id_Client = value; }

        public static void AddFacture(Facture facture)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO facture (Num_Facture, Date_Facture, Montant_Total, FK_Id_Client) VALUES (@Num_Facture, @Date_Facture, @Montant_Total, @FK_Id_Client)";

                insertCommand.Parameters.AddWithValue("@Num_Facture", facture.Num_facture);
                insertCommand.Parameters.AddWithValue("@Date_Facture", facture.Date_facture);
                insertCommand.Parameters.AddWithValue("@Montant_Total", facture.Montant_total);
                insertCommand.Parameters.AddWithValue("@FK_Id_Client", facture.Fk_Id_Client);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }
        public static int GetLastNumFacture()
        {
            Facture facture = new Facture();
            int resultat;
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();

                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT facture.Num_Facture FROM facture ORDER BY facture.Num_Facture DESC LIMIT 1";

                MySqlDataReader query = insertCommand.ExecuteReader();

                if (query.Read())
                {

                    facture.Num_facture = query.GetInt32(0);
                }
                else
                {
                    facture.Num_facture = 0;
                }
                resultat = facture.Num_facture;

                db.Close();
            }

            return resultat;
        }

    }
    

}
