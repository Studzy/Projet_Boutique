using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace BoutiqueBDDLibrary
{
    public class DataAccess
    {
        //Chemin vers la base de données
        #region [BDD] Chemin vers Base de données
        /// <summary>
        /// Chemin d'accès à la base de données (MySql PhpmyAdmin).
        /// </summary>
        public const string CHEMINBDD = "SERVER=127.0.0.1; DATABASE=bdd_boutique; UID=root; PASSWORD=;";
        #endregion

        //[QUENTIN] - PARTIE CLIENTS (Tables: Client|Admin|CpVille|)
        //Clients
        #region [BDD] Ajouter un Client
        /// <summary>
        /// Insère un client dans la base de données, dans la table "Client" à l'aide de l'objet Client.
        /// </summary>
        /// <param name="x"></param>
        public static void InsererClientEnBDD(Client x)
        {
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand firstInsert =
                        new MySqlCommand("INSERT INTO Client (Nom_Client, Prenom_Client, Adresse_Client, Mail_Client, NumTel_Client, Date_Naissance_Client, MDP_Client, FK_Id_CPVille) VALUES (@nom, @prenom, @adresse, @email, @telephone, @datenaissance, SHA2(@motdepasse,256), @idcpville)", db);
                    var nomParameter = new MySqlParameter("@nom", x.Nom_Client);
                    var prenomParameter = new MySqlParameter("@prenom", x.Prenom_client);
                    var adresseParameter = new MySqlParameter("@adresse", x.Adresse_client);
                    var emailParameter = new MySqlParameter("@email", x.Mail_client);
                    var telephoneParameter = new MySqlParameter("@telephone", x.Numtel_Client);
                    var datenaissanceParameter = new MySqlParameter("@datenaissance", x.Date_naissance_client);
                    var motdepasseParameter = new MySqlParameter("@motdepasse", x.Mdp_client);
                    var idcpvilleParameter = new MySqlParameter("@idcpville", x.Id_CpVille);
                    firstInsert.Parameters.Add(nomParameter);
                    firstInsert.Parameters.Add(prenomParameter);
                    firstInsert.Parameters.Add(adresseParameter);
                    firstInsert.Parameters.Add(emailParameter);
                    firstInsert.Parameters.Add(telephoneParameter);
                    firstInsert.Parameters.Add(datenaissanceParameter);
                    firstInsert.Parameters.Add(motdepasseParameter);
                    firstInsert.Parameters.Add(idcpvilleParameter);
                    firstInsert.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        #region [BDD] Modifier un client
        /// <summary>
        /// Modifie un client dans la base de données.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="x"></param>
        public static void modifieUnClientenBDD(string email, Client x)
        {
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;

                    //Format qui protège des SQL Injections.
                    insertCommand.CommandText = "UPDATE client SET Nom_Client = @Nom_Client , Prenom_Client = @Prenom_Client, Adresse_Client = @Adresse_Client, NumTel_Client = @Numtel_Client, Date_Naissance_Client = @Date_Naissance_Client, MDP_Client = SHA2(@MDP_Client,256), FK_Id_CPVille = @FK_Id_CpVille WHERE Mail_Client = @email ";

                    insertCommand.Parameters.AddWithValue("@Nom_Client", x.Nom_Client);
                    insertCommand.Parameters.AddWithValue("@Prenom_Client", x.Prenom_client);
                    insertCommand.Parameters.AddWithValue("@Adresse_Client", x.Adresse_client);
                    insertCommand.Parameters.AddWithValue("@NumTel_Client", x.Numtel_Client);
                    insertCommand.Parameters.AddWithValue("@MDP_Client", x.Mdp_client);
                    insertCommand.Parameters.AddWithValue("@Date_Naissance_Client", x.Date_naissance_client);
                    insertCommand.Parameters.AddWithValue("@FK_Id_CpVille", x.Id_CpVille);
                    insertCommand.Parameters.AddWithValue("@email", email);
                    insertCommand.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        // 5 requêtes pour supprimer un client de la base de données.
        #region [BDD] 1- Récupère l'ID facture d'un client
        /// <summary>
        /// Recupère l'ID_Facture
        /// S'il trouve l'ID stocke les ID sinon renvoie juste 0.
        /// </summary>
        public static List<int> recupeIdFacture(string email)
        {
            List<int> IdMultiple = new List<int>();

            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "SELECT facture.Id_Facture, commande.Id_Commande FROM client INNER JOIN facture ON facture.FK_Id_Client = client.Id_Client INNER JOIN commande ON commande.FK_Id_Facture = facture.Id_Facture WHERE Mail_Client = @email";

                    insertCommand.Parameters.AddWithValue("@email", email);
                    MySqlDataReader query = insertCommand.ExecuteReader();

                    if (query.Read())
                    {
                        IdMultiple.Add(query.GetInt32(0));//Facture
                    }
                    else
                    {
                        IdMultiple.Add(0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return IdMultiple;

        }
        #endregion

        #region [BDD] 2- Supprime une commande d'un client
        /// <summary>
        /// Supprime une commande à partir de FK_Id_Facture.
        /// </summary>
        public static void supprimerUneCommande(int FK_Id_Facture)
        {
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "DELETE FROM commande WHERE commande.FK_Id_Facture = @idfacture";
                    insertCommand.Parameters.AddWithValue("@idfacture", FK_Id_Facture);
                    insertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        #region [BDD] 3- Supprime Le lien Inter_Facture_Paiement
        /// <summary>
        /// Supprime la ligne qui est dans la table Inter_Facture_Paiement qui selectionne le moyen de paiement.
        /// </summary>
        public static void supprimerInterFacturePaiement(int Id_Facture)
        {
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "DELETE FROM inter_facture_paiement WHERE inter_facture_paiement.FK_Id_Facture = @idfacture";
                    insertCommand.Parameters.AddWithValue("@idfacture", Id_Facture);
                    insertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        #region [BDD] 4- Supprime une facture d'un client
        /// <summary>
        /// Supprime une facture à partir de l'ID d'une facture.
        /// </summary>
        public static void supprimerUneFacture(int Id_Facture)
        {
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "DELETE FROM facture WHERE facture.Id_Facture = @idfacture";
                    insertCommand.Parameters.AddWithValue("@idfacture", Id_Facture);
                    insertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        #region [BDD] 5- Supprime un client 
        /// <summary>
        /// Supprime un client dans la base de données.
        /// </summary>
        /// <param name="email"></param>
        public static void supprimerUnClient(string email)
        {
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "DELETE FROM client WHERE Mail_Client = @email ";
                    insertCommand.Parameters.AddWithValue("@email", email);
                    insertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
        #endregion


        #region [BDD] Affiche un client
        /// <summary>
        /// Vérifie si l'email existe dans la base de données,
        /// S'il existe renvoie l'objet client, sinon change l'objet client.Mail_Client à "Rien".
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Client verifieSiClientExiste(string email)
        {
            Client z = new Client();
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "SELECT * FROM client INNER JOIN cp_ville ON cp_ville.Id_CPVille=client.FK_Id_CPVille WHERE Mail_Client= @email";
                    insertCommand.Parameters.AddWithValue("@email", email);

                    MySqlDataReader query = insertCommand.ExecuteReader();

                    if (query.Read())
                    {
                        z.Nom_Client = query.GetString(1);
                        z.Prenom_client = query.GetString(2);
                        z.Adresse_client = query.GetString(3);
                        z.Mail_client = query.GetString(4);
                        z.Numtel_Client = query.GetString(5);
                        z.Date_naissance_client = query.GetDateTime(6);
                        z.Id_CpVille = query.GetInt32(8);
                        z.codePostal_Ville = query.GetString(10);
                        z.nom_Ville = query.GetString(11);
                    }
                    else
                    {
                        z.Mail_client = "Rien";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

            return z;
        }
        #endregion

        #region [BDD] Affiche ID et Email Client
        /// <summary>
        /// Vérifie si l'email existe dans la base de données,
        /// S'il existe renvoie l'objet client, sinon change l'objet client.Mail_Client à "Rien".
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Client AfficheIDEmailClient(string email)
        {
            Client z = new Client();
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "SELECT Id_Client, Mail_Client FROM client WHERE Mail_Client= @email";
                    insertCommand.Parameters.AddWithValue("@email", email);

                    MySqlDataReader query = insertCommand.ExecuteReader();

                    if (query.Read())
                    {
                        z.Mail_client = query.GetString(1);
                        z.Id_client = query.GetInt32(0);
                    }
                    else
                    {
                        z.Mail_client = "Rien";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

            return z;
        }
        #endregion

        #region [BDD] Afficher tous les clients
        /// <summary>
        /// Affiche tous les clients trouvés dans la base de données, retourne sous forme de liste.
        /// </summary>
        /// <returns></returns>
        public static List<Client> GetAllClients()
        {
            List<Client> entries = new List<Client>();
            try
            {
                using (MySqlConnection db = new MySqlConnection(DataAccess.CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand selectCommand = new MySqlCommand
                        ("SELECT * FROM client INNER JOIN cp_ville ON cp_ville.Id_CPVille = client.FK_Id_CPVille", db);

                    MySqlDataReader query = selectCommand.ExecuteReader();

                    while (query.Read())
                    {
                        Client client = new Client();
                        client.Nom_Client = query.GetString(1);
                        client.Prenom_client = query.GetString(2);
                        client.Adresse_client = query.GetString(3);
                        client.Mail_client = query.GetString(4);
                        client.Numtel_Client = query.GetString(5);
                        client.Date_naissance_client = query.GetDateTime(6);
                        client.codePostal_Ville = query.GetString(10);
                        client.nom_Ville = query.GetString(11);
                        entries.Add(client);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

            return entries;
        }
        #endregion

        #region [BDD] Vérifie Si l'Email Client existe
        /// <summary>
        /// Vérifie si l'émail client est présent dans la base de données, si présent retourne "true" sinon "false".
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static bool VerifierExistanceMailClient(string mail)
        {
            bool resultat = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CHEMINBDD))
                {
                    connection.Open();
                    MySqlCommand firstInsert = connection.CreateCommand();
                    {
                        firstInsert.CommandText = "SELECT Mail_Client FROM Client WHERE Mail_Client = @mail";
                        firstInsert.Parameters.AddWithValue("@mail", mail);
                        MySqlDataReader reader = firstInsert.ExecuteReader();
                        {
                            if (reader.Read() == true)
                            {
                                resultat = true;
                            }
                            else
                            {
                                resultat = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return resultat;
        }
        #endregion

        #region [BDD] Vérifier MDP = Email pour  Client
        /// <summary>
        /// Vérifie que l'émail client est compatible avec le mot de passe.
        /// Si compatible retourne "true" sinon "false".
        /// </summary>
        /// <param name="email"></param>
        /// <param name="motdepasse"></param>
        /// <returns></returns>
        public static bool VerifieMailMdpClient(string email, string motdepasse)
        {
            bool resultat = false;
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand selectCommand = new MySqlCommand
                        ("SELECT Mail_Client, MDP_Client FROM client WHERE Mail_Client = @email AND MDP_Client = SHA2(@motdepasse,256)", db);
                    selectCommand.Parameters.AddWithValue("@email", email);
                    selectCommand.Parameters.AddWithValue("@motdepasse", motdepasse);

                    MySqlDataReader query = selectCommand.ExecuteReader();

                    if (query.Read())
                    {
                        resultat = true;
                    }
                    else
                    {
                        resultat = false;
                    }
                    return resultat;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return resultat;
        }
        #endregion

        //Admin
        #region [BDD] Ajoute un Admin
        /// <summary>
        /// Insère un administrateur dans la base de donnée, dans la table "Admin" à l'aide de l'objet Admin.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="motdepasse"></param>
        public static void InsererAdministrateurEnBDD(Admin x)
        {
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    MySqlCommand firstInsert =
                    new MySqlCommand("INSERT INTO admin (Mail_Admin, MDP_Admin) VALUES (@email,SHA2(@motdepasse,256))", db);

                    db.Open();
                    var emailParameter = new MySqlParameter("@email", x.Mail_Admin);
                    var motdepasseParameter = new MySqlParameter("@motdepasse", x.Mot_De_Passe);
                    firstInsert.Parameters.Add(emailParameter);
                    firstInsert.Parameters.Add(motdepasseParameter);
                    firstInsert.ExecuteNonQuery();
                }
                Console.Clear();
                Console.WriteLine("L'inscription s'est déroulée avec succès !\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        #region [BDD] Vérifie Si l'Email Admin existe
        /// <summary>
        /// Vérifie si l'émail administrateur est présent dans la base de données.
        /// Si présent retourne "true" sinon "false".
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static bool VerifierExistanceMailAdmin(string mail)
        {
            bool resultat = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CHEMINBDD))
                {
                    connection.Open();
                    using (MySqlCommand firstInsert = connection.CreateCommand())
                    {
                        firstInsert.CommandText = "SELECT Mail_Admin FROM Admin WHERE Mail_Admin = @mail";
                        firstInsert.Parameters.AddWithValue("@mail", mail);
                        using (MySqlDataReader reader = firstInsert.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                resultat = true;
                            }
                            else
                            {
                                resultat = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return resultat;

        }
        #endregion

        #region [BDD] Vérifier MDP = Email pour  Admin
        /// <summary>
        /// Vérifie que l'émail admin est compatible avec le mot de passe.
        /// Si compatible retourne "true" sinon "false".
        /// </summary>
        /// <param name="email"></param>
        /// <param name="motdepasse"></param>
        /// <returns></returns>
        public static bool VerifieMailMdpAdministrateur(string email, string motdepasse)
        {
            bool resultat = false;
            try
            {
                using (MySqlConnection db = new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand selectCommand = new MySqlCommand
                        ("SELECT Mail_Admin, MDP_Admin FROM admin WHERE Mail_Admin = @email AND MDP_Admin = SHA2(@motdepasse,256)", db);
                    selectCommand.Parameters.AddWithValue("@email", email);
                    selectCommand.Parameters.AddWithValue("@motdepasse", motdepasse);

                    MySqlDataReader query = selectCommand.ExecuteReader();

                    if (query.Read())
                    {
                        resultat = true;
                    }
                    else
                    {
                        resultat = false;
                    }
                    return resultat;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return resultat;
        }
        #endregion

        //Admin + Client
        #region [BDD] Ajouter Ville
        /// <summary>
        /// Ajoute une ville dans la table cp_ville dans la base de données.
        /// </summary>
        /// <param name="codepostal"></param>
        /// <param name="nomville"></param>
        public static void InsererVilleEnBDD(string codepostal, string nomville)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(CHEMINBDD);
                connection.Open();

                MySqlCommand firstInsert =
                    new MySqlCommand("INSERT INTO cp_ville (CodePostal_Ville, Nom_Ville) VALUES (@codepostal, @nomville)", connection);
                var codepostalParameter = new MySqlParameter("@codepostal", codepostal);
                var nomvilleParameter = new MySqlParameter("@nomville", nomville);
                firstInsert.Parameters.Add(codepostalParameter);
                firstInsert.Parameters.Add(nomvilleParameter);
                firstInsert.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        #region [BDD] Vérifie si la ville existe
        /// <summary>
        /// Vérifie si le client existe dans la base de données.
        /// </summary>
        /// <param name="Ville"></param>
        /// <returns></returns>
        public static FonctionsConsole.IdTrouve VerificationVille(string Ville)
        {
            FonctionsConsole.IdTrouve resultat = new FonctionsConsole.IdTrouve();
            try
            {
                string nomVille = Ville.ToUpper();
                using (MySqlConnection db =
                    new MySqlConnection(CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand selectCommand = new MySqlCommand
                        ("SELECT Id_CPVille, Nom_Ville FROM cp_ville WHERE Nom_Ville = @Ville", db);
                    selectCommand.Parameters.AddWithValue("@Ville", Ville);

                    MySqlDataReader query = selectCommand.ExecuteReader();

                    if (query.Read())
                    {
                        int idVille = (int)query["Id_CPVille"];
                        resultat = new FonctionsConsole.IdTrouve(idVille);
                    }
                    else
                    {
                        resultat = new FonctionsConsole.IdTrouve();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return resultat;
        }
        #endregion

        //[JEREMY] - PARTIE PRODUITS (Tables: Produit|Categorie|Commande|Facture|Inter_Facture_Paiement|Origine|Unite|)
        //Produit
        #region [BDD] Ajoute un produit
        /// <summary>
        /// Ajoute un produit à la table "produit".
        /// </summary>
        /// <param name="produit"></param>
        public static void AddProduct(Produit produit)
        {
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;

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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
        #endregion

        #region [BDD] Affiche tous les produits
        /// <summary>
        /// Affiche tous les produits présents dans la base de données
        /// </summary>
        /// <returns></returns>
        public static List<Produit> GetAllProducts()
        {
            List<Produit> entries = new List<Produit>();
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand selectCommand = new MySqlCommand
                        ("SELECT Id_Produit, Nom_Produit, TVA, Nom_Categorie, Nom_Origine, Prix_Produit, Libelle_Unite, Description_Produit, Remise_Produit, ValNutrition_Produit FROM produit INNER JOIN origine ON origine.Id_Origine = produit.FK_Id_Origine INNER JOIN unite ON unite.Id_Unite = produit.FK_Id_Unite INNER JOIN categorie ON categorie.Id_Categorie = produit.FK_Id_Categorie", db);

                    MySqlDataReader query = selectCommand.ExecuteReader();

                    while (query.Read())
                    {
                        Produit produit = new Produit();
                        produit.Id_Produit = query.GetInt32(0);
                        produit.Nom_Produit = query.GetString(1);
                        produit.TVA = query.GetInt32(2);
                        produit.Nom_categorie = query.GetString(3);
                        produit.Nom_origine = query.GetString(4);
                        produit.Prix_Produit = query.GetDecimal(5);
                        produit.Libelle_unite = query.GetString(6);
                        if (!DBNull.Value.Equals(query.GetValue(7)))
                        {
                            produit.Description_Produit = query.GetString(7);
                        }
                        else
                        {
                            produit.Description_Produit = "";
                        }
                        if (!DBNull.Value.Equals(query.GetValue(8)))
                        {
                            produit.Remise_Produit = query.GetDecimal(8);
                        }
                        else
                        {
                            produit.Remise_Produit = 0;
                        }
                        produit.Val_Nutrition_Produit = query.GetInt32(9);
                        entries.Add(produit);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return entries;
        }
        #endregion

        #region [BDD] Affiche un nombre limité de produits et trie
        /// <summary>
        /// Affiche un nombre de produits limité par la valeur 'limit' à partir du 'start' et trie par 'group'.
        /// </summary
        public static List<Produit> GetLimitProducts(int start, string group, int limit)
        {
            List<Produit> entries = new List<Produit>();
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "SELECT Id_Produit, Nom_Produit, Nom_Categorie, Nom_Origine, Prix_Produit, Libelle_Unite, Description_Produit, ValNutrition_Produit FROM produit INNER JOIN origine ON origine.Id_Origine = produit.FK_Id_Origine INNER JOIN unite ON unite.Id_Unite = produit.FK_Id_Unite INNER JOIN categorie ON categorie.Id_Categorie = produit.FK_Id_Categorie LIMIT @limit OFFSET @start;";
                    //insertCommand.Parameters.AddWithValue("@order", group);
                    insertCommand.Parameters.AddWithValue("@start", start);
                    insertCommand.Parameters.AddWithValue("@limit", limit);

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
                        produit.Val_Nutrition_Produit = query.GetInt32(7);
                        entries.Add(produit);
                    }
                }

                // Condition qui permettent de trier en fonction de la variable group, par defaut les produits sont trier par Ordre alphabetique
                if (group == "Prix_Produit")
                {
                    entries = entries.OrderBy(x => x.Prix_Produit).ToList();
                }
                else if (group == "Nom_Categorie")
                {
                    entries = entries.OrderBy(x => x.Nom_categorie).ToList();
                }
                else
                {
                    entries = entries.OrderBy(x => x.Nom_Produit).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return entries;
        }
        #endregion

        #region [BDD] Modifie un produit
        /// <summary>
        /// Modifie un produit dans la base de données à l'aide du nom du produit (@nom) à modifier et "Produit p" est l'objet produit avec toutes ses informations. 
        /// </summary>
        public static void ModifyOneProduct(string nom, Produit p)
        {
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        #region [BDD] Afficher un produit par son nom
        /// <summary>
        /// Affiche un produit en fonction de son nom.
        /// Retourne un Produit p si le produit a été trouvé sinon retourne un Nom.produit = "Rien"
        /// </summary>
        public static Produit GetOneProduct(string Nom)
        {
            Produit p = new Produit();
            p.Nom_Produit = "Rien";
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
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
                        p.Val_Nutrition_Produit = query.GetInt32(6);
                        p.FK_Id_Categorie = query.GetInt32(7);
                        p.FK_Id_Origine = query.GetInt32(8);
                        p.FK_Id_Unite = query.GetInt32(9);
                    }
                    else
                    {
                        p.Nom_Produit = "Rien";
                        return p;
                    }
                }
                
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
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
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
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
                        p.Val_Nutrition_Produit = query.GetInt32(6);
                        p.FK_Id_Categorie = query.GetInt32(7);
                        p.FK_Id_Origine = query.GetInt32(8);
                        p.FK_Id_Unite = query.GetInt32(9);
                    }
                    else
                    {
                        p.Nom_Produit = "Rien";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return p;
        }
        #endregion

        #region [BDD] Affiche les produits triés par nom
        /// <summary>
        /// Renvoie une liste de produits en fonction du nom
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<Produit> GetAllProductsByName(int start, int limit)
        {
            List<Produit> entries = new List<Produit>();
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "SELECT Id_Produit, Nom_Produit, Nom_Categorie, Nom_Origine, Prix_Produit, Libelle_Unite, Description_Produit, ValNutrition_Produit FROM produit INNER JOIN origine ON origine.Id_Origine = produit.FK_Id_Origine INNER JOIN unite ON unite.Id_Unite = produit.FK_Id_Unite INNER JOIN categorie ON categorie.Id_Categorie = produit.FK_Id_Categorie ORDER BY @order LIMIT @limit OFFSET @start ;";

                    insertCommand.Parameters.AddWithValue("@start", start);
                    insertCommand.Parameters.AddWithValue("@limit", limit);

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
                        produit.Val_Nutrition_Produit = query.GetInt32(7);
                        entries.Add(produit);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

            return entries;
        }

        #endregion

        //Catégorie
        #region [BDD] Ajouter une catégorie
        /// <summary>
        /// Requête SQL qui ajoute une catégorie à la table "Categorie".
        /// </summary>
        public static void AddCategorie(Categorie categorie)
        {
            try
            {
                using (MySqlConnection db =
            new MySqlConnection(DataAccess.CHEMINBDD))
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        #region [BDD] Vérifie sur la catégorie existe
        /// <summary>
        /// Requête SQL qui vérifie si la catégorie existe.
        /// Si elle existe, stocke l'Id dans une variable et la retourne.
        /// Sinon, retourne juste IdTrouve.
        /// </summary>
        public static FonctionsConsole.IdTrouve VerificationCategorie(string categorie)
        {
            FonctionsConsole.IdTrouve resultat = new FonctionsConsole.IdTrouve();
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand selectCommand = new MySqlCommand
                        ("SELECT Id_Categorie, Nom_Categorie FROM categorie WHERE Nom_Categorie = @categorie", db);
                    selectCommand.Parameters.AddWithValue("@categorie", categorie);

                    MySqlDataReader query = selectCommand.ExecuteReader();

                    if (query.Read())
                    {
                        int idCategorie = (int)query["Id_Categorie"];
                        resultat = new FonctionsConsole.IdTrouve(idCategorie);
                    }
                    else
                    {
                        resultat = new FonctionsConsole.IdTrouve();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return resultat;
        }
        #endregion

        //Commande
        #region [BDD] Ajouter une commande
        /// <summary>
        /// Requête SQL qui ajoute une commande à la table "commande".
        /// </summary>
        public static void AddCommande(Commande commande)
        {
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        //Options Paiement & Moyen de paiement
        #region [BDD] Afficher toute les options de paiement
        /// <summary>
        /// Affiche toutes les options de paiement situées dans la table opt_paiement.
        /// Stocke les options de paiement dans une liste.
        /// </summary>
        public static List<OptionPaiement> GetAllPayement()
        {
            List<OptionPaiement> entries = new List<OptionPaiement>();
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

            return entries;
        }
        #endregion

        #region [BDD] Ajouter un moyen de paiement
        /// <summary>
        /// Ajoute un moyen de paiement à la table "Inter_Facture_Paiement".
        /// </summary>
        public static void AddIFP(IFP ifp)
        {
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "INSERT INTO inter_facture_paiement (FK_Id_Facture, FK_Id_Paiement, Montant_Paiement) VALUES (@FK_Id_Facture, @FK_Id_Paiement, @Montant_paiement)";

                    insertCommand.Parameters.AddWithValue("@FK_Id_Facture", ifp.FK_Id_Facture);
                    insertCommand.Parameters.AddWithValue("@FK_Id_Paiement", ifp.FK_Id_Paiement);
                    insertCommand.Parameters.AddWithValue("@Montant_paiement", ifp.Montant_Paiement);
                    insertCommand.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        //Origine
        #region [BDD] Ajouter une origine
        /// <summary>
        /// Ajoute une origine à la table "Origine" de la base de données. 
        /// </summary>
        public static void AddOrigine(Origine origine)
        {
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
                {
                    db.Open();
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "INSERT INTO origine (Nom_Origine) VALUES (@Nom_Origine)";

                    insertCommand.Parameters.AddWithValue("@Nom_Origine", origine.Nom_Origine);
                    insertCommand.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        #region [BDD] Vérifie si l'origine existe
        /// <summary>
        /// Vérifie si l'origine existe dans la base de données.
        /// Si elle existe stock prend l'id et la stocke dans une variable.
        /// Si elle n'existe pas renvoie juste IdTrouve.
        /// </summary>
        public static FonctionsConsole.IdTrouve VerificationOrigine(string origine)
        {
            FonctionsConsole.IdTrouve resultat = new FonctionsConsole.IdTrouve();
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand selectCommand = new MySqlCommand
                        ("SELECT Id_Origine, Nom_Origine FROM origine WHERE Nom_Origine = @origine", db);

                    selectCommand.Parameters.AddWithValue("@origine", origine);
                    MySqlDataReader query = selectCommand.ExecuteReader();

                    if (query.Read())
                    {
                        int idOrigine = (int)query["Id_Origine"];
                        resultat = new FonctionsConsole.IdTrouve(idOrigine);
                    }
                    else
                    {
                        resultat = new FonctionsConsole.IdTrouve();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return resultat;
        }
        #endregion

        //Unité
        #region [BDD] Ajoute une nouvelle unité 
        /// <summary>
        /// Ajoute une unité à la base table "Unite".
        /// </summary>
        /// <param name="unite"></param>
        public static void AddUnite(Unite unite)
        {
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }


        }
        #endregion

        #region [BDD] Vérifie si l'unité existe déjà
        /// <summary>
        /// Vérifie si l'unité existe.
        /// Si elle existe on retourne l'ID.
        /// Si elle n'existe pas alors on retourne juste IDTrouve.
        /// </summary>
        public static FonctionsConsole.IdTrouve VerificationUnite(string unite)
        {
            FonctionsConsole.IdTrouve resultat = new FonctionsConsole.IdTrouve();
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand selectCommand = new MySqlCommand
                        ("SELECT Id_Unite, Libelle_Unite FROM unite WHERE Libelle_Unite = @Libelle_Unite", db);
                    selectCommand.Parameters.AddWithValue("@Libelle_Unite", unite);

                    MySqlDataReader query = selectCommand.ExecuteReader();

                    if (query.Read())
                    {
                        int idUnite = (int)query["Id_Unite"];
                        resultat = new FonctionsConsole.IdTrouve(idUnite);
                    }
                    else
                    {
                        resultat = new FonctionsConsole.IdTrouve();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            return resultat;
        }
        #endregion

        //Facture
        #region [BDD] Ajoute une facture
        /// <summary>
        /// Requête SQL qui ajoute une facture à la table "Facture".
        /// </summary>
        /// <param name="facture"></param>
        public static void AddFacture(Facture facture)
        {
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

        }
        #endregion

        #region [BDD] Trouver le dernier numéro de facture
        /// <summary>
        /// Requête qui trouve le dernier numéro de facture et le stocke.
        /// Si aucun numéro de facture trouvé alors numéro de facture égal 0.
        /// </summary>
        public static int GetLastNumFacture()
        {
            int resultat = 0;
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "SELECT facture.Num_Facture FROM facture ORDER BY facture.Num_Facture DESC LIMIT 1";

                    MySqlDataReader query = insertCommand.ExecuteReader();

                    if (query.Read())
                    {

                        resultat = query.GetInt32(0);
                    }
                    else
                    {
                        resultat = 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

            return resultat;
        }
        #endregion

        #region [BDD] Trouver le dernier ID de Facture
        /// <summary>
        /// Retourne le dernier id facture
        /// </summary>
        /// <returns></returns>
        public static int GetLastIdFacture()
        {
            int ID = 0;
            try
            {
                using (MySqlConnection db =
                new MySqlConnection(CHEMINBDD))
                {
                    db.Open();

                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "SELECT facture.Id_Facture FROM facture ORDER BY facture.Id_Facture DESC LIMIT 1";

                    MySqlDataReader query = insertCommand.ExecuteReader();

                    if (query.Read())
                    {

                        ID = query.GetInt32(0);
                    }
                    else
                    {
                        ID = 0;
                    }

                    db.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

            return ID;
        }

        #endregion
    }
}
