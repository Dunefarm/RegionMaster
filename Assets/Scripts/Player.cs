using UnityEngine;
using System.Collections;

public class Player
{

    public int Number;
    public Deck Deck;
    public DiscardPile DiscardPile;
    public Hand Hand;

    private Vector3 DECK_PLACEMENT = new Vector3(5.22f, -6.7f, 0);
    private Vector3 DISCARD_PILE_PLACEMENT = new Vector3(8.76f, -6.7f, 0);

    private MegaManager _megaManager;

    public Player(MegaManager megaMan, int playerNo)
    {
        _megaManager = megaMan;
        Number = playerNo;
    }

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
