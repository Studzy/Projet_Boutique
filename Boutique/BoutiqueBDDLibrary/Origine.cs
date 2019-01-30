using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class Origine
    {
        //Déclaration des variables
        private int id_Origine;
        private string nom_Origine;

        //Constructeur
        public Origine()
        {
        }

        //Get;Set; Vérifications
        #region Origine
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public Origine(int id_Origine, string nom_Origine)
        {
            Id_Origine = id_Origine;
            Nom_Origine = nom_Origine;
        }
        #endregion

        #region Id_Origine
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public int Id_Origine
        {
            get => id_Origine;
            set => id_Origine = value;
        }
        #endregion

        #region Nom_Origine
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public string Nom_Origine { get => nom_Origine; set => nom_Origine = value.ToUpper(); }
        #endregion


        //Base de données
        #region [BDD] Ajouter une origine
        /// <summary>
        /// Ajoute une origine à la table "Origine" de la base de données. 
        /// </summary>
        public static void AddOrigine(Origine origine)
        {
            using (MySqlConnection db =
            new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO origine (Nom_Origine) VALUES (@Nom_Origine)";

                insertCommand.Parameters.AddWithValue("@Nom_Origine", origine.Nom_Origine);
                insertCommand.ExecuteReader();
            }
        }
        #endregion

        #region [BDD] Vérifie si l'origine existe
        /// <summary>
        /// Vérifie si l'origine existe dans la base de données.
        /// Si il existe stock prend l'id et la stock dans une variable.
        /// Si il existe pas renvoi juste IdTrouve.
        /// </summary>
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
        #endregion

    }
}
