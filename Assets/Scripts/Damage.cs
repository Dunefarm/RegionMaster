using UnityEngine;
using System.Collections;

public class Damage {

    public DamageDisplay Display;
    public int DamageInPool;

    public Damage()
    {
        Display = MegaManager.Table.DamageDisplay;
        EventManager.OnAddDamageToPool += AddDamage;
        EventManager.OnOpponentHealthClicked += DealDamageToOpponent;
        EventManager.Phases.EndOfTurn_OnExit += OnExit_EndOfTurn;
    }

    public void AddDamage(int amount)
    {
        DamageInPool += amount;
        Display.Damage = DamageInPool;
    }

    public void DealDamageToOpponent()
    {
        if (MegaManager.CurrentPlayer.Damage != this)
            return;

        if (TurnPhases.IsCurrentPhase(TurnPhase.Main))
        {
            EventManager.DealDamageToPlayer(DamageInPool, MegaManager.CurrentOpponent);
            EmptyPool();
        }
    }

    public void EmptyPool()
    {
        DamageInPool = 0;
        Display.Damage = 0;
    }

    private void OnExit_EndOfTurn()
    {
        EmptyPool();
    }
}
