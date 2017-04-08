using UnityEngine;
using System.Collections;

public class GridToken : CustomBehaviour {

    public Token.ColorType Color;

    public override void OnMouseClicked()
    {
        GridCell.GridTokenClicked(this);
    }

}
