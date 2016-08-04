using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionManager : MonoBehaviour {

    public Transform Red;
    public Transform Green;
    public Transform Blue;

    public List<Token> RedTokens = new List<Token>();
    public List<Token> BlueTokens = new List<Token>();
    public List<Token> GreenTokens = new List<Token>();

    List<Token> _collectedTokens = new List<Token>();

    public GridManager GridMan;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddTokenToPool(Token newToken)
    {
        int rowSize;
        Vector3 newPos = Vector3.zero;
        _collectedTokens.Add(newToken);
        switch (newToken.Color)
        {
            case Token.ColorType.Red:
                rowSize = RedTokens.Count;
                newPos = Red.position - Vector3.up * rowSize;
                RedTokens.Add(newToken);
                break;
            case Token.ColorType.Green:
                rowSize = GreenTokens.Count;
                newPos = Green.position - Vector3.up * rowSize;
                GreenTokens.Add(newToken);
                break;
            case Token.ColorType.Blue:
                rowSize = BlueTokens.Count;
                newPos = Blue.position - Vector3.up * rowSize;
                BlueTokens.Add(newToken);
                break;
        }
        GridMan.RemoveTokenFromGrid(newToken);
        newToken.PlaceInPlayerPool(newPos);
    }

    public void CleanUp()
    {
        foreach(Token token in _collectedTokens)
        {
            token.PlaceInBag();
        }
        _collectedTokens.Clear();
        RedTokens.Clear();
        BlueTokens.Clear();
        GreenTokens.Clear();
    }
}
