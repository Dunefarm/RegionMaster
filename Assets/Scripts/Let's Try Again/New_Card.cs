using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class New_Card {

    public enum Color {Colorless, Red, Green, Blue };

    public int Cost = 1;
    public Color CardColor = Color.Colorless;
    public string Name = "Name of the card";
    public string CardText = "Card abilities.";
    public string CardFlavor = "Card flavor.";

    public PhysicalCard PhysicalCard;
    public CardAbility[] CardAbilities;

    public static Dictionary<PhysicalCard, New_Card> PhysicalToAbstractCard = new Dictionary<PhysicalCard, New_Card>();

    public void AddPhysicalCard(PhysicalCard phys)
    {
        PhysicalCard = phys;
        PhysicalToAbstractCard.Add(phys, this);
        CardAbilities = PhysicalCard.gameObject.GetComponents<CardAbility>();
    }

    public void RemovePhysicalCard()
    {
        PhysicalCard phys = PhysicalCard;
        PhysicalToAbstractCard.Remove(PhysicalCard);
        MonoBehaviour.Destroy(PhysicalCard.gameObject);
        PhysicalCard = null;
        CardAbilities = null;
    }

    public CardHolder CardHolder
    {
        get { return CardHolder.CardHolders[this]; }
    }

    public static New_Card CreatePlaceholderCard(int cost = 1, Color color = Color.Colorless, string name = "Placeholder Card")
    {
        New_Card card = new New_Card();
        card.Cost = cost;
        card.CardColor = color;
        card.Name = name;
        return card;
    }

    public void PlayCard()
    {

    }
}
