using UnityEngine;
using System.Collections;

public class PlayerHealth {

    int HealthPoints = 20;
    HealthDisplay Display;

    public PlayerHealth()
    {
        //EventManager.OnTurnPhaseBegin += SwitchDisplay;
        EventManager.OnDealDamageToPlayer += TakeDamageIfTarget;
    }

    public void SetDisplay(HealthDisplay newDisplay)
    {
        Display = newDisplay;
        Display.SetHealth(HealthPoints);
    }

	public void TakeDamage(int amount)
    {
        HealthPoints -= amount;
        Display.SetHealth(HealthPoints);
    }

    void SwitchDisplay(TurnPhase newTurnPhase)
    {
        if (newTurnPhase != TurnPhase.Beginning)
            return;


    }

    void TakeDamageIfTarget(int damageAmount, Player player)
    {
        if (player.Health == this)
            TakeDamage(damageAmount);
    }
}
