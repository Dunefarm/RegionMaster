using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.NetworkSystem;

public class CollectionManager {

    public class ManaToken
    {
        public Token Token;
        public Transform PhysicalToken;

        public void Clear()
        {
            MonoBehaviour.Destroy(PhysicalToken.gameObject);
            Token = null;
        }
    }

    private float TOKEN_SEPARATION = 1.16f;
    private Vector3 RED_PLACEMENT;
    private Vector3 GREEN_PLACEMENT;
    private Vector3 BLUE_PLACEMENT;

    public static List<ManaToken> RedManaTokens = new List<ManaToken>();
    public static List<ManaToken> GreenManaTokens = new List<ManaToken>();
    public static List<ManaToken> BlueManaTokens = new List<ManaToken>();
    public static List<ManaToken> AllManaTokens = new List<ManaToken>();

    public static List<Token> RedTokens = new List<Token>();
    public static List<Token> BlueTokens = new List<Token>();
    public static List<Token> GreenTokens = new List<Token>();

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

    public void AddTokensToPool(List<Token> tokens)
    {
        for(int i = 0; i < tokens.Count; i++)
        {
            AddTokenToPool(tokens[i]);
        }
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
        //newToken = _megaManager.GridMan.PullTokenFromGrid(newToken);
        //if(newToken != null)
        CreatePhysicalToken(newToken, newPos);
        //newToken.PlaceInPlayerPool(newPos);
    }

    void CreatePhysicalToken(Token token, Vector3 position)
    {
        ManaToken mana = new ManaToken();
        mana.Token = token;
        Transform physicalToken;
        if (token.Color == Token.ColorType.Red)
        {
            physicalToken = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/RedManaToken") as GameObject, position, Quaternion.identity)).transform;
            mana.PhysicalToken = physicalToken;
            RedManaTokens.Add(mana);
        }
        else if (token.Color == Token.ColorType.Green)
        {
            physicalToken = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/GreenManaToken") as GameObject, position, Quaternion.identity)).transform;
            mana.PhysicalToken = physicalToken;
            GreenManaTokens.Add(mana);
        }
        else
        {
            physicalToken = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/BlueManaToken") as GameObject, position, Quaternion.identity)).transform;
            mana.PhysicalToken = physicalToken;
            BlueManaTokens.Add(mana);
        }
        AllManaTokens.Add(mana);
    }

    public void CleanUp()
    {
        _collectedTokens.Clear();
        RedTokens.Clear();
        BlueTokens.Clear();
        GreenTokens.Clear();
        ManaPool.Clear();

        foreach(ManaToken mana in AllManaTokens)
        {
            mana.Clear();
        }
        AllManaTokens = new List<ManaToken>();
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

            RedTokens.RemoveAt(r);
        }
        for (int g = RedTokens.Count - 1; g > ManaPool.Green - 1; g--)
        {
            GreenTokens.RemoveAt(g);
        }
        for (int b = RedTokens.Count - 1; b > ManaPool.Blue - 1; b--)
        {
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
