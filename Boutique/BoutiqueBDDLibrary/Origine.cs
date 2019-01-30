using System;
using System.Collections.Generic;
using System.Text;
using BoutiqueBDDLibrary;
using MySql.Data.MySqlClient;
using BoutiqueBDDLibrary;

namespace BoutiqueBDDLibrary
{
    public class Origine
    {
        private int id_Origine;
        private string nom_Origine;

        public Origine()
        {
        }

        public Origine(int id_Origine, string nom_Origine)
        {
            Id_Origine = id_Origine;
            Nom_Origine = nom_Origine;
        }

        public int Id_Origine
        {
            get => id_Origine;
            set => id_Origine = value;
        }
        public string Nom_Origine { get => nom_Origine; set => nom_Origine = value.ToUpper(); }

        public static void AddOrigine(Origine origine)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO origine (Nom_Origine) VALUES (@Nom_Origine)";

                insertCommand.Parameters.AddWithValue("@Nom_Origine", origine.Nom_Origine);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static IdTrouve VerificationOrigine(string origine)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();

                MySqlCommand selectCommand = new MySqlCommand
                    ("SELECT Id_Origine, Nom_Origine FROM origine WHERE Nom_Origine = @origine", db);
                selectCommand.Parameters.AddWithValue("@origine", origine);

                MySqlDataReader query = selectCommand.ExecuteReader();

                if (query.Read())
                {
                    int idOrigine = (int)query["Id_Origine"];
                    return new IdTrouve(idOrigine);
                }
                else
                {
                    return new IdTrouve();
                }
            }
        }

    }
}
