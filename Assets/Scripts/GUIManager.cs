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
        EventManager.OnTurnPhaseChange += ToggleButtonCollect;
        EventManager.OnTurnPhaseChange += ToggleButtonNextTurn;
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

    public void ToggleButtonCollect(TurnPhase turnPhase)
    {
        ButtonCollectObj.SetActive(turnPhase == TurnPhase.Place);
    }

    public void ToggleButtonNextTurn(TurnPhase turnPhase)
    {
        ButtonNextTurnObj.SetActive(turnPhase == TurnPhase.Buy);
    }

    public void CollectAndGoToBuyPhase()
    {
        List<Token> tokens = MegaMan.GridMan.GetCompletedRegions(MegaManager.CurrentPlayer); //TODO: Make into event
        MegaMan.CollectionManager.AddTokensToPool(tokens);
        EventManager.ChangeTurnPhase(TurnPhase.Buy);
    }
}
