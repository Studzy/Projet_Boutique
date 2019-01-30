using System;
using System.Collections.Generic;
using System.Text;
using BoutiqueBDDLibrary;
using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class OptionPaiement
    {
        private int id_Paiement;
        private string libelle_paiement;

        public OptionPaiement()
        {
        }

        public OptionPaiement(string libelle_paiement)
        {
            Libelle_paiement = libelle_paiement;
        }

        public int Id_Paiement { get => id_Paiement; set => id_Paiement = value; }
        public string Libelle_paiement { get => libelle_paiement; set => libelle_paiement = value; }

        public static List<OptionPaiement> GetAllPayement()
        {
            List<OptionPaiement> entries = new List<OptionPaiement>();

            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();

                MySqlCommand selectCommand = new MySqlCommand
                    ("SELECT * from opt_paiement", db);

                MySqlDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    OptionPaiement optionPaiement = new OptionPaiement();
                    optionPaiement.Id_Paiement = query.GetInt32(0);
                    optionPaiement.Libelle_paiement = query.GetString(1);
                    entries.Add(optionPaiement);
                }

                db.Close();
            }

            return entries;
        }
    }
}
