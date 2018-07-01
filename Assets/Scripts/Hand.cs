using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand
{
    public static Dictionary<Card, Hand> WhoHoldsThisCard = new Dictionary<Card, Hand>();
    public List<Card> Cards = new List<Card>();
    public Transform CardInHandPoint;

    public Hand(Transform handTrans)
    {
        CardInHandPoint = handTrans;
        EventManager.Phases.EndOfTurn_OnEnter += AtEndOfTurn;
    }

    public void PutCardsInHand(List<Card> cards)
    {
        foreach(Card card in cards)
        {
            PutCardInHand(card);
        }
    }

    public void PutCardInHand(Card card)
    {
        if (!Cards.Contains(card))
        {
            InstantiatePhysicalCardForHand(card);
            Cards.Add(card);
            WhoHoldsThisCard.Add(card, this);
        }
        RearrangeCards();
    }

    void InstantiatePhysicalCardForHand(Card card)
    {
        GameObject prefab = Resources.Load("Prefabs/Cards/PhysicalCard") as GameObject;
        GameObject obj = (GameObject)MonoBehaviour.Instantiate(prefab);
        PhysicalCard tempCard = obj.AddComponent<PhysicalCard_Hand>();
        card.AssignPhysicalRepresentation(tempCard);
    }

    public Card PullCardOutofHand(Card card)
    {
        if (Cards.Contains(card))
        {
            Cards.Remove(card);
            WhoHoldsThisCard.Remove(card);
            return card;
        }
        RearrangeCards();
        return null;
    }

    void RearrangeCards()
    {
        float width = Cards.Count;
        Vector3 shift = Vector3.right*width*0.5f + Vector3.back*0.05f;
        float weight = 0;
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards.Count > 1)
                weight = (float) i/(Cards.Count - 1);
            Cards[i].transform.position = Vector3.Lerp(CardInHandPoint.position - shift,
                CardInHandPoint.position + shift, weight);
            Cards[i].transform.rotation = CardInHandPoint.rotation;
        }

    }

    public void DiscardHand()
    {
        foreach (Card card in Cards)
        {
            MegaManager.CurrentPlayer.DiscardPile.PutCardInDiscardPile(card);
        }
        Cards.Clear();
        WhoHoldsThisCard.Clear();
    }

    public void AtEndOfTurn()
    {
        //DiscardHand();
    }
}
