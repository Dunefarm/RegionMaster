using UnityEngine;
using System.Collections;

public class CA_DrawCards : CardAbility
{

    public int AmountToDraw = 1;

    public override void ActivateAbility()
    {
        MegaManager.CurrentPlayer.DrawCards(AmountToDraw);
        ResolveAbility();
    }
}
