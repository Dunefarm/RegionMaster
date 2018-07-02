using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CardHolder {

    public List<Card> Cards = new List<Card>();
    public static Dictionary<Card, CardHolder> CardHolders = new Dictionary<Card, CardHolder>();

    public virtual Card PullCardOut(Card card)
    {
        if (Cards.Count == 0 || !Cards.Contains(card))
            return null;

        Cards.Remove(card);
        CardHolders.Remove(card);
        return card;
    }

    public virtual List<Card> PullCardsOut(List<Card> cards)
    {
        if (cards == null)
            return null;
        if (cards.Count < 1 || cards.Except(Cards).Count() != 0) // The cards needed are not (all) in the holder
            return null;

        List<Card> newCards = new List<Card>();
        for (int i = 0; i < cards.Count; i++)
        {
            newCards.Add(PullCardOut(cards[i]));
        }

        return newCards;
    }

    public virtual Card PullCardOutAtIndex(int index)
    {
        if (Cards.Count == 0 || index > Cards.Count || index < 0)
            return null;

        Card card = Cards[index];
        Cards.Remove(card);
        return card;
    }

    public virtual List<Card> PullCardsOutAtIndex(int index, int amount, bool ignoreOutOfRange = true)
    {
        if (index > Cards.Count || index < 0)
            return null;
        if (!ignoreOutOfRange && index + amount > Cards.Count)
            return null;

        List<Card> cards = Cards.GetRange(index, amount);
        return PullCardsOut(cards);
    }

    public virtual void AddCard(Card card)
    {
        if (card == null || Cards.Contains(card))
            return;

        CardHolders.Add(card, this);
        Cards.Add(card);
    }

    public virtual void InsertCard(Card card, int atIndex)
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

    public void AddCards(List<Card> cards)
    {
        if (cards == null)
            return;
        foreach (Card card in cards)
            AddCard(card);
    }

    public void InsertCards(List<Card> cards, int atIndex)
    {
        if (cards == null)
            return;
        foreach (Card card in cards)
        {
            InsertCard(card, atIndex);
            atIndex++;
        }
    }

    public int GetCardIndex(Card card)
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
