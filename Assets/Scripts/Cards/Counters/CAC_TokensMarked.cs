using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAC_TokensMarked : CardAbilityCounter
{
    public enum PlayerOwner { CurrentPlayer, Opponent}
    public enum CountColorsType { Zero, One, Two}

    public bool CountOwned = false;
    public PlayerOwner OwnedBy = PlayerOwner.CurrentPlayer;
    public CountColorsType CountColors = CountColorsType.Zero;
    public Token.ColorType FirstColor = Token.ColorType.Red;
    public Token.ColorType SecondColor = Token.ColorType.Green;


    public override int Count()
    {
        if(CountOwned)
        {
            Player targetPlayer = (OwnedBy == PlayerOwner.CurrentPlayer ? Player.GetCurrentPlayer : Player.GetCurrentOpponent);
            if (CountColors == CountColorsType.Zero)
                _value = GridManager.Count.Tokens(targetPlayer);
            else if(CountColors == CountColorsType.One)
                _value = GridManager.Count.Tokens(targetPlayer, FirstColor);
            else
                _value = GridManager.Count.Tokens(targetPlayer, FirstColor, SecondColor);
        }
        else
        {
            if (CountColors == CountColorsType.One)
                _value = GridManager.Count.Tokens(FirstColor);
            else
                _value = GridManager.Count.Tokens(FirstColor, SecondColor);
        }
        return _value;
    }

}
