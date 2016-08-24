﻿using UnityEngine;
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
        card.transform.position = Vector3.one * 1000;
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
                card.PutInDeck();
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
