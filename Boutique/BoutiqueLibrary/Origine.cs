using MySql.Data.MySqlClient;

namespace BoutiqueBDDLibrary
{
    public class Origine
    {
        //Déclaration des variables
        private int id_Origine;
        private string nom_Origine;

        //Constructeur
        public Origine()
        {
        }

        //Get;Set; Vérifications
        #region Origine
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public Origine(int id_Origine, string nom_Origine)
        {
            Id_Origine = id_Origine;
            Nom_Origine = nom_Origine;
        }
        #endregion

        #region Id_Origine
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public int Id_Origine
        {
            get => id_Origine;
            set => id_Origine = value;
        }
        #endregion

        #region Nom_Origine
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI REMISE_PRODUIT
        /// </summary>
        public string Nom_Origine
        {
            get => nom_Origine.ToUpper();
            set
            {
                if (value.Length < 1 || value.Length > 50 || !FonctionsConsole.VerifieSiQueDesLettres(value))
                {
                    throw new FonctionsConsole.MonMessageErreur("Le nom n'est pas valable");
                }
                else
                {
                    nom_Origine = value.ToUpper();
                }
            }
            
        }
        #endregion
    }
}
