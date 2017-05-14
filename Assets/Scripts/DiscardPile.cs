using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiscardPile : MonoBehaviour {

    public List<Card> Cards = new List<Card>();

    private Player _owner;

    public void SetOwner(Player newOwner)
    {
        _owner = newOwner;
    }

    public void PutCardInDiscardPile(Card card)
    {
        card.DestroyPhysicalRepresentation();
        card.transform.position = Vector3.one * 1000;
        card.Owner = _owner;
        Cards.Add(card);
        ResizeDiscardPile();
    }

    void ResizeDiscardPile()
    {
        transform.localScale = new Vector3(1, 1, Cards.Count);
    }

    public void PutDiscardPileOnBottomOfDeck()
    {
        if (Cards.Count > 0)
        {
            foreach (Card card in Cards)
            {
                MegaManager.CurrentPlayer.Deck.AddCardToBottomOfDeck(card);
            }
        }
        Cards.Clear();
        ResizeDiscardPile();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
