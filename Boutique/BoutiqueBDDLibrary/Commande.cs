using System;
using System.Collections.Generic;
using System.Text;
using BoutiqueBDDLibrary;
using MySql.Data.MySqlClient;
using BoutiqueBDDLibrary;

namespace BoutiqueBDDLibrary
{
    public class Commande
    {
        private int id_Commande;
        private int fK_Id_Facture;
        private int fK_Id_Produit;
        private int qtite_Produit;

        public Commande()
        {
        }

        public Commande(int fK_Id_Facture, int fK_Id_Produit, int qtite_Produit)
        {
            FK_Id_Facture = fK_Id_Facture;
            FK_Id_Produit = fK_Id_Produit;
            Qtite_Produit = qtite_Produit;
        }

        public int Id_Commande { get => id_Commande; set => id_Commande = value; }
        public int FK_Id_Facture { get => fK_Id_Facture; set => fK_Id_Facture = value; }
        public int FK_Id_Produit { get => fK_Id_Produit; set => fK_Id_Produit = value; }
        public int Qtite_Produit { get => qtite_Produit; set => qtite_Produit = value; }

        public static void AddCommande(Commande commande)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO commande (FK_Id_Facture, FK_Id_Produit, Qtite_Produit) VALUES (@FK_Id_Facture, @FK_Id_Produit, @Qtite_Produit)";

                insertCommand.Parameters.AddWithValue("@FK_Id_Facture", commande.FK_Id_Facture);
                insertCommand.Parameters.AddWithValue("@FK_Id_Produit", commande.FK_Id_Produit);
                insertCommand.Parameters.AddWithValue("@Qtite_Produit", commande.Qtite_Produit);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

    }


}
