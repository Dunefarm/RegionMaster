using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class MegaManager : MonoBehaviour {

    public GridManager GridMan;
    public CollectionManager ColMan;
    public GUIManager GUIMan;
    public CameraManager CamMan;
    public Shop Shop;
    public Markers Markers;
    public Hand Hand;
    public List<Deck> Decks = new List<Deck>();
    public Deck CurrentDeck;
    public List<DiscardPile> DiscardPiles = new List<DiscardPile>();
    public DiscardPile CurrentDiscardPile;
    public List<Player> Players = new List<Player>(); 

    public Token.OwnerTypes CurrentPlayer = Token.OwnerTypes.White;

    public void ChangeAmountOfMarkers(TokenMarkers markers)
    {
        Markers.Amount = markers;

    }

    public int PlayerNo
    {
        get { return _playerNo; }
        set
        {
            if (value < 0 || value > 1)
                return;
            _playerNo = value;
            CurrentDeck.gameObject.SetActive(false);
            CurrentDiscardPile.gameObject.SetActive(false);
            CurrentDeck = Decks[value];
            CurrentDiscardPile = DiscardPiles[value];
            CurrentDeck.gameObject.SetActive(true);
            CurrentDiscardPile.gameObject.SetActive(true);
            switch (value)
            {
                case 0:
                    GUIMan.PlayerTurnIcon.color = Color.white;
                    CurrentPlayer = Token.OwnerTypes.White;
                    break;
                case 1:
                    GUIMan.PlayerTurnIcon.color = Color.black;
                    CurrentPlayer = Token.OwnerTypes.Black;
                    break;
                default:
                    GUIMan.PlayerTurnIcon.color = Color.cyan;
                    break;
            }
        }
    }

    private int _playerNo = 0;

    public TurnPhases TurnPhases;

    void Awake()
    {
        EventManager.OnTurnPhaseChange += OnTurnPhaseChange;
        TurnPhases = gameObject.AddComponent<TurnPhases>();
    }

    void Start()
    {
        CurrentDeck = Decks[0];
        CurrentDiscardPile = DiscardPiles[0];
        PlayerNo = 0;
        EventManager.ChangeTurnPhase(TurnPhase.Beginning);
        EventManager.CallOnSomethingChange();
    }

    void Update()
    {

    }
    public void NextTurn()
    {
        ColMan.CleanUp();
        GridMan.FillGrid();
        PlayerNo = (PlayerNo + 1) % 2;
        EventManager.ChangeTurnPhase(TurnPhase.End);
    }

    void DrawCard(int amount)
    {
        CurrentDeck.DrawCard(amount);
    }

    public void OnTurnPhaseChange(TurnPhase newPhase)
    {
        switch (newPhase)
        {
            case TurnPhase.Beginning:
                DrawCard(3);
                break;
            case TurnPhase.Place:
                break;
            case TurnPhase.Buy:
                break;
            case TurnPhase.End:
                Markers.ClearMarkers();
                Hand.DiscardHand();
                break;
        }
    }
}
