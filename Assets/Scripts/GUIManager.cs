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
        EventManager.OnTurnPhaseBegin += EnableButtonCollect;
        EventManager.OnTurnPhaseEnd += DisableButtonCollect;

        EventManager.OnTurnPhaseBegin += EnableButtonNextTurn;
        EventManager.OnTurnPhaseEnd += DisableButtonNextTurn;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NextTurn()
    {
        MegaMan.NextTurn();
    }

    public void EnableButtonCollect(TurnPhase turnPhase)
    {
        if(turnPhase == TurnPhase.Place)
            ButtonCollectObj.SetActive(true);
    }

    public void DisableButtonCollect(TurnPhase turnPhase)
    {
        if (turnPhase == TurnPhase.Place)
            ButtonCollectObj.SetActive(false);
    }

    public void EnableButtonNextTurn(TurnPhase turnPhase)
    {
        if (turnPhase == TurnPhase.Buy)
            ButtonNextTurnObj.SetActive(true);
    }

    public void DisableButtonNextTurn(TurnPhase turnPhase)
    {
        if (turnPhase == TurnPhase.Buy)
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
