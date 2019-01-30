using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class Commande
    {
        //Déclaration de base de données
        private int id_Commande;
        private int fK_Id_Facture;
        private int fK_Id_Produit;
        private int qtite_Produit;

        //Constructeur
        public Commande()
        {
        }

        //Get;Set; Vérifications
        #region Id_Commande
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI Id_Commande
        /// </summary>
        public int Id_Commande { get => id_Commande; set => id_Commande = value; }
        #endregion

        #region FK_Id_Facture
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI FK_Id_Facture
        /// </summary>
        public int FK_Id_Facture { get => fK_Id_Facture; set => fK_Id_Facture = value; }
        #endregion

        #region FK_Id_Produit
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI FK_Id_Produit
        /// </summary>
        public int FK_Id_Produit { get => fK_Id_Produit; set => fK_Id_Produit = value; }
        #endregion

        #region Qtite_Produit
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI Qtite_Produit
        /// </summary>
        public int Qtite_Produit { get => qtite_Produit; set => qtite_Produit = value; }
        #endregion

        //Base de données
        #region [BDD] Ajouter une commande
        /// <summary>
        /// Requête SQL qui ajoute une commande à la table "commande".
        /// </summary>
        public static void AddCommande(Commande commande)
        {
            using (MySqlConnection db =
            new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO commande (FK_Id_Facture, FK_Id_Produit, Qtite_Produit) VALUES (@FK_Id_Facture, @FK_Id_Produit, @Qtite_Produit)";

                insertCommand.Parameters.AddWithValue("@FK_Id_Facture", commande.FK_Id_Facture);
                insertCommand.Parameters.AddWithValue("@FK_Id_Produit", commande.FK_Id_Produit);
                insertCommand.Parameters.AddWithValue("@Qtite_Produit", commande.Qtite_Produit);
                insertCommand.ExecuteReader();
            }       
        }
        #endregion
    }
}
