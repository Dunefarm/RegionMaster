using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {

    public MegaManager MegaMan;

    public GameObject ButtonCollectObj;
    public GameObject ButtonEndTurnObj;
    public Image PlayerTurnIcon;

    void Awake()
    {
        //EventManager.Phases.MainPhase_OnEnter += EnableButtonCollect;
        //EventManager.Phases.MainPhase_OnExit += DisableButtonCollect;

        EventManager.Phases.MainPhase_OnEnter += EnableButtonEndTurn;
        EventManager.Phases.MainPhase_OnExit += DisableButtonEndTurn;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //GUI button
    public void EndTurn()
    {
        MegaMan.EndTurn();
    }

    public void EnableButtonCollect()
    {
        ButtonCollectObj.SetActive(true);
    }

    public void DisableButtonCollect()
    {
        ButtonCollectObj.SetActive(false);
    }

    public void EnableButtonEndTurn()
    {
        ButtonEndTurnObj.SetActive(true);
    }

    public void DisableButtonEndTurn()
    {
        ButtonEndTurnObj.SetActive(false);
    }

    public void CollectAndGoToBuyPhase()
    {
        //List<GridCell> cells = GridManager.GetCompletedRegions(MegaManager.CurrentPlayer); //TODO: Make into event
        //List<Token> tokens = GridManager.PullTokensFromGrid(cells);
        //MegaManager.CollectionManager.AddTokensToPool(tokens);
        //EventManager.CollectTokens();
        EventManager.TryChangeTurnPhase(TurnPhase.Buy);
    }
}
