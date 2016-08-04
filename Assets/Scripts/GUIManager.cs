using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    public MegaManager MegaMan;

    public GameObject ButtonCollectObj;
    public GameObject ButtonNextTurnObj;
    public Image PlayerTurnIcon;

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

    public void Collect()
    {
        MegaMan.GridMan.CheckForCompletedRegions();
        MegaMan.CurrentTurnPhase = TurnPhase.Buy;
    }
}
