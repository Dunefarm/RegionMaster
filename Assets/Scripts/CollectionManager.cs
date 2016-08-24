using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.NetworkSystem;

public class CollectionManager {

    private float TOKEN_SEPARATION = 1.16f;
    private Vector3 RED_PLACEMENT;
    private Vector3 GREEN_PLACEMENT;
    private Vector3 BLUE_PLACEMENT;

    public List<Token> RedTokens = new List<Token>();
    public List<Token> BlueTokens = new List<Token>();
    public List<Token> GreenTokens = new List<Token>();

    public ManaPool ManaPool;

    List<Token> _collectedTokens = new List<Token>();

    private MegaManager _megaManager;

    public CollectionManager(MegaManager newMegaMan)
    {
        RED_PLACEMENT = new Vector3(1.77f, -2.19f, 0);
        GREEN_PLACEMENT = RED_PLACEMENT + Vector3.right * TOKEN_SEPARATION;
        BLUE_PLACEMENT = GREEN_PLACEMENT + Vector3.right * TOKEN_SEPARATION;
        _megaManager = newMegaMan;
        ManaPool = new ManaPool();
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
                ManaPool.Red++;
                break;
            case Token.ColorType.Green:
                rowSize = GreenTokens.Count;
                newPos = GREEN_PLACEMENT - Vector3.up * rowSize;
                GreenTokens.Add(newToken);
                ManaPool.Green++;
                break;
            case Token.ColorType.Blue:
                rowSize = BlueTokens.Count;
                newPos = BLUE_PLACEMENT - Vector3.up * rowSize;
                BlueTokens.Add(newToken);
                ManaPool.Blue++;
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
        ManaPool.Clear();
    }

    public bool CheckIfCanAfford(ManaPool manaCost)
    {
        return ManaPool.CheckIfCanAfford(manaCost);
    }

    public void Subtract(ManaPool manaCost)
    {
        if (!ManaPool.TrySubtract(manaCost))
        {
            Debug.Log("Don't try and subtract mana, when you can't afford it!");
            return;
        }
        for (int r = RedTokens.Count - 1; r > ManaPool.Red - 1; r--)
        {
            RedTokens[r].PlaceInBag();
            RedTokens.RemoveAt(r);
        }
        for (int g = RedTokens.Count - 1; g > ManaPool.Green - 1; g--)
        {
            GreenTokens[g].PlaceInBag();
            GreenTokens.RemoveAt(g);
        }
        for (int b = RedTokens.Count - 1; b > ManaPool.Blue - 1; b--)
        {
            BlueTokens[b].PlaceInBag();
            BlueTokens.RemoveAt(b);
        }
    }
}

[Serializable]
public class ManaPool
{
    public int Red;
    public int Green;
    public int Blue;

    public ManaPool() { }

    public ManaPool(int r, int g, int b)
    {
        Red = r;
        Green = g;
        Blue = b;
    }

    public int Total
    {
        get { return Red + Green + Blue; }
    }

    public void Clear()
    {
        Red = 0;
        Green = 0;
        Blue = 0;
    }

    public bool CheckIfCanAfford(ManaPool manaCost)
    {
        return manaCost.Total <= Total
            && manaCost.Red <= Red
            && manaCost.Green <= Green
            && manaCost.Blue <= Blue;
    }

    public bool TrySubtract(ManaPool manaCost)
    {
        if (!CheckIfCanAfford(manaCost))
            return false;
        Red -= manaCost.Red;
        Green -= manaCost.Green;
        Blue -= manaCost.Blue;
        return true;
    }
}
