using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TokenHolder {

    public List<Token> Tokens = new List<Token>();
    //public Dictionary<Token.ColorType, Token> TokensByColor = new Dictionary<Token.ColorType, Token>();

    // Use this for initialization
    void Start()
    {
        InstantiateAndPoolTokens(30, Token.ColorType.Red);
        InstantiateAndPoolTokens(30, Token.ColorType.Green);
        InstantiateAndPoolTokens(30, Token.ColorType.Blue);
    }

    public Token InstantiateAndPoolToken(Token.ColorType color)
    {
        GameObject tokenObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Token") as GameObject, Vector3.one * 1000, Quaternion.identity);
        Token token = tokenObject.GetComponent<Token>();
        //tokenScript.Owner = new Player();
        token.Color = color;
        AddToken(token);
        return token;
    }

    public void InstantiateAndPoolTokens(int amount, Token.ColorType color)
    {
        if (amount < 1)
            return;
        for (int i = 0; i < amount; i++)
        {
            GameObject token = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Token") as GameObject, Vector3.one * 1000, Quaternion.identity);
            Token tokenScript = token.GetComponent<Token>();
            //tokenScript.Owner = new Player();
            tokenScript.Color = color;
            Tokens.Add(tokenScript);
        }
    }

    public virtual Token AddToken(Token token)
    {
        if (token == null || Tokens.Contains(token))
            return null;

        Tokens.Add(token);
        return token;
    }

    public Token PullToken(Token token)
    {
        if (Tokens.Count == 0 || !Tokens.Contains(token))
            return null;

        Tokens.Remove(token);
        return token;
    }

    public Token PullToken(Token.ColorType color)
    {
        for(int i = 0; i < Tokens.Count; i++)
        {
            if(Tokens[i].Color == color)
            {
                return PullToken(Tokens[i]);
            }
        }

        return null;
    }

    public Token PullRandomToken()
    {
        int randomID = Random.Range(0, Tokens.Count);
        return PullToken(Tokens[randomID]);
    }
}
