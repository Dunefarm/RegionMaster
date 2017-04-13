using UnityEngine;
using System.Collections;

public enum CardColor { Colorless, Red, Green, Blue };

public struct Finite2DCoord
{
    public int x, y;

    public Finite2DCoord (int newX, int newy)
    {
        x = newX;
        y = newy;
    }
}

public struct TokenMarkers
{
    public int r, g, b;

    public TokenMarkers (int red, int green, int blue)
    {
        r = red;
        g = green;
        b = blue;
    }
}

[System.Serializable]
public struct ManaCost
{
    public int Red, Green, Blue;

    public ManaCost(int red, int green, int blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
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

    public static ManaCost operator +(ManaCost x, ManaCost y)
    {
        int red = x.Red + y.Red;
        int green = x.Green + y.Green;
        int blue = x.Blue + y.Blue;
        return new ManaCost(red, green, blue);
    }

    public static ManaCost operator -(ManaCost x, ManaCost y)
    {
        int red = x.Red - y.Red;
        int green = x.Green - y.Green;
        int blue = x.Blue - y.Blue;
        return new ManaCost(red, green, blue);
    }
}
