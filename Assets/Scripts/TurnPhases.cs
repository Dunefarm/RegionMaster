using UnityEngine;
using System.Collections;

public enum TurnPhase { Beginning, Main, End }


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
        EndPreviousTurnPhase(CurrentTurnPhase);
        StartNextTurnPhase(newPhase);
    }

    void EndPreviousTurnPhase(TurnPhase previousTurnPhase)
    {
        switch (previousTurnPhase)
        {
            case TurnPhase.Beginning:
                EventManager.Phases.Exit_BeginningOfTurn();
                break;
            case TurnPhase.Main:
                EventManager.Phases.Exit_MainPhase();
                break;
            //case TurnPhase.Buy:
            //    EventManager.Phases.Exit_BuyFromShop();
            //    break;
            case TurnPhase.End:
                EventManager.Phases.Exit_EndOfTurn();
                break;
            default:
                break;
        }
    }

    void StartNextTurnPhase(TurnPhase newPhase)
    {
        CurrentTurnPhase = newPhase;
        switch (newPhase)
        {
            case TurnPhase.Beginning:
                EventManager.Phases.Enter_BeginningOfTurn();
                break;
            case TurnPhase.Main:
                EventManager.Phases.Enter_MainPhase();
                break;
            //case TurnPhase.Buy:
            //    EventManager.Phases.Enter_BuyFromShop();
            //    break;
            case TurnPhase.End:
                EventManager.Phases.Enter_EndOfTurn();
                break;
            default:
                break;
        }
    }

    //public void SetCurrentTurnPhaseToBeginning()
    //{
    //    CurrentTurnPhase = TurnPhase.Beginning;
    //}

    //public void SetCurrentTurnPhaseToPlace()
    //{
    //    CurrentTurnPhase = TurnPhase.Place;
    //}

    //public void SetCurrentTurnPhaseToBuy()
    //{
    //    CurrentTurnPhase = TurnPhase.Buy;
    //}

    //public void SetCurrentTurnPhaseToEnd()
    //{
    //    CurrentTurnPhase = TurnPhase.End;
    //}

    public void NextTurnPhase()
    {
        if (CurrentTurnPhase == TurnPhase.Beginning)
            EventManager.TryChangeTurnPhase(TurnPhase.Main);
        else if (CurrentTurnPhase == TurnPhase.Main)
            EventManager.TryChangeTurnPhase(TurnPhase.End);
        //else if (CurrentTurnPhase == TurnPhase.Buy)
        //    EventManager.TryChangeTurnPhase(TurnPhase.End);
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
