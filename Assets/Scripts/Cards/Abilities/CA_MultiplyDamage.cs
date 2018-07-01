using UnityEngine;
using System.Collections;

public class CA_MultiplyDamage : CardAbility
{

    public int MultiplyBy = 2;

    public override void ActivateAbility()
    {        
        EventManager.AddDamageToPool(Player.GetCurrentPlayer.Damage.DamageInPool * (MultiplyBy - 1));
    }
}
