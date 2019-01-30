using System;
using System.Collections.Generic;
using System.Text;

namespace BoutiqueBDDLibrary
{
    /// <summary>
    /// Le resultat d'une recherche d'id.
    /// <see cref="Trouve"/> est true si un id a été trouvé.
    /// L'id est alors stocké dans <see cref="Id"/>.
    /// </summary>
    ///
    public class IdTrouve
    {
        int id;
        public bool Trouve { get; }
        public int Id
        {
            get
            {
                if (!this.Trouve)
                    throw new InvalidOperationException();
                return this.id;
            }
        }
        /// <summary>
        /// L'id n'a pas été trouvé.
        /// </summary>
        public IdTrouve()
        {
            this.Trouve = false;
        }
        /// <summary>
        /// id est l'id qui a été trouvé.
        /// </summary>
        public IdTrouve(int id)
        {
            this.Trouve = true;
            this.id = id;
        }
    }
}
