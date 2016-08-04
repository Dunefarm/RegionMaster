using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {

    public List<Card> Cards = new List<Card>();
    public Transform CardInHandPoint;

    public void PutCardInHand(Card newCard)
    {
        if (!Cards.Contains(newCard))
        {
            if (newCard.HandPosition < 0)
            {
                Cards.Add(newCard);
                newCard.HandPosition = Cards.Count - 1;
            }
            else
            {
                Cards.Insert(newCard.HandPosition, newCard);
            }
        }
        ArrangeCards();
    }

    public void RemoveCardInHand(Card theCard)
    {
        if (Cards.Contains(theCard))
        {
            print("Removed " + theCard.transform.name);
            Cards.Remove(theCard);
        }
        ArrangeCards();
    }

    void ArrangeCards()
    {
        float width = Cards.Count;
        Vector3 shift = Vector3.right * width * 0.5f + Vector3.back * 0.05f;
        float weight = 0;
        for(int i = 0; i < Cards.Count; i++)
        {
            if(Cards.Count > 1)
                weight = (float)i / (Cards.Count-1);
            Cards[i].transform.position = Vector3.Lerp(CardInHandPoint.position - shift, CardInHandPoint.position + shift, weight);
            Cards[i].transform.rotation = CardInHandPoint.rotation;
            Cards[i].HandPosition = i;
        }

    }

    public void DiscardHand()
    {
        foreach(Card card in Cards)
        {
            card.PutInDiscardPile();
        }
        Cards.Clear();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
