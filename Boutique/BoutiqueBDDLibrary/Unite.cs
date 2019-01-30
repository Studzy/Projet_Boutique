using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class Unite
    {
        //Déclaration des variables
        private int id_Libelle;
        private string libelle_unite;

        //Constructeur
        public Unite()
        {
        }

        //Get;Set; Vérifications
        #region Id_Libelle
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI Id_LIBELLE
        /// </summary>
        public int Id_Libelle { get => id_Libelle; set => id_Libelle = value; }
        #endregion

        #region Libelle_Unite
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI lIBELLE8uNITE
        /// </summary>
        public string Libelle_unite { get => libelle_unite; set => libelle_unite = value; }
        #endregion

        //Base de données
        #region [BDD] Ajoute une nouvelle unité 
        /// <summary>
        /// Ajoute une unité à la base table "Unité".
        /// </summary>
        /// <param name="unite"></param>
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
            }

        }
        #endregion

        #region [BDD] Verifie si l'unité existe déjà
        /// <summary>
        /// Vérifie si l'unité existe alors on l'a stock et on retourne l'ID.
        /// Si existe déjà alors retourne juste IDTrouve.
        /// </summary>
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
        #endregion
    }
}
