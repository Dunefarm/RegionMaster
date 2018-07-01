using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CA_MultiplyScore : CardAbility
{

    //public int MultiplyBy = 2;

    private List<Token> _duplicatedTokens = new List<Token>();

    public override void ActivateAbility()
    {
        foreach(CollectionManager.ManaToken manaToken in CollectionManager.AllManaTokens)
        {
            _duplicatedTokens.Add(Token.CreateToken(manaToken.Color, true));
        }
        MegaManager.CollectionManager.AddTokensToPool(_duplicatedTokens);
    }
}
