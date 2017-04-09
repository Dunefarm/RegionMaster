using UnityEngine;
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
    public AbilityResolver AbilityResolver;
    public Shop Shop;
    public static Markers Markers;
    public Hand Hand;
    public List<Deck> Decks = new List<Deck>();
    public List<DiscardPile> DiscardPiles = new List<DiscardPile>();
    public static List<Player> Players = new List<Player>();

    public static TurnPhases TurnPhases;
    public CollectionManager CollectionManager;

    private int NUMBER_OF_PLAYERS = 2;
    private static int _currentPlayerNumber = -1;

    //public Token.OwnerTypes CurrentPlayer = Token.OwnerTypes.White;

    public static Player CurrentPlayer
    {
        get { return Players[_currentPlayerNumber]; }
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
        EventManager.OnTurnPhaseBegin += OnTurnPhaseChange;
        TurnPhases = gameObject.AddComponent<TurnPhases>();
        CollectionManager = new CollectionManager(this);
    }

    void Start()
    {
        SetupAbilityResolver();
        SetUpGrid();
        SetupMarkerHolder();
        SetupPlayers();

        EventManager.ActivatePlayer(_currentPlayerNumber);
        EventManager.TryChangeTurnPhase(TurnPhase.Beginning);
        EventManager.CallOnSomethingChange();
    }

    void SetupAbilityResolver()
    {
        AbilityResolver = new AbilityResolver();
    }

    void SetUpGrid()
    {
        Grid grid = Instantiate(Resources.Load("Prefabs/Grid") as GameObject).GetComponent<Grid>();
        GridMan = new GridManager(grid);
        GridMan.BeginNewGame();
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
        CollectionManager.CleanUp();
        GridMan.RefillGrid();
        EventManager.TryChangeTurnPhase(TurnPhase.End);
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
