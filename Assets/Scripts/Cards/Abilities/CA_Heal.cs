using UnityEngine;
using System.Collections;
using ExtendedEditor;

public class CA_Heal : CardAbility
{

    public int HealAmount = 2;

    public bool MultiplyByCounter = false;
    [ShowIf("MultiplyByCounter")]
    public CardAbilityCounter CACounter;

    protected int _amount = 0;

    public override void ActivateAbility()
    {
        _amount = HealAmount * (MultiplyByCounter ? CACounter.Value : 1);
        HealAndResolve(_amount);
    }

    protected void HealAndResolve(int amountToHeal)
    {
        EventManager.HealPlayer(amountToHeal, Player.GetCurrentPlayer);
        ResolveAbility();
    }
}
