﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class MegaManager : MonoBehaviour
{

    public GameObject DeckPrefab;
    public GameObject DiscardPilePrefab;

    public GridManager GridMan;
    public GUIManager GUIMan;
    public CameraManager CamMan;
    public Shop Shop;
    public Markers Markers;
    public Hand Hand;
    public List<Deck> Decks = new List<Deck>();
    public List<DiscardPile> DiscardPiles = new List<DiscardPile>();
    public List<Player> Players = new List<Player>();

    public TurnPhases TurnPhases;
    public CollectionManager CollectionManager;

    private int NUMBER_OF_PLAYERS = 2;
    private int _currentPlayerNumber = -1;

    //public Token.OwnerTypes CurrentPlayer = Token.OwnerTypes.White;

    public Player CurrentPlayer
    {
        get { return Players[_currentPlayerNumber]; }
    }

    public void ChangeAmountOfMarkers(TokenMarkers markers)
    {
        Markers.Amount = markers;

    }

    public int CurrentPlayerNumber
    {
        get { return _currentPlayerNumber; }
    }

    void Awake()
    {
        EventManager.OnTurnPhaseChange += OnTurnPhaseChange;
        TurnPhases = gameObject.AddComponent<TurnPhases>();
        CollectionManager = new CollectionManager(this);
    }

    void Start()
    {
        for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
        {
            Player player = new Player(this, i, DeckPrefab, DiscardPilePrefab, CamMan);
            Players.Add(player);
        }
        EventManager.ActivatePlayer(_currentPlayerNumber);
        EventManager.ChangeTurnPhase(TurnPhase.Beginning);
        EventManager.CallOnSomethingChange();
    }

    void Update()
    {

    }
    public void NextTurn()
    {
        CollectionManager.CleanUp();
        GridMan.RefillGrid();
        EventManager.ChangeTurnPhase(TurnPhase.End);
    }

    private void DrawCard(int amount)
    {
        Players[_currentPlayerNumber].DrawCard(amount);
    }

    public void OnTurnPhaseChange(TurnPhase newPhase)
    {
        switch (newPhase)
        {
            case TurnPhase.Beginning: //This might result in some stack problems!!
                _currentPlayerNumber = (_currentPlayerNumber + 1) % 2;
                EventManager.ActivatePlayer(_currentPlayerNumber);
                DrawCard(3);
                break;
            case TurnPhase.Place:
                break;
            case TurnPhase.Buy:
                break;
            case TurnPhase.End:
                Markers.ClearMarkers();
                Players[_currentPlayerNumber].Hand.DiscardHand();
                break;
        }
    }
}
