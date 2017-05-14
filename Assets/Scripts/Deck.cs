using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Deck : MonoBehaviour {

    //Essentially starter deck:
    public List<GameObject> CardObjects = new List<GameObject>();

    //[HideInInspector]
    public List<Card> Cards = new List<Card>();
    public int OwnerNumber = -1;
    public Player Owner;

	// Use this for initialization
	void Awake ()
    {
        CreateStarterDeck();
        ShuffleDeck();
	}

    protected virtual void CreateStarterDeck()
    {
        foreach (GameObject cardObj in CardObjects)
        {
            GameObject obj = (GameObject)Instantiate(cardObj);
            Card card = obj.GetComponent<Card>();
            card.OwnerNumber = OwnerNumber;
            card.SetOwner(Owner);
            Cards.Add(card);
        }
    }

    public virtual void SetOwner(Player player)
    {
        Owner = player;
        OwnerNumber = Owner.PlayerNumber;
    }

    public virtual void ShuffleDeck()
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

    public virtual Card DrawCard()
    {
        if (Cards.Count < 1)
            ShuffleDiscardPileIntoDeck();
        if (Cards.Count < 1)
            return null;
        return PullCardAt(0);
    }

    public virtual List<Card> DrawCards(int amount)
    {
        List<Card> drawnCards = new List<Card>();
        while (amount > 0)
        {
            Card card = DrawCard();
            if (card == null)
                break;
            drawnCards.Add(card);
            amount--;
        }
        return drawnCards;
    }

    public virtual Card PullCardAt(int index)
    {
        if (index < 0 || index > Cards.Count)
            return null;
        Card card = Cards[index];
        Cards.RemoveAt(index);
        ResizeDeck();
        return card;
    }

    protected virtual void ShuffleDiscardPileIntoDeck()
    {
        Owner.DiscardPile.PutDiscardPileOnBottomOfDeck();
        ShuffleDeck();
    }

    public virtual void AddCardToBottomOfDeck(Card card)
    {
        Cards.Add(card);
    }

    protected virtual void ResizeDeck()
    {
        transform.localScale = new Vector3(1, 1, Cards.Count);
    }
}
