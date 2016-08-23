using UnityEngine;
using System.Collections;

public enum TurnPhase { Beginning, Place, Buy, End }


public class TurnPhases : MonoBehaviour {

    public TurnPhase CurrentTurnPhase = TurnPhase.Beginning;

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

    public bool SetCurrentTurnPhase(TurnPhase newPhase)
    {
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
        return success;
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
            EventManager.ChangeTurnPhase(TurnPhase.Place);
        else if (CurrentTurnPhase == TurnPhase.Place)
            EventManager.ChangeTurnPhase(TurnPhase.Buy);
        else if (CurrentTurnPhase == TurnPhase.Buy)
            EventManager.ChangeTurnPhase(TurnPhase.End);
        else if (CurrentTurnPhase == TurnPhase.End)
            EventManager.ChangeTurnPhase(TurnPhase.Beginning);
    }

    private bool CheckIfShouldChangeTurnPhase()
    {
        return CurrentTurnPhase == TurnPhase.Beginning ||
               CurrentTurnPhase == TurnPhase.End;
    }
}
