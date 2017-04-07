using UnityEngine;
using System.Collections;

public class PhysicalCard : CustomBehaviour {

    public New_Card Card;
    public string Name;
    public int Cost;
    public New_Card.Color Color;

    public void AssignCard(New_Card card)
    {
        Card = card;
        Name = Card.Name;
        Cost = Card.Cost;
        Color = Card.CardColor;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
