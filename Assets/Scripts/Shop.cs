using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour {

    public Transform ShopSupplyPlacement;
    public GameObject CardPrefab; // for now...
    public Transform CardSockets;
    public Finite2DCoord ShopDimensions = new Finite2DCoord();

    public Card[] CardsOnDisplay;
    public Vector3[] CardPlacements;

    private Dictionary<Card, int> _cardIndex = new Dictionary<Card, int>();
    public static ShopSupply Supply;

	// Use this for initialization
	void Start ()
    {
        SetupShop();
        RefillShop();
	}

    void SetupShop()
    {
        SetupSupply();
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

    void SetupSupply()
    {
        ShopSupplyPlacement.gameObject.SetActive(false);
        GameObject supplyPrefab = Resources.Load("Prefabs/ShopSupply") as GameObject;
        Vector3 supplyPlacement = ShopSupplyPlacement.position;
        Supply = (MonoBehaviour.Instantiate(supplyPrefab, supplyPlacement, Quaternion.identity) as GameObject).GetComponent<ShopSupply>();
        Supply.transform.parent = transform;
    }

    void RefillShop()
    {
        for (int index = 0; index < CardPlacements.Length; index++)
        {
            if (CardsOnDisplay[index] != null) //Checks if there's already a card in this space
                continue;

            Card card = DrawCardFromSupply();
            if(card != null)
                PutCardInShopAt(card, index);
        }
    }

    private Card DrawCardFromSupply() //Creates placeholder cards for now. Should draw from an actual supply.
    {
        Card card = Supply.DrawCard();
        //GameObject cardPrefab = Resources.Load("Prefabs/Cards/Card") as GameObject;
        //GameObject obj = (GameObject)Instantiate(cardPrefab);
        //Card card = obj.GetComponent<Card>();
        //card.ManaCost = new ManaCost(1, 0, 0);
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
