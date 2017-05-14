using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopSupply : Deck {

    protected override void CreateStarterDeck()
    {
        foreach (GameObject cardObj in CardObjects)
        {
            GameObject obj = (GameObject)Instantiate(cardObj);
            Card card = obj.GetComponent<Card>();
            Cards.Add(card);
        }
    }

    protected override void ShuffleDiscardPileIntoDeck()
    {
        
    }
}
