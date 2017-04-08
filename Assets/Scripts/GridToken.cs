using UnityEngine;
using System.Collections;

public class GridToken : CustomBehaviour {

    public override void OnMouseClicked()
    {
        GridCell.GridTokenClicked(this);
    }


}
