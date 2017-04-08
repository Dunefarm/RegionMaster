using UnityEngine;
using System.Collections;

public class TokenHolder_Bag : TokenHolder {

    public override Token AddToken(Token token)
    {
        if (token == null || Tokens.Contains(token))
            return null;


        Tokens.Add(token);
        return token;
    }


}
