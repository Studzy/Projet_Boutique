namespace BoutiqueBDDLibrary
{
    public class Admin
    {
        //Déclaration des variables
        private string mail_admin;
        private string mot_de_passe;

        //Constructeur
        public Admin()
        {
        }

        //Get;Set; Vérifications
        #region Mail_Admin
        /// <summary>
        /// Vérifie Mail de l'administrateur dans le set, si ce n'est pas bon une exeption est afficher
        /// </summary>
        public string Mail_Admin
        {
            get => mail_admin;
            set
            {
                if (value.Length < 6 || value.Length > 80 || !FonctionsConsole.VerifEmail(value) || DataAccess.VerifierExistanceMailAdmin(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("ERREUR: L'adresse email n'est pas valide où existe en déjà");
                }
                else
                {
                    mail_admin = value;
                }
            }
        }
        #endregion

        #region Mot_De_Passe
        /// <summary>
        /// Vérifie le mot de passe de l'administrateur dans le set, si ce n'est pas bon une exeption est afficher
        /// </summary>
        public string Mot_De_Passe
        {
            get => mot_de_passe;
            set
            {
                if (value.Length < 1 || value.Length > 100 || string.IsNullOrWhiteSpace(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("ERREUR: Le mot de passe n'est pas valide (7 caractères mini | 1 chiffre | 1 Maj)");
                }
                else
                {
                    mot_de_passe = value;
                }
            }
        }
        #endregion
    }
}
