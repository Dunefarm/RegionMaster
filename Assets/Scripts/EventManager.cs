using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{

    public delegate void d_NoArgVoid();
    public delegate void d_TurnPhase(TurnPhase turnPhase);

    public static event d_NoArgVoid OnSomethingChange;
    public static event d_TurnPhase OnTurnPhaseChange;

    public static void CallOnSomethingChange()
    {
        if (OnSomethingChange != null)
            OnSomethingChange();
    }

    public static void ChangeTurnPhase(TurnPhase turnPhase)
    {
        if (OnTurnPhaseChange != null)
            OnTurnPhaseChange(turnPhase);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
