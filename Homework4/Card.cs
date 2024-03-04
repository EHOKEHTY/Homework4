
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