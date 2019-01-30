using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class Categorie
    {
        //Déclaration de variables
        private int id_Categorie;
        private string nom_categorie;

        //Constructeur
        public Categorie()
        {
        }

        //Get;Set; Vérifications
        #region Id_Categorie
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI Id_Categorie
        /// </summary>
        public int Id_Categorie { get => id_Categorie; set => id_Categorie = value; }
        #endregion

        #region Nom_Categorie
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI Nom_Categorie
        /// </summary>
        public string Nom_categorie { get => nom_categorie; set => nom_categorie = value; }
        #endregion

        //Base de données
        #region [BDD] Ajouter une catégorie
        /// <summary>
        /// Requête SQL qui ajoute une catégorie à la table "Categorie".
        /// </summary>
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
            }
        }
        #endregion

        #region [BDD] Vérifie sur la catégorie existe
        /// <summary>
        /// Requête SQL qui vérifie si la catégorie existe si la catégorie existe.
        /// Si elle existe, stock l'Id dans une variable et le retourne.
        /// Sinon, retourne juste IdTrouve.
        /// </summary>
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
        #endregion
    }   
}
