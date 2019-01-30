using System;

namespace BoutiqueBDDLibrary
{
    public class Client
    {
        //Déclaration des variables
        private string nom_client;
        private string prenom_client;
        private string adresse_client;
        private string mail_client;
        private string numtel_client;
        private DateTime date_naissance_client;
        private string mdp_client;
        private int id_CpVille;
        private string CodePostal_Ville;
        private string Nom_Ville;

        //Constructeur
        public Client()
        {
        }

        //Get;Set; Vérifications
        #region Nom_Client
        /// <summary>
        /// Vérifie le nom du client dans le set, si ce n'est pas bon une exeption est afficher
        /// </summary>
        public string Nom_Client
        {
            get => nom_client.ToUpper();
            set
            {
                if (value.Length < 1 || value.Length > 80 || !FonctionsConsole.VerifieSiQueDesLettres(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("ERREUR: Le nom n'est pas valide");
                }
                else
                {
                    nom_client = value;
                }
            }
        }
        #endregion

        #region Prenom_client
        /// <summary>
        /// Vérifie le prénom du client dans le set, si ce n'est pas bon une exeption est afficher
        /// </summary>
        public string Prenom_client
        {
            get => prenom_client;
            set
            {
                if (value.Length < 2 || value.Length > 80 || !FonctionsConsole.VerifieSiQueDesLettres(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("ERREUR: Le prénom n'est pas valide");
                }
                else
                {
                    prenom_client = value;
                }
            }
        }
        #endregion

        #region Adresse_client
        /// <summary>
        /// Vérifie l'adresse du client dans le set, si ce n'est pas bon une exeption est afficher
        /// </summary>
        public string Adresse_client
        {
            get => adresse_client;
            set
            {
                if (value.Length < 2 || value.Length > 80 || string.IsNullOrWhiteSpace(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("ERREUR: L'adresse n'est pas valide");
                }
                else
                {
                    adresse_client = value;
                }
            }
        }
        #endregion

        #region Mail_client
        /// <summary>
        /// Vérifie l'émail du client dans le set, si ce n'est pas bon une exeption est afficher
        /// </summary>
        public string Mail_client
        {
            get => mail_client;
            set => mail_client = value;
        }
        #endregion

        #region Numtel_Client
        /// <summary>
        /// Vérifie le numéro de téléphone du client dans le set, si ce n'est pas bon une exeption est afficher
        /// </summary>
        public string Numtel_Client
        {
            get => numtel_client;
            set
            {

                if (value.Length != 10 || !FonctionsConsole.verifieSiQueDesChiffres(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("ERREUR: Le numéro n'est pas valide !");
                }
                else
                {
                    numtel_client = value;
                }

            }
        }
        #endregion

        #region Date_naissance_client
        /// <summary>
        /// Stock les données de la date de naissance dans le set et vérifie dans une autre fonction.
        /// </summary>
        public DateTime Date_naissance_client
        {
            get => date_naissance_client;
            set
            {
                FonctionsConsole.verifDateDeNaissance(value);
                date_naissance_client = value;
            }
        }
        #endregion

        #region Mdp_Client
        /// <summary>
        /// Vérifie le mot de passe dans le set, si ce n'est pas bon une exeption est afficher
        /// </summary>
        public string Mdp_client
        {
            get => mdp_client;
            set
            {
                FonctionsConsole.VerifMdp(value);
                mdp_client = value;
            }
        }
        #endregion

        #region Id_CpVille
        public int Id_CpVille { get => id_CpVille; set => id_CpVille = value; }
        #endregion

        #region Id_client
        public int Id_client { get; set; }
        #endregion

        #region CodePostal_Ville
        public string codePostal_Ville { get => CodePostal_Ville; set => CodePostal_Ville = value; }
        #endregion

        #region Nom_Ville
        public string nom_Ville { get => Nom_Ville; set => Nom_Ville = value; }
        #endregion

    }
}
