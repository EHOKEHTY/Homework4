using System.Collections.Generic;
using System;

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