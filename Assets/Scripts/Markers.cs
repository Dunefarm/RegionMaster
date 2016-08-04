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

    TokenMarkers _amount = new TokenMarkers(0, 0, 0);

    public void AddMarkers(TokenMarkers markers)
    {
        TokenMarkers newTokenMarker = new TokenMarkers(Amount.r + markers.r, Amount.g + markers.g, Amount.b + markers.b);
        Amount = newTokenMarker;
    }

    public void ClearMarkers()
    {
        Amount = new TokenMarkers(0, 0, 0);
    }

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
