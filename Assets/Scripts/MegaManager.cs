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

    int _playerNo = 0;
    public TurnPhase _currentTurnPhase = TurnPhase.Beginning;

    public void NextTurn()
    {
        ColMan.CleanUp();
        GridMan.FillGrid();
        PlayerNo = (PlayerNo + 1) % 2;
        CurrentTurnPhase = TurnPhase.End;
        //NextPhase();
    }

    void DrawCard(int amount)
    {
        CurrentDeck.DrawCard(amount);
    }

    //public void NextPhase()
    //{
    //    int nextPhase = ((int)CurrentTurnPhase + 1) % 4;
    //    print(nextPhase);
    //    CurrentTurnPhase = (TurnPhase)nextPhase;
    //}

    void Start()
    {
        CurrentDeck = Decks[0];
        CurrentDiscardPile = DiscardPiles[0];
        PlayerNo = 0;
        CurrentTurnPhase = TurnPhase.Beginning;
    }
}
