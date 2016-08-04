using UnityEngine;
using System.Collections;

public class CA_GiveMarkers : CardAbility
{
    public int Red, Green, Blue;

    public override void ActivateAbility()
    {
        int R = Red + Card.MegaMan.Markers.Amount.r;
        int G = Green + Card.MegaMan.Markers.Amount.g;
        int B = Blue + Card.MegaMan.Markers.Amount.b;
        Card.MegaMan.Markers.Amount = new TokenMarkers(R, G, B);
    }

}
