using UnityEngine;
using System.Collections;

public enum TurnPhase { Beginning, Place, Buy, End }


public class TurnPhases : MonoBehaviour {

    public static TurnPhase CurrentTurnPhase = TurnPhase.Beginning;

    void Awake()
    {
        //EventManager.OnTurnPhaseChange += SetCurrentTurnPhase;
        EventManager.OnTryTurnPhaseChange += SetCurrentTurnPhase;
    }

    void Start()
    {

    }

    void Update()
    {
        if (CheckIfShouldChangeTurnPhase())
        {
            NextTurnPhase();
        }
    }

    public void SetCurrentTurnPhase(TurnPhase newPhase)
    {
        TurnPhase previousTurnPhase = CurrentTurnPhase;
        CurrentTurnPhase = newPhase;
        bool success = true;
        switch (newPhase)
        {
            case TurnPhase.Beginning:
                SetCurrentTurnPhaseToBeginning();
                break;
            case TurnPhase.Place:
                SetCurrentTurnPhaseToPlace();
                break;
            case TurnPhase.Buy:
                SetCurrentTurnPhaseToBuy();
                break;
            case TurnPhase.End:
                SetCurrentTurnPhaseToEnd();
                break;
            default:
                success = false;
                break;
        }
        if (success)
            EventManager.ChangeTurnPhase(newPhase, previousTurnPhase);
    }

    public void SetCurrentTurnPhaseToBeginning()
    {
        CurrentTurnPhase = TurnPhase.Beginning;
    }

    public void SetCurrentTurnPhaseToPlace()
    {
        CurrentTurnPhase = TurnPhase.Place;
    }

    public void SetCurrentTurnPhaseToBuy()
    {
        CurrentTurnPhase = TurnPhase.Buy;
    }

    public void SetCurrentTurnPhaseToEnd()
    {
        CurrentTurnPhase = TurnPhase.End;
    }

    public void NextTurnPhase()
    {
        if (CurrentTurnPhase == TurnPhase.Beginning)
            EventManager.TryChangeTurnPhase(TurnPhase.Place);
        else if (CurrentTurnPhase == TurnPhase.Place)
            EventManager.TryChangeTurnPhase(TurnPhase.Buy);
        else if (CurrentTurnPhase == TurnPhase.Buy)
            EventManager.TryChangeTurnPhase(TurnPhase.End);
        else if (CurrentTurnPhase == TurnPhase.End)
            EventManager.TryChangeTurnPhase(TurnPhase.Beginning);
    }

    private bool CheckIfShouldChangeTurnPhase()
    {
        return CurrentTurnPhase == TurnPhase.Beginning ||
               CurrentTurnPhase == TurnPhase.End;
    }

    public static bool IsCurrentPhase(TurnPhase phase)
    {
        return phase == CurrentTurnPhase;
    }
}
