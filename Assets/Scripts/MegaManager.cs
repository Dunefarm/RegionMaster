using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum TurnPhase {Beginning, Place, Buy, End}

public class MegaManager : MonoBehaviour {

    public TurnPhase CurrentTurnPhase
    {
        get { return _currentTurnPhase; }
        set
        {
            GUIMan.ButtonCollectObj.SetActive(false);
            GUIMan.ButtonNextTurnObj.SetActive(false);
            _currentTurnPhase = value;
            switch (value)
            {
                case TurnPhase.Beginning:
                    DrawCard(3);
                    CurrentTurnPhase = TurnPhase.Place;
                    break;
                case TurnPhase.Place:
                    GUIMan.ButtonCollectObj.SetActive(true);
                    break;
                case TurnPhase.Buy:
                    GUIMan.ButtonNextTurnObj.SetActive(true);
                    break;
                case TurnPhase.End:
                    Markers.ClearMarkers();
                    Hand.DiscardHand();
                    CurrentTurnPhase = TurnPhase.Beginning;
                    break;
            }
            print(_currentTurnPhase.ToString());
        }
    }

    public GridManager GridMan;
    public CollectionManager ColMan;
    public GUIManager GUIMan;
    public CameraManager CamMan;
    public Shop Shop;
    public Markers Markers;
    public Hand Hand;
    public List<Deck> Decks = new List<Deck>();
    //public Deck Deck1;
    //public Deck Deck2;
    public Deck CurrentDeck;
    public List<DiscardPile> DiscardPiles = new List<DiscardPile>();
    //public DiscardPile DiscardPile1;
    //public DiscardPile DiscardPile2;
    public DiscardPile CurrentDiscardPile;

    public Token.OwnerTypes CurrentPlayer = Token.OwnerTypes.White;
    //public TokenMarkers Markers
    //{
    //    get { return MarkerMan.Amount; }
    //    set { MarkerMan.Amount = value; }
    //}

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
    private TurnPhase _currentTurnPhase = TurnPhase.Beginning;


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

    public void SetCurrentTurnPhase(TurnPhase newPhase)
    {
        _currentTurnPhase = newPhase;
        switch (newPhase)
        {
            case TurnPhase.Beginning:
                SetCurrentTurnPhaseToBeginning();
                break;
            case TurnPhase.Place:
                SetCurrentTurnPhaseToPlace();
                break;
            case TurnPhase.Buy:
                SetCurrentTurnPhaseToBuy();
                break;
            case TurnPhase.End:
                SetCurrentTurnPhaseToEnd();
                break;
        }
    }

    public void SetCurrentTurnPhaseToBeginning()
    {
        _currentTurnPhase = TurnPhase.Beginning;
        DrawCard(3);
    }

    public void SetCurrentTurnPhaseToPlace()
    {
        _currentTurnPhase = TurnPhase.Place;

    }

    public void SetCurrentTurnPhaseToBuy()
    {
        _currentTurnPhase = TurnPhase.Buy;
    }

    public void SetCurrentTurnPhaseToEnd()
    {
        _currentTurnPhase = TurnPhase.End;
        Markers.ClearMarkers();
        Hand.DiscardHand();
    }

    public void NextTurnPhase()
    {
        if(_currentTurnPhase == TurnPhase.Beginning)
            EventManager.ChangeTurnPhase(TurnPhase.Place);
        else if(_currentTurnPhase == TurnPhase.Place)
            EventManager.ChangeTurnPhase(TurnPhase.Buy);
        else if(_currentTurnPhase == TurnPhase.Buy)
            EventManager.ChangeTurnPhase(TurnPhase.End);
        else if(_currentTurnPhase == TurnPhase.End)
            EventManager.ChangeTurnPhase(TurnPhase.Beginning);
    }

    void Awake()
    {
        EventManager.OnTurnPhaseChange += SetCurrentTurnPhase;
    }

    void Start()
    {
        EventManager.OnSomethingChange += PrintSomeShit;
        EventManager.OnSomethingChange += PrintSomeOtherShit;
        CurrentDeck = Decks[0];
        CurrentDiscardPile = DiscardPiles[0];
        PlayerNo = 0;
        EventManager.ChangeTurnPhase(TurnPhase.Beginning);
        EventManager.CallOnSomethingChange();
    }

    void Update()
    {
        if (CheckIfShouldChangeTurnPhase())
        {
            NextTurnPhase();
        }
    }

    public void PrintSomeShit()
    {
        print("MegaMan printed some shit!");
    }

    public void PrintSomeOtherShit()
    {
        print("MegaMan printed some other shit");
    }

    private bool CheckIfShouldChangeTurnPhase()
    {
        if (_currentTurnPhase == TurnPhase.Beginning ||
            _currentTurnPhase == TurnPhase.End)
            return true;
        return false;
    }
}
