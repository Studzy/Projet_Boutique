using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BoutiqueBDDLibrary;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        //TEST DE FONCTIONS 
        #region [TEST] Fonction vérifier existance mail client
        [TestMethod] 
        public void Verifie_Si_Client_Test_Existe()
        {
            bool Resultat = DataAccess.VerifierExistanceMailClient("client.test@boutique.fr");
            Assert.AreEqual(true, Resultat);
        }

        [TestMethod]
        public void Verifie_Si_Client_Test_Existe_Pas()
        {
            bool Resultat = DataAccess.VerifierExistanceMailClient("client");
            Assert.AreEqual(false, Resultat);
        }

        #endregion

        #region [TEST] Fonction vérifie format email 
        //Vérifie quand c'est bon
        [TestMethod]
        public void Verifie_Si_Format_Mail_Est_Bon()
        {
            bool Resultat = FonctionsConsole.VerifEmail("test.test@test.fr");
            Assert.AreEqual(true, Resultat);
        }

        //Vérifie quand c'est pas bon
        [TestMethod]
        public void Verifie_Si_Format_Mail_Est_Pas_Bon()
        {
            bool Resultat = FonctionsConsole.VerifEmail(" ");
            Assert.AreEqual(false, Resultat);
        }
        #endregion


        //TEST DU CONSTRUCTEUR CLIENT
        #region [TEST] Nom_Client
        //Vérifie quand c'est bon
        [TestMethod]
        public void Verifie_Si_Nom_Est_Bon()
        {
            var client = new Client();
            client.Nom_Client = "test";

            Assert.AreEqual("TEST", client.Nom_Client);
        }

        //Vérifie l'exception de Nom_Client
        [TestMethod]
        public void Verifie_Si_Nom_Est_Pas_Bon()
        {
            var client = new Client();

            var exception = Assert.ThrowsException<FonctionsConsole.MonMessageErreur>(
                () => client.Nom_Client = " ");
        }
        #endregion

        #region [TEST] Prenom_Client
        //Vérifie quand c'est bon
        [TestMethod]
        public void Verifie_Si_Prenom_Est_Bon()
        {
            var client = new Client();
            client.Prenom_client = "Quentin";

            Assert.AreEqual("Quentin", client.Prenom_client);
        }

        //Vérifie l'exception de Prenom_Client
        [TestMethod]
        public void Verifie_Si_Prenom_Est_Pas_Bon()
        {
            var client = new Client();
            var exception = Assert.ThrowsException<FonctionsConsole.MonMessageErreur>(
                () => client.Prenom_client = " ");
        }
        #endregion

        #region [TEST] Adresse_Client
        //Vérifie quand c'est bon
        [TestMethod] 
        public void Verifie_Si_Adresse_Est_Bon()
        {
            var client = new Client();
            client.Adresse_client = "6 rue paul verlaine";

            Assert.AreEqual("6 rue paul verlaine", client.Adresse_client);
        }

        //Vérifie l'exception de Adresse_Client
        [TestMethod]
        public void Verifie_Si_Adresse_Est_Pas_Bon()
        {
            var client = new Client();
            var exception = Assert.ThrowsException<FonctionsConsole.MonMessageErreur>(
                () => client.Adresse_client = " ");
        }
        #endregion

        #region [TEST] Email_Client
        //Vérifie quand c'est bon
        [TestMethod]
        public void Verifie_Si_Email_Client_Est_Bon()
        {
            var client = new Client();
            client.Mail_client = "client.test@boutique.fr";

            Assert.AreEqual("client.test@boutique.fr", client.Mail_client);
        }
        #endregion

        #region [TEST] NumTel_Client
        //Vérifie quand c'est bon
        [TestMethod]
        public void Verifie_Si_Numero__Client_Est_Bon()
        {
            var client = new Client();
            client.Numtel_Client = "0123456789";

            Assert.AreEqual("0123456789", client.Numtel_Client);
        }

        //Vérifie l'exception de NumTel_Client
        [TestMethod]
        public void Verifie_Si_Numero__Client_Est_Pas_Bon()
        {
            var client = new Client();
            var exception = Assert.ThrowsException<FonctionsConsole.MonMessageErreur>(
                () => client.Numtel_Client = "12");
        }
        #endregion

        #region [TEST] Mot de passe
        //Vérifie quand c'est bon
        [TestMethod]
        public void Verifie_Si_Mot_De_Passe_Client_Est_Bon()
        {
            var client = new Client();
            client.Mdp_client = "Boutique123";

            Assert.AreEqual("Boutique123", client.Mdp_client);
        }

        //Vérifie l'exception Mot de passe client
        [TestMethod]
        public void Verifie_Si_Mot_De_Passe_Client_Est_Pas_Bon()
        {
            var client = new Client();
            var exception = Assert.ThrowsException<FonctionsConsole.MonMessageErreur>(
                () => client.Mdp_client = " ");
        }
        #endregion

        //TEST DU CONSTRUCTEUR PRODUIT
        #region [TEST] Nom du produit
        /// <summary>
        /// Test si le nom produit est bon
        /// </summary>
        [TestMethod]

        public void Le_Nom_Du_Produit_Est_Bon()
        {
            var produit = new Produit();
            produit.Nom_Produit = "Pomme";
            Assert.AreEqual("Pomme", produit.Nom_Produit);
        }
        /// <summary>
        /// Test si le nom produit est mauvais
        /// </summary>
        [TestMethod]
        public void Le_Nom_Du_Produit_Est_Mauvais()
        {
            var produit = new Produit();
            var exception = Assert.ThrowsException<FonctionsConsole.MonMessageErreur>(() => produit.Nom_Produit = "");
        }
        #endregion

        #region [TEST] TVA du produit
        /// <summary>
        /// Test si la TVA produit est bon
        /// </summary>
        [TestMethod]

        public void La_TVA_Du_Produit_Est_Bon()
        {
            var produit = new Produit();
            produit.TVA = 5.5m;
            Assert.AreEqual(5.5m, produit.TVA);
        }
        /// <summary>
        /// Test si la TVA produit est mauvaise
        /// </summary>
        [TestMethod]
        public void La_TVA_Du_Produit_Est_Mauvaise()
        {
            var produit = new Produit();
            var exception = Assert.ThrowsException<FonctionsConsole.MonMessageErreur>(() => produit.TVA = 120);
        }
        #endregion

        #region [TEST] Prix du produit
        /// <summary>
        /// Test si le prix produit est bon
        /// </summary>
        [TestMethod]

        public void Le_Prix_Du_Produit_Est_Bon()
        {
            var produit = new Produit();
            produit.Prix_Produit = 10;
            Assert.AreEqual(10, produit.Prix_Produit);
        }
        /// <summary>
        /// Test si le prix produit est mauvais
        /// </summary>
        [TestMethod]
        public void Le_Prix_Du_Produit_Est_Mauvais()
        {
            var produit = new Produit();
            var exception = Assert.ThrowsException<FormatException>(() => produit.Prix_Produit = -1);
        }
        #endregion

        #region [TEST] Remise du produit
        /// <summary>
        /// Test si la remise produit est bon
        /// </summary>
        [TestMethod]

        public void La_Remise_Du_Produit_Est_Bon()
        {
            var produit = new Produit();
            produit.Remise_Produit = 25m;
            Assert.AreEqual(25m, produit.Remise_Produit);
        }
        /// <summary>
        /// Test si la remise produit est mauvaise
        /// </summary>
        [TestMethod]
        public void La_Remise_Du_Produit_Est_Mauvaise()
        {
            var produit = new Produit();
            var exception = Assert.ThrowsException<FonctionsConsole.MonMessageErreur>(() => produit.Remise_Produit = 150m);
        }
        #endregion

        #region [TEST] Description du produit
        /// <summary>
        /// Test si la description produit est bon
        /// </summary>
        [TestMethod]

        public void La_Description_Du_Produit_Est_Bon()
        {
            var produit = new Produit();
            produit.Description_Produit = "Ceci est une description";
            Assert.AreEqual("Ceci est une description", produit.Description_Produit);
        }
        /// <summary>
        /// Test si la description produit est mauvaise
        /// </summary>
        [TestMethod]
        public void La_Description_Du_Produit_Est_Mauvaise()
        {
            var produit = new Produit();
            string message = "";
            for (int i = 0; i < 65536; i++)
            {
                message = message + "a";
            }
            var exception = Assert.ThrowsException<FonctionsConsole.MonMessageErreur>(() => produit.Description_Produit = message);
        }
        #endregion

        #region [TEST] Valeur nutritionnelle du produit
        /// <summary>
        /// Test si la valeur nutritionnelle produit est bon
        /// </summary>
        [TestMethod]

        public void La_Valeur_Nutritionnelle_Du_Produit_Est_Bon()
        {
            var produit = new Produit();
            produit.Val_Nutrition_Produit = 400;
            Assert.AreEqual(400, produit.Val_Nutrition_Produit);
        }
        /// <summary>
        /// Test si la valeur nutritionnelle produit est mauvaise
        /// </summary>
        [TestMethod]
        public void La_Valeur_Nutritionnelle_Du_Produit_Est_Mauvaise()
        {
            var produit = new Produit();
            var exception = Assert.ThrowsException<FonctionsConsole.MonMessageErreur>(() => produit.Val_Nutrition_Produit = -1);
        }
        #endregion
    }
}
