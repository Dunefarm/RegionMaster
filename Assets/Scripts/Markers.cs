using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Markers : MonoBehaviour {

    public Text Red, Green, Blue;
    public TokenMarkers Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            Red.text = _amount.r.ToString();
            Green.text = _amount.g.ToString();
            Blue.text = _amount.b.ToString();
        }
    }

    public int ColorAmount(Token.ColorType color)
    {
        if (color == Token.ColorType.Red)
            return Amount.r;
        if (color == Token.ColorType.Green)
            return Amount.g;
        else
            return Amount.b;
    }

    TokenMarkers _amount = new TokenMarkers(0, 0, 0);

    public void AddMarkers(TokenMarkers markers)
    {
        TokenMarkers newTokenMarker = new TokenMarkers(Amount.r + markers.r, Amount.g + markers.g, Amount.b + markers.b);
        Amount = newTokenMarker;
    }

    public void UseMarker(Token.ColorType color)
    {
        TokenMarkers newTokenMarker;

        if (color == Token.ColorType.Red)
            newTokenMarker = new TokenMarkers(-1, 0, 0);
        else if (color == Token.ColorType.Green)
            newTokenMarker = new TokenMarkers(0, -1, 0);
        else
            newTokenMarker = new TokenMarkers(0, 0, -1);

        AddMarkers(newTokenMarker);
    }

    public void ClearMarkers()
    {
        Amount = new TokenMarkers(0, 0, 0);
    }
}
