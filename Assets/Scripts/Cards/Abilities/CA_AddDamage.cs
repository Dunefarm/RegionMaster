using UnityEngine;
using System.Collections;

public class CA_AddDamage : CardAbility
{

    public int Damage;

    public override void ActivateAbility()
    {
        EventManager.AddDamageToPool(Damage);
    }
}
