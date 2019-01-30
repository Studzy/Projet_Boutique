using System;
using BoutiqueBDDLibrary;

namespace BoutiqueConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("                                          _______________________________  ");
            Console.WriteLine("                                         [=U=U=U=U=U=U=U=U=U=U=U=U=U=U=U=] ");
            Console.WriteLine("                                         |.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.| ");
            Console.WriteLine("                                         |        +-+-+-+-+-+-+-+        | ");
            Console.WriteLine("                                         |        |   BOUTIQUE  |        | ");
            Console.WriteLine("                                         |        +-+-+-+-+-+-+-+        | ");
            Console.WriteLine("                                         |.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.| ");
            Console.WriteLine("                                         |  _________  __ __  _________  | ");
            Console.WriteLine("                                       _ | |___   _  ||[]|[]||  _      | | _ ");
            Console.WriteLine("                                      (!)||OPEN|_(!)_|| ,| ,||_(!)_____| |(!) ");
            Console.WriteLine("                                      .T~T|:.....:T~T.:|__|__|:.T~T.:....:|T~T. ");
            Console.WriteLine("                                     || _||||||||||_|||||||||||||_||||||||||_|| ");
            Console.WriteLine("                                     ~\\=/~~~~~~~~\\=/~~~~~~~~~~~\\=/~~~~~~~~\\=/~ ");
            Console.WriteLine("                                       | -------- | ----------- | -------- | ");
            Console.WriteLine("                                     ~ |~^ ^~~^ ~~| ~^  ~~ ^~^~ |~ ^~^ ~~^ |^~ \n\n");
            Console.WriteLine("                                  APPUYER SUR ENTRER POUR ENTRER DANS LA BOUTIQUE");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
            }
            Console.Clear();
            Fonctions.MenuPrincipal();
        }
    }
}