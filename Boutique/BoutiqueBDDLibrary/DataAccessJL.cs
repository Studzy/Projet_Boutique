using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;
using BoutiqueBDDLibrary;

namespace BoutiqueBDDLibrary
{
    public static class DataAccessJL
    {
        #region [BDD] Chemin vers la base de données
        public const string CHEMINBDD = "SERVER=127.0.0.1; DATABASE=bdd_boutique; UID=root; PASSWORD=;";
        #endregion

        public static bool IsCorrectString80(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length > 79)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }
        public static bool IsCorrectString100(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length > 99)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }
        public static bool IsCorrectString50(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length > 51)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }
        public static bool IsCorrectString15(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length < 16)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }
        public static bool IsCorrectString5(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length < 6)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }
        public static bool IsCorrectString25(string insertString)
        {
            if (string.IsNullOrWhiteSpace(insertString) || insertString.Length < 26)
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }
        public static bool IsCorrectDecimal(string param)
        {

            if (typeof(decimal).IsAssignableFrom(param.GetType()))
            {
                // EXCEPTION
                throw new MonMessageErreur("");
            }
            else
            {
                return true;
            }
        }
    }

    public class MonMessageErreur : Exception
    {
        public string errorMessage;
        public MonMessageErreur(string message)
           : base(message)
        {
            errorMessage = message;
        }
    }
}

