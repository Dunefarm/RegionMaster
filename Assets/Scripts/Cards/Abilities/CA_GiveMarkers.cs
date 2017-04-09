using UnityEngine;
using System.Collections;

public class CA_GiveMarkers : CardAbility
{
    public int Red, Green, Blue;

    public override void ActivateAbility()
    {
        ManaCost manaCost = new ManaCost(Red, Green, Blue);
        EventManager.AddMarkersToMarkerPool(manaCost);
    }

}
