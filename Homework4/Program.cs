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

    public struct Card
    {
        public string Suit { get; }
        public string Rank { get; }
        public int Weight { get; }

        public Card(string suit, string rank, int weight)
        {
            Suit = suit;
            Rank = rank;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}, card weight {Weight}";
        }
    }

    public class Deck
    {
        private string[] ranks = { "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
        private string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        private int[] weights = { 6, 7, 8, 9, 10, 2, 3, 4, 1 };
        private List<Card> currentDeck = new List<Card>();
        private Random random = new Random();

        public List<Card> Cards
        {
            get { return currentDeck; }
        }

        public Deck()
        {
            currentDeck = GenerateDeck();
        }

        private List<Card> GenerateDeck()
        {
            List<Card> deck = new List<Card>();
            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; j++)
                {
                    deck.Add(new Card(suits[i], ranks[j], weights[j]));
                }
            }
            return deck;
        }

        public Card DealCard()
        {
            if (currentDeck.Count == 0)
            {
                Console.WriteLine("Out of cards. The deck will be reshuffled.");
                currentDeck = GenerateDeck();
                ShuffleDeck();
            }
            Card toDeal = currentDeck[0];
            currentDeck.Remove(toDeal);
            return toDeal;
        }

        public void ShuffleDeck()
        {
            int n = currentDeck.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card value = currentDeck[k];
                currentDeck[k] = currentDeck[n];
                currentDeck[n] = value;
            }
        }
    }

    internal class Player
    {
        int score = 0;
        internal List<Card> Hand { get; }
        public int Score { get { return score; } }
        public string Name { get; }

        public Player(string name)
        {
            Hand = new List<Card>();
            Name = name;
        }

        public void GetNewCard(Card card)
        {
            Hand.Add(card);
            score += card.Weight;
        }

        public void HandOff()
        {
            Hand.Clear();
            score = 0;
        }
        public void DisplayHand()
        {
            Console.WriteLine($"{Name}`s hand: ");
            foreach (Card card in Hand)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine();
        }
    }
}
