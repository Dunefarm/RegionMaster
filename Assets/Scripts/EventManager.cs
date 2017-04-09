using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{

    public delegate void d_NoArgVoid();
    public delegate void d_TurnPhase(TurnPhase turnPhase);
    public delegate void d_TurnPhaseTwice(TurnPhase turnPhase, TurnPhase previousTurnPhase);
    public delegate void d_int(int value);
    public delegate bool d_TurnPhaseBool(TurnPhase turnPhase);
    public delegate void d_Manacost(ManaCost manaCost);

    public static event d_NoArgVoid OnSomethingChange;
    public static event d_TurnPhase OnTurnPhaseBegin;
    public static event d_TurnPhase OnTurnPhaseEnd;
    public static event d_TurnPhase OnTryTurnPhaseChange;
    public static event d_int OnTryDrawCard;
    public static event d_int OnActivatePlayer;
    public static event d_Manacost OnAddMarkersToMarkerPool;

    public static void CallOnSomethingChange()
    {
        if (OnSomethingChange != null)
            OnSomethingChange();
    }

    public static void TryChangeTurnPhase(TurnPhase turnPhase)
    {
        TurnPhase previousTurnPhase = MegaManager.TurnPhases.CurrentTurnPhase;
        if (OnTryTurnPhaseChange != null)
        {
            OnTryTurnPhaseChange(turnPhase);
        }
    }

    public static void ChangeTurnPhase(TurnPhase turnPhase, TurnPhase previousTurnPhase)
    {
        if (OnTurnPhaseEnd != null)
            OnTurnPhaseEnd(previousTurnPhase);

        if (OnTurnPhaseBegin != null)
            OnTurnPhaseBegin(turnPhase);
}

    public static void TryDrawCard(int value)
    {
        if (OnTryDrawCard != null)
        {
            OnTryDrawCard(value);
        }
    }

    public static void ActivatePlayer(int number)
    {
        if (OnActivatePlayer != null)
        {
            OnActivatePlayer(number);
        }
    }

    public static void AddMarkersToMarkerPool(ManaCost manaCost)
    {
        if (OnAddMarkersToMarkerPool != null)
            OnAddMarkersToMarkerPool(manaCost);
    }

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    
    /* Attempt at making an event wrapper
    public class CustomEvent
    {
        private readonly bool _unlimitedSubscriptions = true;
        bool _alreadySubscribed = false;

        public CustomEvent(bool canHaveUnlimitedSubscribers = true)
        {
            _unlimitedSubscriptions = canHaveUnlimitedSubscribers;
        }

        System.Action<object> _singleArgSubscriptions;
        System.Action _noArgSubscriptions;

        public void Subscribe(System.Action method)
        {
            if (!_unlimitedSubscriptions && _alreadySubscribed)
                return;
            _alreadySubscribed = true;
            _noArgSubscriptions = method;
        }

        public void Subscribe(System.Action<object> method)
        {
            if (!_unlimitedSubscriptions && _alreadySubscribed)
                return;
            _alreadySubscribed = true;
            _singleArgSubscriptions = method;
        }

        public void Unsubscribe(System.Action<object> method)
        {
            _singleArgSubscriptions -= method;
        }

        public void Invoke()
        {
            if (_singleArgSubscriptions != null)
            {
                _singleArgSubscriptions();
            }
            if (_noArgSubscriptions != null)
            {
                _noArgSubscriptions
            }
        }

        public void Clear()
        {
            _singleArgSubscriptions = null;
        }
    }
    */
}
