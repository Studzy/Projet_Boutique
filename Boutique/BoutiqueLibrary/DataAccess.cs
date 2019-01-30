using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BoutiqueBDDLibrary;

namespace BoutiqueBDDLibrary
{
    public class DataAccess
    {
        //Chemin BDD
        #region [BDD] Chemin vers Base de données
        /// <summary>
        /// Chemin d'accès à la base de données ici sur MySql PhpmyAdmin.
        /// </summary>
        public const string CHEMINBDD = "SERVER=127.0.0.1; DATABASE=bdd_boutique; UID=root; PASSWORD=;";
        #endregion

        //Clients
        #region [BDD] Ajouter un Client
        /// <summary>
        /// Insère un client dans la base de donnée, dans la table "Client" à l'aide de l'objet Client.
        /// </summary>
        /// <param name="x"></param>
        public static void InsererClientEnBDD(Client x)
        {
            MySqlConnection connection = new MySqlConnection(DataAccess.CHEMINBDD);
            connection.Open();

            MySqlCommand firstInsert =
                new MySqlCommand("INSERT INTO Client (Nom_Client, Prenom_Client, Adresse_Client, Mail_Client, NumTel_Client, Date_Naissance_Client, MDP_Client, FK_Id_CPVille) VALUES (@nom, @prenom, @adresse, @email, @telephone, @datenaissance, SHA2(@motdepasse,256), @idcpville)", connection);
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

            Console.Clear();

            connection.Close();
        }
        #endregion

        #region [BDD] Modifier un client
        /// <summary>
        /// Modifie le client dans la base de données.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="x"></param>
        public static void modifieUnClientenBDD(string email, Client x)
        {
            using (MySqlConnection db =
            new MySqlConnection(DataAccess.CHEMINBDD))
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
        #endregion

        // 5 requêtes pour supprimer un client de la base de données.
        #region [BDD] 1- Récupère les ID facture et commande d'un client
        public static List<int> recupeIdFactureEtIdCommande(string email)
        {
            List<int> IdMultiple = new List<int>();
            using (MySqlConnection db =
            new MySqlConnection(DataAccess.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "SELECT facture.Id_Facture, commande.Id_Commande FROM client INNER JOIN facture ON facture.FK_Id_Client = client.Id_Client INNER JOIN commande ON commande.FK_Id_Facture = facture.Id_Facture WHERE Mail_Client = @email";

                insertCommand.Parameters.AddWithValue("@email", email);
                MySqlDataReader query = insertCommand.ExecuteReader();
                
                if (query.Read())
                {
                    IdMultiple.Add(query.GetInt32(1));//Commande
                    IdMultiple.Add(query.GetInt32(0));//Facture
                }
                else
                {
                    IdMultiple.Add(0);
                }
                return IdMultiple;
            }
        }
        #endregion

        #region [BDD] 2- Supprime une commande d'un client
        /// <summary>
        /// Supprime un client dans la base de données.
        /// </summary>
        public static void supprimerUneCommande(int Id_Commande)
        {
            using (MySqlConnection db =
            new MySqlConnection(DataAccess.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "DELETE FROM commande WHERE commande.Id_Commande = @idcommande";
                insertCommand.Parameters.AddWithValue("@idcommande", Id_Commande);
                insertCommand.ExecuteNonQuery();
            }
        }
        #endregion

        #region [BDD] 3- Supprime Le lien Inter_Facture_Paiement
        /// <summary>
        /// Supprime un client dans la base de données.
        /// </summary>
        public static void supprimerInterFacturePaiement(int Id_Facture)
        {
            using (MySqlConnection db =
            new MySqlConnection(DataAccess.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "DELETE FROM inter_facture_paiement WHERE inter_facture_paiement.FK_Id_Facture = @idfacture";
                insertCommand.Parameters.AddWithValue("@idfacture", Id_Facture);
                insertCommand.ExecuteNonQuery();
            }
        }
        #endregion

        #region [BDD] 4- Supprime une facture d'un client
        /// <summary>
        /// Supprime un client dans la base de données.
        /// </summary>
        public static void supprimerUneFacture(int Id_Facture)
        {
            using (MySqlConnection db =
            new MySqlConnection(DataAccess.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "DELETE FROM facture WHERE facture.Id_Facture = @idfacture";
                insertCommand.Parameters.AddWithValue("@idfacture", Id_Facture);
                insertCommand.ExecuteNonQuery();
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
            using (MySqlConnection db =
            new MySqlConnection(DataAccess.CHEMINBDD))
            {
                db.Open();
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "DELETE FROM client WHERE Mail_Client = @email ";
                insertCommand.Parameters.AddWithValue("@email", email);
                insertCommand.ExecuteNonQuery();
            }
            Console.WriteLine("\nNous avons supprimer le client ainsi que toutes les données qui lui étaient liés !");
        }
        #endregion


        #region [BDD] Affiche un client
        /// <summary>
        /// Vérifie si l'email existe dans la base de données,
        /// Si il existe renvoie l'objet client sinon, change l'objet client.Mail_Client à "Rien".
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Client verifieSiClientExiste(string email)
        {
            Client z = new Client();
            using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
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
            return z;
        }
        #endregion

        #region [BDD] Affiche ID et Email Client
        /// <summary>
        /// Vérifie si l'email existe dans la base de données,
        /// Si il existe renvoie l'objet client sinon, change l'objet client.Mail_Client à "Rien".
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Client afficheIDEmailClient(string email)
        {
            Client z = new Client();
            using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
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
            return z;
        }
        #endregion

        #region [BDD] Afficher tout les clients
        /// <summary>
        /// Affiche tout les clients trouvé dans la base de données, retourne sous forme de liste.
        /// </summary>
        /// <returns></returns>
        public static List<Client> GetAllProducts()
        {
            List<Client> entries = new List<Client>();

            using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
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
            using (MySqlConnection connection = new MySqlConnection(DataAccess.CHEMINBDD))
            {
                connection.Open();
                using (MySqlCommand firstInsert = connection.CreateCommand())
                {
                    firstInsert.CommandText = "SELECT Mail_Client FROM Client WHERE Mail_Client = @mail";
                    firstInsert.Parameters.AddWithValue("@mail", mail);
                    using (MySqlDataReader reader = firstInsert.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        #endregion

        #region [BDD] Vérifier MDP = Email pour  Client
        /// <summary>
        /// Vérifie que l'émail client est compatible avec le mot de passe si compatible retourne "true" sinon "false".
        /// </summary>
        /// <param name="email"></param>
        /// <param name="motdepasse"></param>
        /// <returns></returns>
        public static bool VerifieMailMdpClient(string email, string motdepasse)
        {
            bool resultat = false;
            using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
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
            MySqlConnection connection = new MySqlConnection(DataAccess.CHEMINBDD);
            connection.Open();

            MySqlCommand firstInsert =
                new MySqlCommand("INSERT INTO admin (Mail_Admin, MDP_Admin) VALUES (@email,SHA2(@motdepasse,256))", connection);
            var emailParameter = new MySqlParameter("@email", x.Mail_Admin);
            var motdepasseParameter = new MySqlParameter("@motdepasse", x.Mot_De_Passe);
            firstInsert.Parameters.Add(emailParameter);
            firstInsert.Parameters.Add(motdepasseParameter);
            firstInsert.ExecuteNonQuery();

            Console.Clear();
            Console.WriteLine("L'inscription s'est déroulée avec succès !\n");


            connection.Close();
        }
        #endregion

        #region [BDD] Vérifie Si l'Email Admin existe
        /// <summary>
        /// Vérifie si l'émail administrateur est présent dans la base de données, si présent retourne "true" sinon "false".
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static bool VerifierExistanceMailAdmin(string mail)
        {
            using (MySqlConnection connection = new MySqlConnection(DataAccess.CHEMINBDD))
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
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        #endregion

        #region [BDD] Vérifier MDP = Email pour  Admin
        /// <summary>
        /// Vérifie que l'émail admin est compatible avec le mot de passe si compatible retourne "true" sinon "false".
        /// </summary>
        /// <param name="email"></param>
        /// <param name="motdepasse"></param>
        /// <returns></returns>
        public static bool VerifieMailMdpAdministrateur(string email, string motdepasse)
        {
            bool resultat = false;
            using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
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
            MySqlConnection connection = new MySqlConnection(DataAccess.CHEMINBDD);
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
        #endregion

        #region [BDD] Vérifie si la ville existe
        /// <summary>
        /// Vérifie si le client existe dans la base de données.
        /// </summary>
        /// <param name="Ville"></param>
        /// <returns></returns>
        public static FonctionsConsole.IdTrouve VerificationVille(string Ville)
        {
            string nomVille = Ville.ToUpper();
            using (MySqlConnection db =
                new MySqlConnection(DataAccess.CHEMINBDD))
            {
                db.Open();

                MySqlCommand selectCommand = new MySqlCommand
                    ("SELECT Id_CPVille, Nom_Ville FROM cp_ville WHERE Nom_Ville = @Ville", db);
                selectCommand.Parameters.AddWithValue("@Ville", Ville);

                MySqlDataReader query = selectCommand.ExecuteReader();

                if (query.Read())
                {
                    int idVille = (int)query["Id_CPVille"];
                    return new FonctionsConsole.IdTrouve(idVille);
                }
                else
                {
                    return new FonctionsConsole.IdTrouve();
                }
            }
        }
        #endregion
    }
}
