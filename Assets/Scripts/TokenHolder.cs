using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TokenHolder {

    public List<Token> Tokens = new List<Token>();

    public Token InstantiateAndPoolToken(Token.ColorType color)
    {
        Token token = new Token();
        //tokenScript.Owner = new Player();
        token.Color = color;
        AddToken(token);
        return token;
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
