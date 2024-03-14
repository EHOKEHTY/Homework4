using System;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Homework4
{
    internal class Program
    {
        static Player player = new Player("Player");
        static Player computer = new Player("Computer");
        static Deck deck = new Deck();
        static int gamesPlayed = 0;
        static int yourScore = 0;
        static int computerScore = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the 21 card game!");


            while (true)
            {
                Console.WriteLine($"Games played: {gamesPlayed}");
                Console.WriteLine($"{player.Name} current score: {yourScore} / {computer.Name} current score: {computerScore}");
                Console.WriteLine("Press Enter to start a New Game or 'Q' to quit.");
                string input = Console.ReadLine();

                if (input.ToLower() == "q")
                {
                    Console.WriteLine("Thanks for playing");
                    Console.ReadLine();
                    break;
                }
                else if (string.IsNullOrEmpty(input))
                {
                    PlayGame(deck);
                }
                gamesPlayed++;
            }
        }

        static void PlayGame(Deck deck)
        {


            deck.ShuffleDeck();

            Console.WriteLine("\nDealing cards...");
            player.GetNewCard(deck.DealCard());
            player.GetNewCard(deck.DealCard());
            computer.GetNewCard(deck.DealCard());
            computer.GetNewCard(deck.DealCard());


            player.DisplayHand();

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Get another card");
                Console.WriteLine("2. Stop");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    player.GetNewCard(deck.DealCard());
                    player.DisplayHand();
                    if (player.Score > 21)
                    {
                        Console.WriteLine($"You busted! {computer.Name} wins!");
                        computerScore++;
                        break;
                    }
                    else if (player.Score == 21)
                    {
                        Console.WriteLine("Congratulations! You got 21! You win!");
                        yourScore++;
                        break;
                    }
                }
                else if (choice == "2")
                {
                    computer.DisplayHand();
                    while (computer.Score < 17)
                    {
                        computer.GetNewCard(deck.DealCard());
                        computer.DisplayHand();
                    }

                    if (computer.Score > 21 || computer.Score < player.Score)
                    {
                        Console.WriteLine($"{computer.Name} busted! You win!");
                        yourScore++;
                        break;
                    }
                    else if (computer.Score == player.Score || computer.Score > player.Score)
                    {
                        Console.WriteLine($"You busted! {computer.Name} wins!");
                        computerScore++;
                        break;
                    }
                }
            }
            player.HandOff();
            computer.HandOff();
            Console.WriteLine("---------------------------------------------------------------------");
        }
    }
}
