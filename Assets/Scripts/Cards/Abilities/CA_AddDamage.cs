using UnityEngine;
using System.Collections;
using ExtendedEditor;

public class CA_AddDamage : CardAbility
{

    public int Damage;
    public bool MultiplyByCounter = false;
    [ShowIf("MultiplyByCounter")]
    public CardAbilityCounter CACounter;

    public override void ActivateAbility()
    {
        EventManager.AddDamageToPool(Damage * (MultiplyByCounter ? CACounter.Value : 1));
        ResolveAbility();
    }
}
