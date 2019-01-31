namespace BoutiqueBDDLibrary
{
    public class Categorie
    {
        //Déclaration de variables
        private int id_Categorie;
        private string nom_categorie;

        //Constructeur
        public Categorie()
        {
        }

        //Get;Set; Vérifications
        #region Id_Categorie
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI Id_Categorie
        /// </summary>
        public int Id_Categorie { get => id_Categorie; set => id_Categorie = value; }
        #endregion

        #region Nom_Categorie
        /// <summary>
        /// COMMENTAIRE A MODIFIER CAR NON FINI Nom_Categorie
        /// </summary>
        public string Nom_categorie { get => nom_categorie; set => nom_categorie = value; }
        #endregion
    }   
}
