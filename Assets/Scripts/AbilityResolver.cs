using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityResolver {

    public static List<CardAbility> AbilitiesPending = new List<CardAbility>();
    static CardAbility AbilityCurrentBeingResolved;

    public AbilityResolver()
    {
        EventManager.Abilities.CardAbilityResolved += AbilityResolved;
    }

    public static void AddCardAbility(CardAbility cardAbility, bool andActivate = false)
    {
        AbilitiesPending.Add(cardAbility);
        if (andActivate)
            ActivateNextAbility();
    }

    public static void AddCardAbilities(List<CardAbility> cardAbilities, bool andActivate = false)
    {
        foreach (CardAbility ability in cardAbilities)
        {
            AbilitiesPending.Add(ability);
        }
        if (andActivate)
            ActivateNextAbility();
    }

    public static void ActivateNextAbility()
    {
        if (AbilitiesPending.Count < 1)
        {
            DoneResolvingAbilities();
            return;
        }
        AbilityCurrentBeingResolved = AbilitiesPending[0];
        AbilityCurrentBeingResolved.ActivateAbility();
        //ResolveAbility(AbilitiesPending[0]);
    }

    //static void ResolveAbility(CardAbility ability)
    //{
    //    AbilitiesPending.Remove(ability);
    //    ability.ResolveAbility();
    //}

    static void DoneResolvingAbilities()
    {
        //Something happens... Game unpauses, bla bla, something.
    }

    static void AbilityResolved(CardAbility cardAbility)
    {
        AbilitiesPending.Remove(cardAbility);
        ActivateNextAbility();
    }
}
