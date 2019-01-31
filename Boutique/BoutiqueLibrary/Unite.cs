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
        /// COMMENTAIRE A MODIFIER CAR NON FINI Id_LIBELLE
        /// </summary>
        public int Id_Libelle { get => id_Libelle; set => id_Libelle = value; }
        #endregion

        #region Libelle_Unite
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI lIBELLE8uNITE
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
                    libelle_unite = value;
                }
            }
        }
        #endregion
    }
}
