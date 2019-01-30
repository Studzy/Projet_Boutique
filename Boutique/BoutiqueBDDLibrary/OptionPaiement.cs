using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class OptionPaiement
    {
        //Déclaration des variables
        private int id_Paiement;
        private string libelle_paiement;

        //Constructeur
        public OptionPaiement()
        {
        }

        //Get;Set; Vérifications
        #region Id_Paiement
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public int Id_Paiement { get => id_Paiement; set => id_Paiement = value; }
        #endregion

        #region Libelle_Paiement
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public string Libelle_paiement { get => libelle_paiement; set => libelle_paiement = value; }
        #endregion

        //Base de données
        #region [BDD] Afficher toute les options de paiement
        /// <summary>
        /// Affiche toutes les options de paiement situé dans la table opt_paiement.
        /// Stock les options de paiement dans une liste.
        /// </summary>
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
            }
            return entries;
        }
        #endregion
    }
}
