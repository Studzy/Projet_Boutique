using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class Unite
    {
        //Déclaration des variables
        private int id_Libelle;
        private string libelle_unite;

        //Constructeur
        public Unite()
        {
        }

        //Get;Set; Vérifications
        #region Id_Libelle
        /// <summary>
        /// Id_Libelle est en lecture seule
        /// </summary>
        public int Id_Libelle { get => id_Libelle;}
        #endregion

        #region Libelle_Unite
        /// <summary>
        /// Vérifie le libelle de l'unité dans le set. Si ce n'est pas bon, une exception est affichée.
        /// </summary>
        public string Libelle_unite
        {
            get => libelle_unite;
            set
            {
                if (value.Length < 1 || value.Length > 50 || !FonctionsConsole.VerifieSiQueDesLettres(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("Le libelle n'est pas valable");
                }
                else
                {
                    FonctionsConsole.premiereLettreMajuscule(value);
                    libelle_unite = value;
                }
            }
        }
        #endregion
    }
}
