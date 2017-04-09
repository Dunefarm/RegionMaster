using UnityEngine;
using System.Collections;

public class CardAbility : MonoBehaviour {

    [HideInInspector]
    public Card Card;

    void Awake()
    {
    }

    public virtual void ActivateAbility()
    {
    }

    public virtual void ResolveAbility()
    {
        AbilityResolver.ActivateNextAbility();
    }

}
