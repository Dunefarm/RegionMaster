using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Markers : MonoBehaviour {

    public Text Red, Green, Blue;
    public ManaCost Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            Red.text = _amount.Red.ToString();
            Green.text = _amount.Green.ToString();
            Blue.text = _amount.Blue.ToString();
        }
    }

    public int ColorAmount(Token.ColorType color)
    {
        if (color == Token.ColorType.Red)
            return Amount.Red;
        if (color == Token.ColorType.Green)
            return Amount.Green;
        else
            return Amount.Blue;
    }

    ManaCost _amount = new ManaCost(0, 0, 0);

    void Start()
    {
        EventManager.OnAddMarkersToMarkerPool += AddMarkers;
    }

    public void AddMarkers(ManaCost markers)
    {
        ManaCost newManaCost = new ManaCost(Amount.Red + markers.Red, Amount.Green + markers.Green, Amount.Blue + markers.Blue);
        Amount = newManaCost;
    }

    public void UseMarker(Token.ColorType color)
    {
        ManaCost newTokenMarker;

        if (color == Token.ColorType.Red)
            newTokenMarker = new ManaCost(-1, 0, 0);
        else if (color == Token.ColorType.Green)
            newTokenMarker = new ManaCost(0, -1, 0);
        else
            newTokenMarker = new ManaCost(0, 0, -1);

        AddMarkers(newTokenMarker);
    }

    public void ClearMarkers()
    {
        Amount = new ManaCost(0, 0, 0);
    }
}
