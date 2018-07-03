using UnityEngine;
using System.Collections;

public class CardAbility : MonoBehaviour {

    [HideInInspector]
    public Card Card;

    protected bool _activated = false;

    void Awake()
    {
    }

    public virtual void ActivateAbility()
    {
    }

    public virtual void ResolveAbility()
    {
        _activated = false;
        EventManager.Abilities.ResolveCardAbility(this);
    }

}
