//##################################################################
//#
//# Titre :       Crazy-Eight Automne 2020
//#               INF-1035 - Concepts avancés en objet
//#               Université du Québec à Trois-Rivières
//#
//# Auteurs :     Pascal Godin, David Mongeau, Frédérick Simard
//# Date :        Octobre 2020
//#
//# Langage :     C# 
//#
//##################################################################
using System;
using System.Text;
using inf1035_crazy_eights.Components;

namespace inf1035_crazy_eights.Board
{
    /*
    * Summary: this class contains the program, it is the point of entry to the program flow
    */
    class Program
    {
        //Main entry method
        private static void Main(string[] args)
        {
            Console.WriteLine("\n/////////////////////////////////");
            Console.WriteLine("Welcome to this Crazy-Eight game!");
            Console.WriteLine("/////////////////////////////////\n");

            const int PlayerCount = 4;
            const int NumberOfCards = 8;

            CardGame cg = new CardGame();
            cg.PlayersEvent += DisplayplayersEvent;

            for (int i = 0; i < PlayerCount; i++)
            {
                Random random = new Random();
                int nameSize = random.Next(4, 10);
                string firstName = GenName(nameSize);
                string lastName = GenName(nameSize);
                cg.RegisterPlayer(new Bot(firstName, lastName, id: i + 1));
            }

            cg.DistributeCards(NumberOfCards);
            cg.Play();
        }

        /*
        * Summary: this allows to Display a certain event
        * param name="sender": contains the reference to control that allow to raised the event
        * param name="e": the event data
        */
        private static void DisplayplayersEvent(object sender, PlayerEventArgs e)
        {
            switch (e.eventType)
            {
                case PlayerEventTypes.START_OF_TURN:
                    ConsoleHelper.PrintString($"{e.at} : It's {e.playerName} (Player {e.playerId}) turn\n", 2000);
                    break;
                case PlayerEventTypes.END_OF_TURN:
                    Console.WriteLine("\n///////////////////////////////////////////////////////////////////////");
                    Console.WriteLine("\n///////////////////////////////////////////////////////////////////////\n");
                    break;
                case PlayerEventTypes.WINNER:
                    Console.WriteLine("\n/////////////////");
                    ConsoleHelper.PrintString($"We have a Winner! {e.playerName}!", 500);
                    Console.WriteLine("/////////////////");
                    break;
            }
        }

        /*
        * Summary: this method allows to generate a name
        * param name="size": the size that the name needs to have
        * return: the string name
        */
        private static string GenName(int size)
        {
            string[] consone =
                {"b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z"};
            string[] vowel = {"a", "e", "ae", "o", "i", "u", "y"};

            Random rand = new Random();
            StringBuilder sb = new StringBuilder();
            string firstLetter = consone[rand.Next(0, consone.Length)].ToUpper();
            sb.Append(firstLetter);
            int nbLetters = 1;
            while (nbLetters < size)
            {
                sb.Append(vowel[rand.Next(0, vowel.Length)]);
                nbLetters++;
                sb.Append(consone[rand.Next(0, consone.Length)]);
                nbLetters++;
            }

            return sb.ToString();
        }
    }
}
