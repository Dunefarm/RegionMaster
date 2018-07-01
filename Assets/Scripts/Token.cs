using UnityEngine;
using System.Collections;
using UnityEditor;

public class Token {

    public enum ColorType {Red, Green, Blue}
    public ColorType Color;
    public bool Temporary = false;

    public static Token CreateToken(ColorType color, bool temporary = false)
    {
        Token token = new Token();
        //tokenScript.Owner = new Player();
        token.Color = color;
        token.Temporary = temporary;
        return token;
    }
}
