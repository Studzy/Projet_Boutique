using System;
using MySql.Data.MySqlClient;

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
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public int Num_facture { get => num_facture; set => num_facture = value; }
        #endregion

        #region Date_Facture
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public DateTime Date_facture { get => date_facture; set => date_facture = value; }
        #endregion

        #region Montant_Total
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public decimal Montant_total { get => montant_total; set => montant_total = value; }
        #endregion

        #region Id_Facture
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public int Id_Facture { get => id_Facture; set => id_Facture = value; }
        #endregion

        #region FK_Id_Client
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public int Fk_Id_Client { get => fk_Id_Client; set => fk_Id_Client = value; }
        #endregion

        //Base de données
        #region [BDD] Ajoute une facture
        /// <summary>
        /// Requête SQL qui ajoute une facture à la table "Facture".
        /// </summary>
        /// <param name="facture"></param>
        public static void AddFacture(Facture facture)
        {
            using (MySqlConnection db =
            new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO facture (Num_Facture, Date_Facture, Montant_Total, FK_Id_Client) VALUES (@Num_Facture, @Date_Facture, @Montant_Total, @FK_Id_Client)";

                insertCommand.Parameters.AddWithValue("@Num_Facture", facture.Num_facture);
                insertCommand.Parameters.AddWithValue("@Date_Facture", facture.Date_facture);
                insertCommand.Parameters.AddWithValue("@Montant_Total", facture.Montant_total);
                insertCommand.Parameters.AddWithValue("@FK_Id_Client", facture.Fk_Id_Client);
                insertCommand.ExecuteReader();
            }
        }
        #endregion

        #region [BDD] Trouver le dernier numéro de facture
        /// <summary>
        /// Requête qui trouve le dernier numéro de facture et le stock si aucun numéro de facture trouver alors numéro de facture égal 0.
        /// </summary>
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
            }
            return resultat;
        }
        #endregion
    }
}
