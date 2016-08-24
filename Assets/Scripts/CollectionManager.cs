using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionManager {

    private float TOKEN_SEPARATION = 1.16f;
    private Vector3 RED_PLACEMENT;
    private Vector3 GREEN_PLACEMENT = new Vector3();
    private Vector3 BLUE_PLACEMENT;

    public List<Token> RedTokens = new List<Token>();
    public List<Token> BlueTokens = new List<Token>();
    public List<Token> GreenTokens = new List<Token>();

    List<Token> _collectedTokens = new List<Token>();

    private MegaManager _megaManager;

    public CollectionManager(MegaManager newMegaMan)
    {
        RED_PLACEMENT = new Vector3(1.77f, -2.19f, 0);
        GREEN_PLACEMENT = RED_PLACEMENT + Vector3.right * TOKEN_SEPARATION;
        BLUE_PLACEMENT = GREEN_PLACEMENT + Vector3.right * TOKEN_SEPARATION;
        _megaManager = newMegaMan;
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
                newPos = RED_PLACEMENT - Vector3.up * rowSize;
                RedTokens.Add(newToken);
                break;
            case Token.ColorType.Green:
                rowSize = GreenTokens.Count;
                newPos = GREEN_PLACEMENT - Vector3.up * rowSize;
                GreenTokens.Add(newToken);
                break;
            case Token.ColorType.Blue:
                rowSize = BlueTokens.Count;
                newPos = BLUE_PLACEMENT - Vector3.up * rowSize;
                BlueTokens.Add(newToken);
                break;
        }
        _megaManager.GridMan.RemoveTokenFromGrid(newToken);
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
