using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;
using BoutiqueBDDLibrary;

namespace BoutiqueBDDLibrary
{
    public static class DataAccessJL
    {
        public const string CHEMINBDD = "SERVER=127.0.0.1; DATABASE=bdd_boutique; UID=root; PASSWORD=;";

        public static bool IsCorrectString80(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length > 79)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }

        public static bool IsCorrectString100(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length > 99)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }
        public static bool IsCorrectString50(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length > 51)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }
        public static bool IsCorrectString15(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length < 16)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }

        public static bool IsCorrectString5(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length < 6)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }

        public static bool IsCorrectString25(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length < 26)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }

        public static bool IsCorrectDecimal(string param)
        {

            if (typeof(decimal).IsAssignableFrom(param.GetType()))
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }


        #region Commentaire
        //public static void InitializeDatabase()
        //{
        //    using (MySqlConnection db =
        //        new MySqlConnection(CheminBDD))
        //    {
        //        db.Open();

        //        String tableCommand = "CREATE TABLE IF NOT " +
        //            "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
        //            "Text_Entry NVARCHAR(2048) NULL)";

        //        MySqlCommand createTable = new MySqlCommand(tableCommand, db);

        //        createTable.ExecuteReader();
        //    }
        //}
        //public static void AddProduct(Produit produit)
        //{
        //    using (MySqlConnection db =
        //        new MySqlConnection(CheminBDD1))
        //    {
        //        db.Open();
        //        MySqlCommand insertCommand = new MySqlCommand();
        //        insertCommand.Connection = db;

        //        // Use parameterized query to prevent SQL injection attacks
        //        insertCommand.CommandText = "INSERT INTO produit (Nom_Produit, TVA, Prix_Produit, Remise_Produit, Description_Produit, ValNutrition_Produit, FK_Id_Categorie, FK_Id_Origine, FK_Id_Unite) VALUES (@Nom_Produit, @TVA, @Prix_Produit, @Remise_Produit, @Description_Produit, @ValNutrition_Produit, @FK_Id_Categorie, @FK_Id_Origine, @FK_Id_Unite)";

        //        insertCommand.Parameters.AddWithValue("@Nom_Produit", produit.Nom_Produit);
        //        insertCommand.Parameters.AddWithValue("@TVA", produit.TVA);
        //        insertCommand.Parameters.AddWithValue("@Prix_Produit", produit.Prix_Produit);
        //        insertCommand.Parameters.AddWithValue("@Remise_Produit", produit.Remise_Produit);
        //        insertCommand.Parameters.AddWithValue("@Description_Produit", produit.Description_Produit);
        //        insertCommand.Parameters.AddWithValue("@ValNutrition_Produit", produit.Val_Nutrition_Produit);
        //        insertCommand.Parameters.AddWithValue("@FK_Id_Categorie", produit.FK_Id_Categorie);
        //        insertCommand.Parameters.AddWithValue("@FK_Id_Origine", produit.FK_Id_Origine);
        //        insertCommand.Parameters.AddWithValue("@FK_Id_Unite", produit.FK_Id_Unite);

        //        insertCommand.ExecuteReader();

        //        db.Close();
        //    }

        //}

        //public static List<Produit> GetAllProduits()
        //{
        //    List<Produit> entries = new List<Produit>();

        //    using (MySqlConnection db =
        //        new MySqlConnection(CheminBDD1))
        //    {
        //        db.Open();

        //        MySqlCommand selectCommand = new MySqlCommand
        //            ("SELECT * from produit", db);

        //        MySqlDataReader query = selectCommand.ExecuteReader();

        //        while (query.Read())
        //        {
        //            Produit produit = new Produit();
        //            produit.Id_Produit = query.GetInt32(0);
        //            produit.Nom_Produit = query.GetString(1);
        //            produit.TVA = query.GetDecimal(2);
        //            produit.Prix_Produit = query.GetDecimal(3);
        //            if (! DBNull.Value.Equals(query.GetValue(4)))
        //            {
        //                produit.Remise_Produit = query.GetUInt32(4);
        //            }
        //            else
        //            {
        //                produit.Remise_Produit = 0;
        //            }
        //            //if (!DBNull.Value.Equals(query.GetValue(5)))
        //            //{

        //            //    produit.Description_Produit = "";
        //            //}
        //            //else
        //            //{
        //            //    produit.Description_Produit = query.GetString(5);
        //            //}
        //            if (!DBNull.Value.Equals(query.GetValue(5)))
        //            {
        //                produit.Description_Produit = query.GetString(5);
        //            }
        //            else
        //            {
        //                produit.Description_Produit = "";
        //            }
        //            //produit.Description_Produit = query.GetString(5);
        //            produit.Val_Nutrition_Produit = query.GetInt32(6);
        //            produit.FK_Id_Categorie = query.GetInt32(7);
        //            produit.FK_Id_Origine = query.GetInt32(8);
        //            produit.FK_Id_Unite = query.GetInt32(9);
        //            entries.Add(produit);
        //        }

        //        db.Close();
        //    }

        //    return entries;
        //}
        //public static void DisplayProduct()
        //{
        //    List<Produit> produits = GetAllProduits();
        //    Console.WriteLine("Nous avons trouvé {0} produit(s) :", produits.Count);
        //    foreach (var produit in produits)
        //    {
        //        Console.WriteLine("ID = "+ produit.Id_Produit + "; Nom = " + produit.Nom_Produit + "; TVA = " + produit.TVA + "; Prix = " + produit.Prix_Produit + "; Remise = " + produit.Remise_Produit + "; Description = " + produit.Description_Produit + "; Valeur nutritionnelle = " + produit.Val_Nutrition_Produit + "; FK_Id_Categorie = " + produit.FK_Id_Categorie + "; FK_Id_Origine = " + produit.FK_Id_Origine + "; FK_Id_Unite = " + produit.FK_Id_Unite);
        //    }
        //    Console.WriteLine();
        //    Console.WriteLine();
        //}
        #endregion

    }
    public class MonMessageErreur : Exception
    {
        public string errorMessage;
        public MonMessageErreur(string message)
           : base(message)
        {
            errorMessage = message;
        }
    }
}

