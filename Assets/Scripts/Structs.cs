using UnityEngine;
using System.Collections;

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
