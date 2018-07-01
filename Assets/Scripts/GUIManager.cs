using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {

    public MegaManager MegaMan;

    public GameObject ButtonCollectObj;
    public GameObject ButtonNextTurnObj;
    public Image PlayerTurnIcon;

    void Awake()
    {
        EventManager.Phases.PlayCardsAndPlaceTokens_OnEnter += EnableButtonCollect;
        EventManager.Phases.PlayCardsAndPlaceTokens_OnExit += DisableButtonCollect;

        EventManager.Phases.BuyFromShop_OnEnter += EnableButtonNextTurn;
        EventManager.Phases.BuyFromShop_OnExit += DisableButtonNextTurn;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //GUI button
    public void NextTurn()
    {
        MegaMan.NextTurn();
    }

    public void EnableButtonCollect()
    {
        ButtonCollectObj.SetActive(true);
    }

    public void DisableButtonCollect()
    {
        ButtonCollectObj.SetActive(false);
    }

    public void EnableButtonNextTurn()
    {
        ButtonNextTurnObj.SetActive(true);
    }

    public void DisableButtonNextTurn()
    {
        ButtonNextTurnObj.SetActive(false);
    }

    public void CollectAndGoToBuyPhase()
    {
        List<GridCell> cells = GridManager.GetCompletedRegions(MegaManager.CurrentPlayer); //TODO: Make into event
        List<Token> tokens = GridManager.PullTokensFromGrid(cells);
        MegaManager.CollectionManager.AddTokensToPool(tokens);
        EventManager.TryChangeTurnPhase(TurnPhase.Buy);
    }
}
