using UnityEngine;
using System.Collections;

public class CA_Heal : CardAbility
{

    public int HealAmount = 2;

    public override void ActivateAbility()
    {
        EventManager.HealPlayer(HealAmount, Player.GetCurrentPlayer);
    }
}
