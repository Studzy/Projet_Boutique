using System;
using System.Collections.Generic;
using System.Text;
using BoutiqueBDDLibrary;
using MySql.Data.MySqlClient;
using BoutiqueBDDLibrary;

namespace BoutiqueBDDLibrary
{
    public class Unite
    {
        private int id_Libelle;
        private string libelle_unite;

        public Unite()
        {
        }

        public Unite(int id_Libelle, string libelle_unite)
        {
            Id_Libelle = id_Libelle;
            Libelle_unite = libelle_unite;
        }

        public int Id_Libelle { get => id_Libelle; set => id_Libelle = value; }
        public string Libelle_unite { get => libelle_unite; set => libelle_unite = value; }

        public static void AddUnite(Unite unite)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO unite (Libelle_Unite) VALUES (@Libelle_Unite)";

                insertCommand.Parameters.AddWithValue("@Libelle_Unite", unite.Libelle_unite);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }
        public static IdTrouve VerificationUnite(string unite)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();

                MySqlCommand selectCommand = new MySqlCommand
                    ("SELECT Id_Unite, Libelle_Unite FROM unite WHERE Libelle_Unite = @Libelle_Unite", db);
                selectCommand.Parameters.AddWithValue("@Libelle_Unite", unite);

                MySqlDataReader query = selectCommand.ExecuteReader();

                if (query.Read())
                {
                    int idUnite = (int)query["Id_Unite"];
                    return new IdTrouve(idUnite);
                }
                else
                {
                    return new IdTrouve();
                }
            }
        }
    }
}
