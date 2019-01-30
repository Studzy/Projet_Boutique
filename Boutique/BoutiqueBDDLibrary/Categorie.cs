using System;
using System.Collections.Generic;
using System.Text;
using BoutiqueBDDLibrary;
using MySql.Data.MySqlClient;
using BoutiqueBDDLibrary;

namespace BoutiqueBDDLibrary
{
    public class Categorie
    {
        private int id_Categorie;
        private string nom_categorie;

        public Categorie()
        {
        }

        public Categorie(int id_Categorie, string nom_categorie)
        {
            Id_Categorie = id_Categorie;
            Nom_categorie = nom_categorie;
        }

        public int Id_Categorie { get => id_Categorie; set => id_Categorie = value; }
        public string Nom_categorie { get => nom_categorie; set => nom_categorie = value; }

        public static void AddCategorie(Categorie categorie)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO categorie (Nom_Categorie) VALUES (@Nom_Categorie)";

                insertCommand.Parameters.AddWithValue("@Nom_Categorie", categorie.Nom_categorie);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static IdTrouve VerificationCategorie(string categorie)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();

                MySqlCommand selectCommand = new MySqlCommand
                    ("SELECT Id_Categorie, Nom_Categorie FROM categorie WHERE Nom_Categorie = @categorie", db);
                selectCommand.Parameters.AddWithValue("@categorie", categorie);

                MySqlDataReader query = selectCommand.ExecuteReader();

                if (query.Read())
                {
                    int idCategorie = (int)query["Id_Categorie"];
                    return new IdTrouve(idCategorie);
                }
                else
                {
                    return new IdTrouve();
                }
            }
        }

    }

    
}
