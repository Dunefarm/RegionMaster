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
        EventManager.OnTurnEnd += OnTurnEnd;
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

        if (TurnPhases.IsCurrentPhase(TurnPhase.Buy) ||TurnPhases.IsCurrentPhase(TurnPhase.Place))
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

    private void OnTurnEnd()
    {
        EmptyPool();
    }
}
