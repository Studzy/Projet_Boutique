using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class Produit
    {
        //Déclaration des variables
        private int id_Produit;
        private string Nom_produit;
        private decimal tva;
        private decimal prix_produit;
        private decimal remise_produit;
        private string description_produit;
        private int val_nutrition_produit;
        private int fK_Id_Categorie;
        private int fK_Id_Origine;
        private int fK_Id_Unite;
        private string nom_categorie;
        private string nom_origine;
        private string libelle_unite;
        
        //Constructeur
        public Produit()
        {
        }

        //Get;Set; Vérifications
        #region Nom_Produit
        /// <summary>
        /// Vérifie si le nom du produit est un string qui comporte au maximum 50 caractères.
        /// </summary>
        public string Nom_Produit
        {
            get => Nom_produit;
            set
            {
                try
                {
                    if(!DataAccessJL.IsCorrectString50(value))
                    {
                        throw new MonMessageErreur("");
                    }
                    else
                    {
                        Nom_produit = value;
                    }
                }
                catch (MonMessageErreur)
                {
                    throw new  MonMessageErreur("Le nom n'est pas valable");
                }
            }
        }
        #endregion

        #region TVA
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI TVA
        /// </summary>
        public decimal TVA
        {
            get => tva;
            set
            {
                try
                {
                    if (typeof(decimal).IsAssignableFrom(value.GetType()))
                    {
                        tva = value;
                    }
                    else
                    {
                        throw new MonMessageErreur("");
                    }
                    
                }
                catch (MonMessageErreur)
                {
                    throw new MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region Prix_Produit
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI PRIX_PRODUIT
        /// </summary>
        public decimal Prix_Produit
        {
            get => prix_produit;
            set
            {
                try
                {
                    prix_produit = value;
                }
                catch (MonMessageErreur)
                {
                    throw new MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region Remise_Produit
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public decimal Remise_Produit
        {
            get => remise_produit;
            set
            {
                try
                {
                    remise_produit = value;
                }
                catch (MonMessageErreur)
                {
                    throw new MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region Description_Produit
        public string Description_Produit
        {
            get => description_produit;
            set
            {
                try
                {
                    description_produit = value;
                }
                catch (MonMessageErreur)
                {
                    throw new MonMessageErreur("La description n'est pas valable");
                }
            }
        }
        #endregion

        #region Val_Nutrition_Produit
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public int Val_Nutrition_Produit
        {
            get => val_nutrition_produit;
            set
            {
                try
                {
                    val_nutrition_produit = value;
                }
                catch (MonMessageErreur)
                {
                    throw new MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region FK_Id_Categorie
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public int FK_Id_Categorie
        {
            get => fK_Id_Categorie;
            set
            {
                try
                {
                    fK_Id_Categorie = value;
                }
                catch (MonMessageErreur)
                {
                    throw new MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region FK_Id_Origine
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public int FK_Id_Origine
        {
            get => fK_Id_Origine;
            set
            {
                try
                {
                    fK_Id_Origine = value;
                }
                catch (MonMessageErreur)
                {
                    throw new MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region  FK_Id_Unite
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public int FK_Id_Unite
        {
            get => fK_Id_Unite;
            set
            {
                try
                {
                    fK_Id_Unite = value;
                }
                catch (MonMessageErreur)
                {
                    throw new MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region Id_Produit
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public int Id_Produit
        {
            get => id_Produit;
            set
            {
                try
                {
                    id_Produit = value;
                }
                catch (MonMessageErreur)
                {
                    throw new MonMessageErreur("La valeur n'est pas valable");
                }
            }
        }
        #endregion

        #region Nom_Categorie
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public string Nom_categorie { get => nom_categorie; set => nom_categorie = value; }
        #endregion

        #region Nom_Origine
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public string Nom_origine { get => nom_origine; set => nom_origine = value; }
        #endregion

        #region Libelle_Unite
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI
        /// </summary>
        public string Libelle_unite { get => libelle_unite; set => libelle_unite = value; }
        #endregion

        #region AddProduct
        public static void AddProduct(Produit produit)
        {
            using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO produit (Nom_Produit, TVA, Prix_Produit,  Description_Produit, ValNutrition_Produit, FK_Id_Categorie, FK_Id_Origine, FK_Id_Unite, Remise_Produit) VALUES (@Nom_Produit, @TVA, @Prix_Produit, @Description_Produit, @ValNutrition_Produit, @FK_Id_Categorie, @FK_Id_Origine, @FK_Id_Unite , @Remise_Produit)";

                insertCommand.Parameters.AddWithValue("@Nom_Produit", produit.Nom_Produit);
                insertCommand.Parameters.AddWithValue("@TVA", produit.TVA);
                insertCommand.Parameters.AddWithValue("@Prix_Produit", produit.Prix_Produit);
                insertCommand.Parameters.AddWithValue("@Remise_Produit", produit.Remise_Produit);
                insertCommand.Parameters.AddWithValue("@Description_Produit", produit.Description_Produit);
                insertCommand.Parameters.AddWithValue("@ValNutrition_Produit", produit.Val_Nutrition_Produit);
                insertCommand.Parameters.AddWithValue("@FK_Id_Categorie", produit.FK_Id_Categorie);
                insertCommand.Parameters.AddWithValue("@FK_Id_Origine", produit.FK_Id_Origine);
                insertCommand.Parameters.AddWithValue("@FK_Id_Unite", produit.FK_Id_Unite);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }
        #endregion

        //Base de données
        #region [BDD] Affiche tout les produits
        /// <summary>
        /// Affiche tout les produits présent dans la base de données
        /// </summary>
        /// <returns></returns>
        public static List<Produit> GetAllProducts()
        {
            List<Produit> entries = new List<Produit>();
            using (MySqlConnection db =
            new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();

                MySqlCommand selectCommand = new MySqlCommand
                    ("SELECT * from produit", db);

                MySqlDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    Produit produit = new Produit();
                    produit.Id_Produit = query.GetInt32(0);
                    produit.Nom_Produit = query.GetString(1);
                    produit.TVA = query.GetDecimal(2);
                    produit.Prix_Produit = query.GetDecimal(3);
                    if (!DBNull.Value.Equals(query.GetValue(4)))
                    {
                        produit.Remise_Produit = query.GetUInt32(4);
                    }
                    else
                    {
                        produit.Remise_Produit = 0;
                    }
                    if (!DBNull.Value.Equals(query.GetValue(5)))
                    {
                        produit.Description_Produit = query.GetString(5);
                    }
                    else
                    {
                        produit.Description_Produit = "";
                    }
                    //produit.Description_Produit = query.GetString(5);
                    produit.Val_Nutrition_Produit = query.GetInt32(6);
                    produit.FK_Id_Categorie = query.GetInt32(7);
                    produit.FK_Id_Origine = query.GetInt32(8);
                    produit.FK_Id_Unite = query.GetInt32(9);
                    entries.Add(produit);
                }
            }
            return entries;
        }
        #endregion

        #region [BDD] Affiche 10 produits
        /// <summary>
        /// Affiche 10 produits à partir du @start et de la catégorie @order.
        /// </summary>
        public static List<Produit> Get10Products(int start, string group)
        {
            //List<> entries = new List<String>();
            List<Produit> entries = new List<Produit>();

            using (MySqlConnection db =
            new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();

                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT Id_Produit, Nom_Produit, Nom_Categorie, Nom_Origine, Prix_Produit, Libelle_Unite, Description_Produit, ValNutrition_Produit FROM produit INNER JOIN origine ON origine.Id_Origine = produit.FK_Id_Origine INNER JOIN unite ON unite.Id_Unite = produit.FK_Id_Unite INNER JOIN categorie ON categorie.Id_Categorie = produit.FK_Id_Categorie ORDER BY @order LIMIT 5 OFFSET @start ;";

                insertCommand.Parameters.AddWithValue("@start", start);
                insertCommand.Parameters.AddWithValue("@order", group);
                MySqlDataReader query = insertCommand.ExecuteReader();

                while (query.Read())
                {
                    Produit produit = new Produit();
                    produit.Id_Produit = query.GetInt32(0);
                    produit.Nom_Produit = query.GetString(1);
                    produit.Nom_categorie = query.GetString(2);
                    produit.Nom_origine = query.GetString(3);
                    produit.Prix_Produit = query.GetDecimal(4);
                    produit.Libelle_unite = query.GetString(5);
                    if (!DBNull.Value.Equals(query.GetValue(6)))
                    {
                        produit.Description_Produit = query.GetString(6);
                    }
                    else
                    {
                        produit.Description_Produit = "";
                    }
                    //produit.Description_Produit = query.GetString(5);
                    produit.Val_Nutrition_Produit = query.GetInt32(7);
                    entries.Add(produit);
                }
            }
            return entries;
        }
        #endregion

        #region [BDD] Supprimer un produit
        /// <summary>
        /// Supprime un produit de la base de données
        /// </summary>
        public static void DeleteOneProduct(string nom)
        {
                using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;

                    //Requête qui protège des attaques SQL
                    insertCommand.CommandText = "DELETE FROM produit WHERE Nom_Produit = @Nom_Produit ";

                    insertCommand.Parameters.AddWithValue("@Nom_Produit", nom);
                    insertCommand.ExecuteReader();
                }
        }
        #endregion

        #region [BDD] Modifie un produit
        /// <summary>
        /// Modifie un produit dans la base de données @nom est le nom du produit à modifié et "Produit p" est l'objet produit avec toute ses informations. 
        /// </summary>
        public static void ModifyOneProduct(string nom, Produit p)
        {
                using (MySqlConnection db =
                new MySqlConnection(DataAccessJL.CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "UPDATE produit SET Nom_Produit = @Nom_Produit , TVA = @TVA, Prix_Produit = @Prix_Produit,  Description_Produit = @Description_Produit, ValNutrition_Produit = @ValNutrition_Produit, FK_Id_Categorie = @FK_Id_Categorie, FK_Id_Origine = @FK_Id_Origine, FK_Id_Unite = @FK_Id_Unite, Remise_Produit = @Remise_Produit WHERE Nom_Produit = @nom ";

                    insertCommand.Parameters.AddWithValue("@Nom_Produit", p.Nom_Produit);
                    insertCommand.Parameters.AddWithValue("@TVA", p.TVA);
                    insertCommand.Parameters.AddWithValue("@Prix_Produit", p.Prix_Produit);
                    insertCommand.Parameters.AddWithValue("@Remise_Produit", p.Remise_Produit);
                    insertCommand.Parameters.AddWithValue("@Description_Produit", p.Description_Produit);
                    insertCommand.Parameters.AddWithValue("@ValNutrition_Produit", p.Val_Nutrition_Produit);
                    insertCommand.Parameters.AddWithValue("@FK_Id_Categorie", p.FK_Id_Categorie);
                    insertCommand.Parameters.AddWithValue("@FK_Id_Origine", p.FK_Id_Origine);
                    insertCommand.Parameters.AddWithValue("@FK_Id_Unite", p.FK_Id_Unite);
                    insertCommand.Parameters.AddWithValue("@nom", nom);
                    insertCommand.ExecuteReader();
                }
        }
        #endregion

        #region [BDD] Afficher un produit par son nom
        /// <summary>
        /// Affiche un produit par son nom, @nom correspond au nom du produit à afficher.
        /// </summary>
        public static Produit GetOneProduct(string Nom)
        {
            Produit p = new Produit();

            using (MySqlConnection db =
            new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();
                
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM produit WHERE Nom_Produit = @nom";
                insertCommand.Parameters.AddWithValue("@nom", Nom);

                MySqlDataReader query = insertCommand.ExecuteReader();

                if (query.Read())
                {
                    
                        p.Id_Produit = query.GetInt32(0);
                        p.Nom_Produit = query.GetString(1);
                        p.TVA = query.GetDecimal(2);
                        p.Prix_Produit = query.GetDecimal(3);
                        if (!DBNull.Value.Equals(query.GetValue(4)))
                        {
                            p.Remise_Produit = query.GetUInt32(4);
                        }
                        else
                        {
                            p.Remise_Produit = 0;
                        }
                        if (!DBNull.Value.Equals(query.GetValue(5)))
                        {
                            p.Description_Produit = query.GetString(5);
                        }
                        else
                        {
                            p.Description_Produit = "";
                        }
                        //produit.Description_Produit = query.GetString(5);
                        p.Val_Nutrition_Produit = query.GetInt32(6);
                        p.FK_Id_Categorie = query.GetInt32(7);
                        p.FK_Id_Origine = query.GetInt32(8);
                        p.FK_Id_Unite = query.GetInt32(9);
                }
                else
                {
                    p.Nom_produit = "Rien";
                }
            }
            return p;
        }
        #endregion

        #region [BDD] Affiche un produit par son ID
        /// <summary>
        /// Affiche un produit en fonction de son ID, @id correspond à l'id du produit que l'on recherche.
        /// </summary>
        public static Produit GetOneProductById(int Id)
        {
            Produit p = new Produit();

            using (MySqlConnection db =
            new MySqlConnection(DataAccessJL.CHEMINBDD))
            {
                db.Open();

                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM produit WHERE Id_Produit = @id";
                insertCommand.Parameters.AddWithValue("@id", Id);

                MySqlDataReader query = insertCommand.ExecuteReader();

                if (query.Read())
                {

                    p.Id_Produit = query.GetInt32(0);
                    p.Nom_Produit = query.GetString(1);
                    p.TVA = query.GetDecimal(2);
                    p.Prix_Produit = query.GetDecimal(3);
                    if (!DBNull.Value.Equals(query.GetValue(4)))
                    {
                        p.Remise_Produit = query.GetUInt32(4);
                    }
                    else
                    {
                        p.Remise_Produit = 0;
                    }
                    if (!DBNull.Value.Equals(query.GetValue(5)))
                    {
                        p.Description_Produit = query.GetString(5);
                    }
                    else
                    {
                        p.Description_Produit = "";
                    }
                    //produit.Description_Produit = query.GetString(5);
                    p.Val_Nutrition_Produit = query.GetInt32(6);
                    p.FK_Id_Categorie = query.GetInt32(7);
                    p.FK_Id_Origine = query.GetInt32(8);
                    p.FK_Id_Unite = query.GetInt32(9);
                }
                else
                {
                    p.Nom_produit = "Rien";
                }
            }
            return p;
        }
        #endregion

        #region Commentaires A SUPPRIMER USELESS  <-- JEREMY
        //public static void DisplayProduct()
        //{
        //    List<Produit> produits = GetAllProducts();
        //    Console.WriteLine("Nous avons trouvé {0} produit(s) :", produits.Count);
        //    foreach (var produit in produits)
        //    {
        //        Console.WriteLine("ID = " + produit.Id_Produit + "; Nom = " + produit.Nom_Produit + "; TVA = " + produit.TVA + "; Prix = " + produit.Prix_Produit + "; Remise = " + produit.Remise_Produit + "; Description = " + produit.Description_Produit + "; Valeur nutritionnelle = " + produit.Val_Nutrition_Produit + "; FK_Id_Categorie = " + produit.FK_Id_Categorie + "; FK_Id_Origine = " + produit.FK_Id_Origine + "; FK_Id_Unite = " + produit.FK_Id_Unite);
        //    }
        //    Console.WriteLine();
        //    Console.WriteLine();
        //}
        //public static void DisplayOneProduct(string nom)
        //{
        //    Produit produit = GetOneProduct(nom);

        //    if (produit.Nom_produit != "Rien")
        //    {
        //        Console.WriteLine("Nous avons trouvé votre produit : ");

        //            Console.WriteLine("ID = " + produit.Id_Produit + "; Nom = " + produit.Nom_Produit + "; TVA = " + produit.TVA + "; Prix = " + produit.Prix_Produit + "; Remise = " + produit.Remise_Produit + "; Description = " + produit.Description_Produit + "; Valeur nutritionnelle = " + produit.Val_Nutrition_Produit + "; FK_Id_Categorie = " + produit.FK_Id_Categorie + "; FK_Id_Origine = " + produit.FK_Id_Origine + "; FK_Id_Unite = " + produit.FK_Id_Unite);

        //        Console.WriteLine();
        //        Console.WriteLine();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Nous n'avons pas trouvé votre produit .");
        //    }

        //}
        #endregion
    }
}
