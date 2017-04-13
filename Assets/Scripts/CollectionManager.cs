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

        public Token.ColorType Color
        {
            get { return Token.Color; }
        }

        public void Clear()
        {
            MonoBehaviour.Destroy(PhysicalToken.gameObject);
            Token = null;
        }

        public void CreatePhysicalToken(Vector3 position)
        {
            if (PhysicalToken != null)
                return;

            if (Token.Color == Token.ColorType.Red)
            {
                PhysicalToken = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/RedManaToken") as GameObject, position, Quaternion.identity)).transform;
            }
            else if (Token.Color == Token.ColorType.Green)
            {
                PhysicalToken = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/GreenManaToken") as GameObject, position, Quaternion.identity)).transform;
            }
            else
            {
                PhysicalToken = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/BlueManaToken") as GameObject, position, Quaternion.identity)).transform;
            }
        }
    }

    private float TOKEN_SEPARATION = 1.16f;
    private Vector3 RED_PLACEMENT;
    private Vector3 GREEN_PLACEMENT;
    private Vector3 BLUE_PLACEMENT;

    public static List<ManaToken> RedManaTokens = new List<ManaToken>();
    public static List<ManaToken> GreenManaTokens = new List<ManaToken>();
    public static List<ManaToken> BlueManaTokens = new List<ManaToken>();
    public static List<ManaToken> AllManaTokens
    {
        get
        {
            List<ManaToken> allManaTokens = new List<ManaToken>();
            allManaTokens.AddRange(RedManaTokens);
            allManaTokens.AddRange(GreenManaTokens);
            allManaTokens.AddRange(BlueManaTokens);
            return allManaTokens;
        }
    }

    public ManaPool ManaPool;

    public CollectionManager(MegaManager newMegaMan)
    {
        RED_PLACEMENT = new Vector3(1.77f, -2.19f, 0);
        GREEN_PLACEMENT = RED_PLACEMENT + Vector3.right * TOKEN_SEPARATION;
        BLUE_PLACEMENT = GREEN_PLACEMENT + Vector3.right * TOKEN_SEPARATION;
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
        Vector3 newPosition = CalculateManaTokenPosition(newToken.Color);
        ManaToken manaToken = new ManaToken();
        manaToken.Token = newToken;
        manaToken.CreatePhysicalToken(newPosition);
        AddManaToManaPool(manaToken);
    }

    Vector3 CalculateManaTokenPosition(Token.ColorType color)
    {
        float rowSize;
        Vector3 newPos = Vector3.zero;
        switch (color)
        {
            case Token.ColorType.Red:
                rowSize = RedManaTokens.Count;
                newPos = RED_PLACEMENT - Vector3.up * rowSize;
                break;
            case Token.ColorType.Green:
                rowSize = GreenManaTokens.Count;
                newPos = GREEN_PLACEMENT - Vector3.up * rowSize;
                break;
            case Token.ColorType.Blue:
                rowSize = BlueManaTokens.Count;
                newPos = BLUE_PLACEMENT - Vector3.up * rowSize;
                break;
        }
        return newPos;
    }

    void AddManaToManaPool(ManaToken manaToken)
    {
        if (manaToken.Color == Token.ColorType.Red)
        {
            ManaPool.Red++;
            RedManaTokens.Add(manaToken);
        }
        else if (manaToken.Color == Token.ColorType.Green)
        {
            ManaPool.Green++;
            GreenManaTokens.Add(manaToken);
        }
        else
        {
            ManaPool.Blue++;
            BlueManaTokens.Add(manaToken);
        }
    }

    public List<Token> PullOutAllTokens()
    {
        ManaPool.Clear();
        List<Token> tokens = new List<Token>();
        tokens.AddRange(PullOutAllTokensFromList(ref RedManaTokens));
        tokens.AddRange(PullOutAllTokensFromList(ref GreenManaTokens));
        tokens.AddRange(PullOutAllTokensFromList(ref BlueManaTokens));

        return tokens;
    }

    public Token PullOutToken(ManaToken mana, bool rearrange = false)
    {
        Token token = mana.Token;
        FindManaTokenList(mana).Remove(mana);
        mana.Clear();
        //if(rearrange)
            //Rearrange(); //If this kind of method becomes needed.
        return token;
    }

    List<Token> PullOutAllTokensFromList(ref List<ManaToken> manaList)
    {
        List<Token> tokens = new List<Token>();
        foreach (ManaToken mana in manaList)
        {
            tokens.Add(mana.Token);
            mana.Clear();
        }
        manaList = new List<ManaToken>();
        return tokens;
    }

    public List<ManaToken> FindManaTokenList(ManaToken mana)
    {
        if (mana.Color == Token.ColorType.Red)
            return RedManaTokens;
        else if (mana.Color == Token.ColorType.Green)
            return GreenManaTokens;
        else
            return BlueManaTokens;
    }

    public bool CheckIfCanAfford(ManaCost manaCost)
    {
        return ManaPool.CheckIfCanAfford(manaCost);
    }

    public void Subtract(ManaCost manaCost)
    {
        if (!ManaPool.CheckIfCanAfford(manaCost))
        {
            Debug.Log("Don't try and subtract mana, when you can't afford it!");
            return;
        }

        ManaPool -= manaCost;
        List<Token> tokens = new List<Token>();

        for (int r = RedManaTokens.Count - 1; r > ManaPool.Red - 1; r--)
        {
            tokens.Add(PullOutToken(RedManaTokens[r]));
        }
        for (int g = GreenManaTokens.Count - 1; g > ManaPool.Green - 1; g--)
        {
            tokens.Add(PullOutToken(GreenManaTokens[g]));
        }
        for (int b = BlueManaTokens.Count - 1; b > ManaPool.Blue - 1; b--)
        {
            tokens.Add(PullOutToken(BlueManaTokens[b]));
        }

        GridManager.TokenBag.AddTokens(tokens);
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

    public static ManaPool operator +(ManaPool pool, ManaCost cost)
    {
        return new ManaPool(pool.Red + cost.Red, pool.Green + cost.Green, pool.Blue + cost.Blue);
    }

    public static ManaPool operator -(ManaPool pool, ManaCost cost)
    {
        return new ManaPool(pool.Red - cost.Red, pool.Green - cost.Green, pool.Blue - cost.Blue);
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

    public bool CheckIfCanAfford(ManaCost manaCost)
    {
        return manaCost.Red <= Red
            && manaCost.Green <= Green
            && manaCost.Blue <= Blue;
    }

    public bool TrySubtract(ManaCost manaCost)
    {
        if (!CheckIfCanAfford(manaCost))
            return false;
        Red -= manaCost.Red;
        Green -= manaCost.Green;
        Blue -= manaCost.Blue;
        return true;
    }
}
