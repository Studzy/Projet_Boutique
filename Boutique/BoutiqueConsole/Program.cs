using System;
using BoutiqueBDDLibrary;
using System.Runtime.InteropServices;

namespace BoutiqueConsole
{
    class Program
    {
        #region Change la taille de la fenêtre automatiquement
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        #endregion

        static void Main(string[] args)
        {
            //Assigne la taille à la fenêtre de la console
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);

            #region Stock dans des variables la boutique
            string a ="     _______________________________";
            string b ="     [=U=U=U=U=U=U=U=U=U=U=U=U=U=U=U=]";
            string c ="     |.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.|";
            string d ="     |        +-+-+-+-+-+-+-+        |";
            string e ="     |        |   BOUTIQUE  |        |";
            string f ="     |        +-+-+-+-+-+-+-+        |";
            string g ="     |.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.|";
            string h ="     |  _________  __ __  _________  |";
            string i ="    _ | |___   _  ||[]|[]||  _      | | _";
            string j ="    (!)||OPEN|_(!)_|| ,| ,||_(!)_____| |(!)";
            string k ="    .T~T|:.....:T~T.:|__|__|:.T~T.:....:|T~T.";
            string l ="     ||_||||||||||_|||||||||||||_||||||||||_||";
            string m ="     ~\\=/~~~~~~~~\\=/~~~~~~~~~~~\\=/~~~~~~~~\\=/~";
            string n ="      | -------- | ----------- | -------- | ";
            string o ="       ~ |~^ ^~~^ ~~| ~^  ~~ ^~^~ |~ ^~^ ~~^ |^~ \n\n";
            string p ="  APPUYER SUR ENTRER POUR ENTRER DANS LA BOUTIQUE";
            #endregion

            #region Affiche toutes les variables
            Console.WriteLine("\n\n\n\n\n\n\n\n\n");
            Console.SetCursorPosition((Console.WindowWidth - a.Length) / 2, Console.CursorTop);
            Console.WriteLine(a);
            Console.SetCursorPosition((Console.WindowWidth - b.Length) / 2, Console.CursorTop);
            Console.WriteLine(b);
            Console.SetCursorPosition((Console.WindowWidth - c.Length) / 2, Console.CursorTop);
            Console.WriteLine(c);
            Console.SetCursorPosition((Console.WindowWidth - d.Length) / 2, Console.CursorTop);
            Console.WriteLine(d);
            Console.SetCursorPosition((Console.WindowWidth - e.Length) / 2, Console.CursorTop);
            Console.WriteLine(e);
            Console.SetCursorPosition((Console.WindowWidth - f.Length) / 2, Console.CursorTop);
            Console.WriteLine(f);
            Console.SetCursorPosition((Console.WindowWidth - g.Length) / 2, Console.CursorTop);
            Console.WriteLine(g);
            Console.SetCursorPosition((Console.WindowWidth - h.Length) / 2, Console.CursorTop);
            Console.WriteLine(h);
            Console.SetCursorPosition((Console.WindowWidth - i.Length) / 2, Console.CursorTop);
            Console.WriteLine(i);
            Console.SetCursorPosition((Console.WindowWidth - j.Length) / 2, Console.CursorTop);
            Console.WriteLine(j);
            Console.SetCursorPosition((Console.WindowWidth - k.Length) / 2, Console.CursorTop);
            Console.WriteLine(k);
            Console.SetCursorPosition((Console.WindowWidth - l.Length) / 2, Console.CursorTop);
            Console.WriteLine(l);
            Console.SetCursorPosition((Console.WindowWidth - m.Length) / 2, Console.CursorTop);
            Console.WriteLine(m);
            Console.SetCursorPosition((Console.WindowWidth - n.Length) / 2, Console.CursorTop);
            Console.WriteLine(n);
            Console.SetCursorPosition((Console.WindowWidth - o.Length) / 2, Console.CursorTop);
            Console.WriteLine(o);
            Console.SetCursorPosition((Console.WindowWidth - p.Length) / 2, Console.CursorTop);
            Console.WriteLine(p);
            #endregion

            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
            }
            Console.Clear();
            //while (Fonctions.Run)
            {
                Fonctions.MenuPrincipal();
            }

            Console.Clear();
            #region stock dans les variables 
            string a2 = "Merci, et à bientôt !\n\n";
            string b2 = "                         ¶¶¶¶¶¶¶¶¶¶¶¶ ";
            string c2 = "                         ¶¶            ¶¶ ";
            string d2 = "           ¶¶¶¶¶        ¶¶                ¶¶ ";
            string e2 = "           ¶     ¶     ¶¶      ¶¶    ¶¶     ¶¶ ";
            string f2 = "            ¶     ¶    ¶¶       ¶¶    ¶¶      ¶¶ ";
            string g2 = "             ¶    ¶   ¶¶        ¶¶    ¶¶      ¶¶ ";
            string h2 = "              ¶   ¶   ¶                         ¶¶ ";
            string i2 = "            ¶¶¶¶¶¶¶¶¶¶¶¶                         ¶¶ ";
            string j2 = "           ¶            ¶    ¶¶            ¶¶    ¶¶ ";
            string k2 = "          ¶¶            ¶    ¶¶            ¶¶    ¶¶ ";
            string l2 = "         ¶¶   ¶¶¶¶¶¶¶¶¶¶¶      ¶¶        ¶¶     ¶¶ ";
            string m2 = "          ¶               ¶       ¶¶¶¶¶¶¶       ¶¶ ";
            string n2 = "         ¶¶              ¶                    ¶¶ ";
            string o2 = "          ¶   ¶¶¶¶¶¶¶¶¶¶¶¶                   ¶¶ ";
            string p2 = "          ¶¶           ¶  ¶¶                ¶¶ ";
            string q2 = "          ¶¶¶¶¶¶¶¶¶¶¶¶    ¶¶            ¶¶";
            string r2 = "                          ¶¶¶¶¶¶¶¶¶¶¶";
            #endregion

            #region Affiche toutes les variables
            Console.WriteLine("\n\n\n\n\n\n\n\n\n");
            Console.SetCursorPosition((Console.WindowWidth - a2.Length) / 2, Console.CursorTop);
            Console.WriteLine(a2);
            Console.SetCursorPosition((Console.WindowWidth - b2.Length) / 2, Console.CursorTop);
            Console.WriteLine(b2);
            Console.SetCursorPosition((Console.WindowWidth - c2.Length) / 2, Console.CursorTop);
            Console.WriteLine(c2);
            Console.SetCursorPosition((Console.WindowWidth - d2.Length) / 2, Console.CursorTop);
            Console.WriteLine(d2);
            Console.SetCursorPosition((Console.WindowWidth - e2.Length) / 2, Console.CursorTop);
            Console.WriteLine(e2);
            Console.SetCursorPosition((Console.WindowWidth - f2.Length) / 2, Console.CursorTop);
            Console.WriteLine(f2);
            Console.SetCursorPosition((Console.WindowWidth - g2.Length) / 2, Console.CursorTop);
            Console.WriteLine(g2);
            Console.SetCursorPosition((Console.WindowWidth - h2.Length) / 2, Console.CursorTop);
            Console.WriteLine(h2);
            Console.SetCursorPosition((Console.WindowWidth - i2.Length) / 2, Console.CursorTop);
            Console.WriteLine(i2);
            Console.SetCursorPosition((Console.WindowWidth - j2.Length) / 2, Console.CursorTop);
            Console.WriteLine(j2);
            Console.SetCursorPosition((Console.WindowWidth - k2.Length) / 2, Console.CursorTop);
            Console.WriteLine(k2);
            Console.SetCursorPosition((Console.WindowWidth - l2.Length) / 2, Console.CursorTop);
            Console.WriteLine(l2);
            Console.SetCursorPosition((Console.WindowWidth - m2.Length) / 2, Console.CursorTop);
            Console.WriteLine(m2);
            Console.SetCursorPosition((Console.WindowWidth - n2.Length) / 2, Console.CursorTop);
            Console.WriteLine(n2);
            Console.SetCursorPosition((Console.WindowWidth - o2.Length) / 2, Console.CursorTop);
            Console.WriteLine(o2);
            Console.SetCursorPosition((Console.WindowWidth - p2.Length) / 2, Console.CursorTop);
            Console.WriteLine(p2);
            Console.SetCursorPosition((Console.WindowWidth - q2.Length) / 2, Console.CursorTop);
            Console.WriteLine(q2);
            Console.SetCursorPosition((Console.WindowWidth - r2.Length) / 2, Console.CursorTop);
            Console.WriteLine(r2);
            #endregion

            Console.ReadKey();
        }
    }
}