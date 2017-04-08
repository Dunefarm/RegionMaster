using UnityEngine;
using System.Collections;

public class New_Card {

    public enum Color {Colorless, Red, Green, Blue };

    public int Cost = 1;
    public Color CardColor = Color.Colorless;
    public string Name = "Name of the card";
    public string CardText = "Card abilities.";
    public string CardFlavor = "Card flavor.";

    public CardHolder CardHolder
    {
        get { return CardHolder.CardHolders[this]; }
    }

    public static New_Card CreatePlaceHolderCard(int cost = 1, Color color = Color.Colorless, string name = "Placeholder Card")
    {
        New_Card card = new New_Card();
        card.Cost = cost;
        card.CardColor = color;
        card.Name = name;
        return card;
    }
}
