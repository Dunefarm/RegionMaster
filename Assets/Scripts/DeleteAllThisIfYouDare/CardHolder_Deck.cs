using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardHolder_Deck : CardHolder {

    public void ShuffleDeck()
    {
        int n = Cards.Count;
        while (n > 1)
        {
            n--;
            int randID = Random.Range(0, n);
            Card temp = Cards[n];
            Cards[n] = Cards[randID];
            Cards[randID] = temp;
        }
    }

    public Card DrawCard()
    {
        return PullCardOutAtIndex(0);
    }

    public List<Card> DrawCards(int amount)
    {
        return PullCardsOutAtIndex(0, amount);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
