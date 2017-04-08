﻿using UnityEngine;
using System.Collections;

public class Player
{
    
    public int PlayerNumber;
    public Deck Deck;
    public DiscardPile DiscardPile;
    public Hand Hand;
    public ManaPool ManaPool;
    public Transform DiscardPileTranform;

    private Vector3 DECK_PLACEMENT = new Vector3(5.22f, -6.7f, 0);
    private Vector3 DISCARD_PILE_PLACEMENT = new Vector3(8.76f, -6.7f, 0);
    private Transform HAND_TRANSFORM;

    public Player()
    {
        PlayerNumber = -1;
        //empty placeholder object
    }

    public Player(int playerNo, GameObject discardPilePrefab, CameraManager camMan)
    {
        PlayerNumber = playerNo;
        GameObject deckPrefab = Resources.Load("Prefabs/Deck") as GameObject;
        Deck = (MonoBehaviour.Instantiate(deckPrefab, DECK_PLACEMENT, Quaternion.identity) as GameObject).GetComponent<Deck>();
        Deck.SetOwner(this);
        DiscardPile = (MonoBehaviour.Instantiate(discardPilePrefab, DISCARD_PILE_PLACEMENT, Quaternion.identity) as GameObject).GetComponent<DiscardPile>();
        DiscardPile.SetOwner(this);
        DiscardPileTranform = DiscardPile.transform;
        HAND_TRANSFORM = new GameObject().transform;
        HAND_TRANSFORM.position = camMan.transform.TransformPoint(-7.2f, -5.6f, 33.8f);
        HAND_TRANSFORM.rotation = camMan.transform.rotation;
        HAND_TRANSFORM.Rotate(30.12f, 0, 0, Space.Self);
        Hand = new Hand(HAND_TRANSFORM);
        EventManager.OnActivatePlayer += ActivatePlayer;
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActivatePlayer(int no)
    {
        if (no == PlayerNumber)
        {
            StartPlayersTurn();
        }
        else
        {
            EndPlayersTurn();
        }
    }

    public void DrawCard(int numberOfCards)
    {
        Deck.DrawCard(numberOfCards);
    }

    public void StartPlayersTurn()
    {
        Deck.gameObject.SetActive(true);
        DiscardPile.gameObject.SetActive(true);
    }

    public void EndPlayersTurn()
    {
        Deck.gameObject.SetActive(false);
        DiscardPile.gameObject.SetActive(false);
    }

    public bool IsActualPlayer()
    {
        return PlayerNumber != -1;
    }
}
