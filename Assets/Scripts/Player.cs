using UnityEngine;
using System.Collections;

public class Player
{

    public int Number;
    public Deck Deck;
    public DiscardPile DiscardPile;
    public Hand Hand;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartPlayersTurn()
    {
        Deck.gameObject.SetActive(true);
        DiscardPile.gameObject.SetActive(true);
    }

    public void EndPlayersTurn()
    {
        Deck.gameObject.SetActive(true);
        DiscardPile.gameObject.SetActive(true);
    }
}
