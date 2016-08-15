using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{

    public delegate void d_NoArgVoid();
    public delegate void d_TurnPhase(TurnPhase turnPhase);
    public delegate bool d_TurnPhaseBool(TurnPhase turnPhase);

    public static event d_NoArgVoid OnSomethingChange;
    public static event d_TurnPhase OnTurnPhaseChange;
    public static event d_TurnPhaseBool OnTryTurnPhaseChange;

    public static void CallOnSomethingChange()
    {
        if (OnSomethingChange != null)
            OnSomethingChange();
    }

    public static void ChangeTurnPhase(TurnPhase turnPhase)
    {
        if (OnTryTurnPhaseChange != null)
        {
            if (OnTryTurnPhaseChange(turnPhase))
            {
            }
        }
        if (OnTurnPhaseChange != null)
            OnTurnPhaseChange(turnPhase);
    }

    private static void TryChangeTurnPhase(TurnPhase turnPhase)
    {
        
    }

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public class SingleArgCallback
    {
        System.Action<object> internalCallback;

        bool _alreadySubscribed = false;

        public void Subscribe(System.Action<object> callback)
        {
            if (_alreadySubscribed) return;
            _alreadySubscribed = true;
            internalCallback = callback;
        }

        public void RaiseEvent()
        {
            if (internalCallback != null)
            {
                internalCallback(5);
            }
        }
    }
}
