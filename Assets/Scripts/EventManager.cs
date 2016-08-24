using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{

    public delegate void d_NoArgVoid();
    public delegate void d_TurnPhase(TurnPhase turnPhase);
    public delegate void d_int(int value);
    public delegate bool d_TurnPhaseBool(TurnPhase turnPhase);

    public static event d_NoArgVoid OnSomethingChange;
    public static event d_TurnPhase OnTurnPhaseChange;
    public static event d_TurnPhaseBool OnTryTurnPhaseChange;
    public static event d_int OnTryDrawCard;
    public static event d_int OnActivatePlayer;

    public static void CallOnSomethingChange()
    {
        if (OnSomethingChange != null)
            OnSomethingChange();
    }

    public static void ChangeTurnPhase(TurnPhase turnPhase)
    {
        if (OnTryTurnPhaseChange != null && OnTryTurnPhaseChange(turnPhase))
        {
            if (OnTurnPhaseChange != null)
            {
                OnTurnPhaseChange(turnPhase);
            }
        }
    }

    private static void TryChangeTurnPhase(TurnPhase turnPhase)
    {
        
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
