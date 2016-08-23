using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        MegaMan.GridMan.CheckForCompletedRegions(); //TODO: Make into event
        EventManager.ChangeTurnPhase(TurnPhase.Buy);
    }
}
