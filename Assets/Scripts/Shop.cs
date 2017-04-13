using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour {

    public GameObject CardPrefab; // for now...
    public Transform CardSockets;
    public Finite2DCoord ShopDimensions = new Finite2DCoord();

    public Card[] CardsOnDisplay;
    public Vector3[] CardPlacements;

    private Dictionary<Card, int> _cardIndex = new Dictionary<Card, int>();

	// Use this for initialization
	void Start ()
    {
        SetupShop();
        RefillShop();
	}

    void SetupShop()
    {
        int sockets = CardSockets.childCount;
        CardPlacements = new Vector3[sockets];
        CardsOnDisplay = new Card[sockets];
        for (int i = sockets - 1; i >= 0; i--)
        {
            CardPlacements[i] = CardSockets.GetChild(i).position;
            Destroy(CardSockets.GetChild(i).gameObject);
        }
        Destroy(CardSockets.gameObject);
    }

    void RefillShop()
    {
        for (int index = 0; index < CardPlacements.Length; index++)
        {
            if (CardsOnDisplay[index] != null) //Already a card in this space
                continue;

            Card card = DrawCardFromSupply();
            PutCardInShopAt(card, index);
            //card.transform.parent = transform; // for now...
        }
    }

    private Card DrawCardFromSupply() //Creates placeholder cards for now. Should draw from an actual supply.
    {
        GameObject cardPrefab = Resources.Load("Prefabs/Cards/Card") as GameObject;
        GameObject obj = (GameObject)Instantiate(cardPrefab);
        Card card = obj.GetComponent<Card>();
        return card;
    }

    public Vector3 CoordToVector3(Finite2DCoord coord)
    {
        return new Vector3(coord.x * 3.35f, coord.y * -4.45f, 0) + transform.position;
    }

    public void PutCardInShopAt(Card card, int index)
    {
        if (CardsOnDisplay[index] != null)
            return;

        CardsOnDisplay[index] = card;
        InstantiatePhysicalCardForShop(card);
        card.transform.position = CardPlacements[index];
        _cardIndex.Add(card, index);
    }

    void InstantiatePhysicalCardForShop(Card card)
    {
        GameObject prefab = Resources.Load("Prefabs/Cards/PhysicalCard") as GameObject;
        GameObject obj = (GameObject)MonoBehaviour.Instantiate(prefab);
        PhysicalCard tempCard = obj.AddComponent<PhysicalCard_Shop>();
        card.AssignPhysicalRepresentation(tempCard);
    }

    public Card PullCardFromShop(Card card)
    {
        CardsOnDisplay[_cardIndex[card]] = null;
        _cardIndex.Remove(card);
        RefillShop(); //Might be too early for this...
        return card;
    }

    public void ReturnCard(Card card)
    {
        if (!_cardIndex.ContainsKey(card))
            return;

        card.transform.position = CardPlacements[_cardIndex[card]];
    }
}
