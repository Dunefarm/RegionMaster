using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Deck : MonoBehaviour {

    //Essentially starter deck:
    public List<GameObject> CardObjects = new List<GameObject>();

    //[HideInInspector]
    public List<Card> Cards = new List<Card>();
    public MegaManager MegaMan;
    public int OwnerNo = -1;
    public Player Owner;

	// Use this for initialization
	void Awake ()
    {
        MegaMan = FindObjectOfType<MegaManager>();
        foreach (GameObject cardObj in CardObjects)
        {
            GameObject obj = (GameObject)Instantiate(cardObj);
            Card tempCard = obj.GetComponent<Card>();
            tempCard.OwnerNo = OwnerNo;
            tempCard.SetOwner(Owner);
            Cards.Add(tempCard);
        }
        ShuffleDeck();
	}

    public void SetOwner(Player player)
    {
        Owner = player;
        OwnerNo = Owner.PlayerNumber;
    }

    public void ShuffleDeck()
    {
        int n = Cards.Count;
        while(n > 1)
        {
            n--;
            int randID = Random.Range(0, n);
            Card temp = Cards[n];
            Cards[n] = Cards[randID];
            Cards[randID] = temp;
        }
        ResizeDeck();
    }

    public void DrawCard(int amount)
    {
        while(amount > 0)
        {
            if (Cards.Count < 1)
                ShuffleDiscardPileIntoDeck();
            if (Cards.Count < 1)
                break;
            Card card = PullCardAt(0);
            MegaManager.Hand.PutCardInHand(card);
            amount--;
        }
        ResizeDeck();
    }

    public Card PullCardAt(int index)
    {
        if (index < 0 || index > Cards.Count)
            return null;
        Card card = Cards[index];
        Cards.RemoveAt(index);
        return card;
    }

    void ShuffleDiscardPileIntoDeck()
    {
        Owner.DiscardPile.PutDiscardPileOnBottomOfDeck();
        ShuffleDeck();
    }

    public void AddCardToBottomOfDeck(Card card)
    {
        card.HandPosition = -1;
        card.CurrentLocation = Card.CardLocation.Deck;
        Cards.Add(card);
    }

    void ResizeDeck()
    {
        transform.localScale = new Vector3(1, 1, Cards.Count);
    }
}
