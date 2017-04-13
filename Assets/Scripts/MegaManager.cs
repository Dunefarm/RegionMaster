﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class MegaManager : MonoBehaviour
{

    public GameObject DeckPrefab;
    public GameObject DiscardPilePrefab;
    public LayerMask TableLayerMask;

    public GUIManager GUIMan;
    public CameraManager CamMan;
    public AbilityResolver AbilityResolver;
    public static Shop Shop;
    public static Markers Markers;
    public List<Deck> Decks = new List<Deck>();
    public List<DiscardPile> DiscardPiles = new List<DiscardPile>();
    public static List<Player> Players = new List<Player>();

    public static TurnPhases TurnPhases;
    public static CollectionManager CollectionManager;

    private int NUMBER_OF_PLAYERS = 2;
    private static int _currentPlayerNumber = -1;

    //public Token.OwnerTypes CurrentPlayer = Token.OwnerTypes.White;

    public static Player CurrentPlayer
    {
        get { return Players[_currentPlayerNumber]; }
    }

    public static Hand Hand
    {
        get { return CurrentPlayer.Hand; }
    }

    public void SetAmountOfMarkers(ManaCost markers)
    {
        Markers.Amount = markers;

    }

    public int CurrentPlayerNumber
    {
        get { return _currentPlayerNumber; }
    }

    void Awake()
    {
        EventManager.OnTurnPhaseBegin += OnTurnPhaseBegin;
        TurnPhases = gameObject.AddComponent<TurnPhases>();
        CollectionManager = new CollectionManager(this);
    }

    void Start()
    {
        SetupAbilityResolver();
        SetupTable();
        //SetUpGrid();
        //SetupMarkerHolder();
        SetupPlayers();

        StartGame();        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print(GridManager.TokenBag.Tokens.Count);
        }
    }

    void StartGame()
    {
        EventManager.ActivatePlayer(_currentPlayerNumber);
        EventManager.TryChangeTurnPhase(TurnPhase.Beginning);
    }

    void SetupAbilityResolver()
    {
        AbilityResolver = new AbilityResolver();
    }

    void SetupTable()
    {
        GameObject table = Instantiate(Resources.Load("Prefabs/Table")) as GameObject;

        GridManager.AssignGrid(table.GetComponentInChildren<Grid>());
        GridManager.SetupNewGame();

        Shop = table.GetComponentInChildren<Shop>();
        Markers = table.GetComponentInChildren<Markers>();
    }

    void SetUpGrid()
    {
        Grid grid = Instantiate(Resources.Load("Prefabs/Grid") as GameObject).GetComponent<Grid>();
        GridManager.AssignGrid(grid);
        GridManager.SetupNewGame();
    }

    void SetupMarkerHolder()
    {
        Markers markers = Instantiate(Resources.Load("Prefabs/MarkerHolder") as GameObject).GetComponent<Markers>();
        Markers = markers;
    }

    void SetupPlayers()
    {
        for (int ID = 0; ID < NUMBER_OF_PLAYERS; ID++)
        {
            Player player = new Player(ID, DiscardPilePrefab, CamMan);
            Players.Add(player);
        }
    }

    public void NextTurn()
    {
        List<Token> tokens = CollectionManager.PullOutAllTokens();
        GridManager.TokenBag.AddTokens(tokens);
        GridManager.RefillGrid();
        EventManager.TryChangeTurnPhase(TurnPhase.End);
    }

    public void OnTurnPhaseBegin(TurnPhase newPhase)
    {
        if (newPhase == TurnPhase.Beginning)
            BeginningOfTurn();

        else if (newPhase == TurnPhase.End)
            EndOfTurn();
    }

    void BeginningOfTurn()
    {
        NextPlayer();
        CurrentPlayerDrawsCards(3);
    }

    void NextPlayer()
    {
        _currentPlayerNumber = (_currentPlayerNumber + 1) % 2;
        EventManager.ActivatePlayer(_currentPlayerNumber);
    }

    private void CurrentPlayerDrawsCards(int amount)
    {
        Players[_currentPlayerNumber].DrawCard(amount);
    }

    void EndOfTurn()
    {
        Markers.ClearMarkers();
        Players[_currentPlayerNumber].Hand.DiscardHand();
    }
}
