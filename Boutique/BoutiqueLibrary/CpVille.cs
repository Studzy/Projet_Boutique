using BoutiqueBDDLibrary;

namespace BoutiqueBDDLibrary
{
    public class CpVille
    {
        //Déclaration des variables
        private string code_postal_ville;
        private string nom_ville;

        //Constructeur
        public CpVille()
        {
        }

        //Get;Set; Vérifications
        #region Code_postal_ville
        /// <summary>
        /// Vérifie le code postal dans le set, si ce n'est pas bon une exeption est afficher.
        /// </summary>
        public string Code_postal_ville
        {
            get => code_postal_ville;
            set
            {
                if (value.Length != 5 || string.IsNullOrWhiteSpace(value) || !FonctionsConsole.verifieSiQueDesChiffres(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("ERREUR: Le code postal n'est pas valide !");
                }
                else
                {
                    code_postal_ville = value;
                }
            }
        }
        #endregion

        #region Nom_ville
        /// <summary>
        /// Vérifie le nom de la ville dans le set, si ce n'est pas bon une exeption est afficher.
        /// </summary>
        public string Nom_ville
        {
            get => nom_ville.ToUpper();
            set
            {
                if (value.Length < 3 || value.Length > 50 || !FonctionsConsole.VerifieSiQueDesLettres(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("ERREUR: Le nom de ville n'est pas valide !");
                }
                else
                {
                    nom_ville = value;
                }
            }
        }
        #endregion
    }
}
