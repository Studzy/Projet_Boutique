using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
