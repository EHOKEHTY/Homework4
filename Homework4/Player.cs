using System.Collections.Generic;
using System;

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