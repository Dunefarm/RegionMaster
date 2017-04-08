using UnityEngine;
using System.Collections;

public class CA_GiveMarkers : CardAbility
{
    public int Red, Green, Blue;

    public override void ActivateAbility()
    {
        int R = Red + MegaManager.Markers.Amount.r;
        int G = Green + MegaManager.Markers.Amount.g;
        int B = Blue + MegaManager.Markers.Amount.b;
        MegaManager.Markers.Amount = new TokenMarkers(R, G, B);
    }

}
