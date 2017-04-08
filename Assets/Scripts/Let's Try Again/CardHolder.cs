using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CardHolder {

    public List<New_Card> Cards = new List<New_Card>();
    public static Dictionary<New_Card, CardHolder> CardHolders = new Dictionary<New_Card, CardHolder>();

    public virtual New_Card PullCardOut(New_Card card)
    {
        if (Cards.Count == 0 || !Cards.Contains(card))
            return null;

        Cards.Remove(card);
        CardHolders.Remove(card);
        return card;
    }

    public virtual List<New_Card> PullCardsOut(List<New_Card> cards)
    {
        if (cards == null)
            return null;
        if (cards.Count < 1 || cards.Except(Cards).Count() != 0) // The cards needed are not (all) in the holder
            return null;

        List<New_Card> newCards = new List<New_Card>();
        for (int i = 0; i < cards.Count; i++)
        {
            newCards.Add(PullCardOut(cards[i]));
        }

        return newCards;
    }

    public virtual New_Card PullCardOutAtIndex(int index)
    {
        if (Cards.Count == 0 || index > Cards.Count || index < 0)
            return null;

        New_Card card = Cards[index];
        Cards.Remove(card);
        return card;
    }

    public virtual List<New_Card> PullCardsOutAtIndex(int index, int amount, bool ignoreOutOfRange = true)
    {
        if (index > Cards.Count || index < 0)
            return null;
        if (!ignoreOutOfRange && index + amount > Cards.Count)
            return null;

        List<New_Card> cards = Cards.GetRange(index, amount);
        return PullCardsOut(cards);
    }

    public virtual void AddCard(New_Card card)
    {
        if (card == null || Cards.Contains(card))
            return;

        CardHolders.Add(card, this);
        Cards.Add(card);
    }

    public virtual void InsertCard(New_Card card, int atIndex)
    {
        if (card == null)
            return;

        if (Cards.Contains(card))
            return;

        if (atIndex > Cards.Count || atIndex < 0)
            return;

        CardHolders.Add(card, this);
        Cards.Insert(atIndex, card);
    }

    public void AddCards(List<New_Card> cards)
    {
        if (cards == null)
            return;
        foreach (New_Card card in cards)
            AddCard(card);
    }

    public void InsertCards(List<New_Card> cards, int atIndex)
    {
        if (cards == null)
            return;
        foreach (New_Card card in cards)
        {
            InsertCard(card, atIndex);
            atIndex++;
        }
    }

    public int GetCardIndex(New_Card card)
    {
        if (card == null)
            return -1;
        return Cards.IndexOf(card);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
