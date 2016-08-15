using UnityEngine;
using System.Collections;

public enum TurnPhase { Beginning, Place, Buy, End }


public class TurnPhases : MonoBehaviour {

    private TurnPhase _currentTurnPhase = TurnPhase.Beginning;

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
        _currentTurnPhase = newPhase;
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
        }
    }

    public void SetCurrentTurnPhaseToBeginning()
    {
        _currentTurnPhase = TurnPhase.Beginning;
    }

    public void SetCurrentTurnPhaseToPlace()
    {
        _currentTurnPhase = TurnPhase.Place;
    }

    public void SetCurrentTurnPhaseToBuy()
    {
        _currentTurnPhase = TurnPhase.Buy;
    }

    public void SetCurrentTurnPhaseToEnd()
    {
        _currentTurnPhase = TurnPhase.End;
    }

    public void NextTurnPhase()
    {
        if (_currentTurnPhase == TurnPhase.Beginning)
            EventManager.ChangeTurnPhase(TurnPhase.Place);
        else if (_currentTurnPhase == TurnPhase.Place)
            EventManager.ChangeTurnPhase(TurnPhase.Buy);
        else if (_currentTurnPhase == TurnPhase.Buy)
            EventManager.ChangeTurnPhase(TurnPhase.End);
        else if (_currentTurnPhase == TurnPhase.End)
            EventManager.ChangeTurnPhase(TurnPhase.Beginning);
    }

    private bool CheckIfShouldChangeTurnPhase()
    {
        return _currentTurnPhase == TurnPhase.Beginning ||
               _currentTurnPhase == TurnPhase.End;
    }
}
