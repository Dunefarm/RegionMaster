using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GridManager : MonoBehaviour {

    public CollectionManager ColMan;
    public MegaManager MegaMan;

    public GameObject TokenPrefab;
    public List<Token> Tokens = new List<Token>();
    public List<Token> TokensInBag = new List<Token>();
    public Token[,] TokensInGrid = new Token[8, 8];

    public Material RedMat;
    public Material GreenMat;
    public Material BlueMat;

    public Transform UpperLeftCorner;
    public Transform LowerRightCorner;

    // Use this for initialization
    void Start ()
    {
        SpawnTokensInBag(30, Token.ColorType.Red);
        SpawnTokensInBag(30, Token.ColorType.Green);
        SpawnTokensInBag(30, Token.ColorType.Blue);

        float x;
        float y;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                x = Mathf.Lerp(UpperLeftCorner.position.x, LowerRightCorner.position.x, i / 7f);
                y = Mathf.Lerp(UpperLeftCorner.position.y, LowerRightCorner.position.y, j / 7f);
                int randomID = Random.Range(0, TokensInBag.Count);
                TokensInGrid[i, j] = TokensInBag[randomID];
                TokensInBag[randomID].PlaceInGrid(new Finite2DCoord(i, j), new Vector3(x, y, 0));
                TokensInBag.RemoveAt(randomID);
            }
        }
    }

    Token.ColorType RandomColor()
    {
        switch(Random.Range(0,3))
        {
            case 0:
                return Token.ColorType.Red;
            case 1:
                return Token.ColorType.Green;
            case 2:
                return Token.ColorType.Blue;
            default:
                return Token.ColorType.Blue; //why not...
        }
    }

    void SpawnTokensInBag(int amount, Token.ColorType color)
    {
        if (amount < 1)
            return;
        for (int i = 0; i < amount; i++)
        {
            GameObject token = (GameObject)Instantiate(TokenPrefab, Vector3.one * 1000, Quaternion.identity);
            Token tokenScript = token.GetComponent<Token>();
            //tokenScript.Owner = new Player();
            tokenScript.Color = color;
            Tokens.Add(tokenScript);
            TokensInBag.Add(tokenScript);
        }
    }

    public void RemoveTokenFromGrid(Token token)
    {
        if (token.GridCoord.x == -1)
            return;
        TokensInGrid[token.GridCoord.x, token.GridCoord.y] = null;
        token.GridCoord = new Finite2DCoord(-1, -1);
    }

    public void CheckForCompletedRegions()
    {
        List<Token> currentTokens = new List<Token>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (TokensInGrid[i,j] != null && !TokensInGrid[i, j].CheckedInGrid && TokensInGrid[i,j].Owner == MegaMan.CurrentPlayer)
                {
                    currentTokens.Clear();
                    if(RecursiveCheckNeighbors(i, j, TokensInGrid[i, j].Color, ref currentTokens))
                    {
                        foreach (Token token in currentTokens)
                        {
                            ColMan.AddTokenToPool(token);
                        }
                    }
                }
            }
        }
        foreach(Token token in TokensInGrid)
        {
            if(token != null)
                token.CheckedInGrid = false;
        }
    }

    bool RecursiveCheckNeighbors (int i, int j, Token.ColorType color, ref List<Token> currentTokens)
    {
        TokensInGrid[i, j].CheckedInGrid = true;
        bool hasOwner = true;
        if (TokensInGrid[i, j].Owner == MegaMan.CurrentPlayer)
            currentTokens.Add(TokensInGrid[i, j]);
        else if (!TokensInGrid[i, j].HasOwner())
            return false;
        else
            return true;


        //if (TokensInGrid[i, j].OwnerTypeOwner == Token.OwnerTypes.None)
        //    hasOwner = false;
        //else if(TokensInGrid[i, j].OwnerTypeOwner == MegaMan.CurrentPlayer)
        //    currentTokens.Add(TokensInGrid[i, j]);

        if (i > 0)
        {
            if(TokensInGrid[i - 1, j] != null && !TokensInGrid[i-1,j].CheckedInGrid && TokensInGrid[i-1,j].Color == color)
            {
                if (!RecursiveCheckNeighbors(i - 1, j, color, ref currentTokens))
                    hasOwner = false;
            }
        }
        if (i < 7)
        {
            if (TokensInGrid[i + 1, j] != null && !TokensInGrid[i + 1, j].CheckedInGrid && TokensInGrid[i + 1, j].Color == color)
            {
                if (!RecursiveCheckNeighbors(i + 1, j, color, ref currentTokens))
                    hasOwner = false;
            }
        }
        if(j > 0)
        {
            if (TokensInGrid[i, j - 1] != null && !TokensInGrid[i, j - 1].CheckedInGrid && TokensInGrid[i, j - 1].Color == color)
            {
                if (!RecursiveCheckNeighbors(i, j - 1, color, ref currentTokens))
                    hasOwner = false;
            }
        }
        if (j < 7)
        {
            if (TokensInGrid[i, j + 1] != null && !TokensInGrid[i, j + 1].CheckedInGrid && TokensInGrid[i, j + 1].Color == color)
            {
                if (!RecursiveCheckNeighbors(i, j + 1, color, ref currentTokens))
                    hasOwner = false;
            }
        }

        return hasOwner;
    }

    public void FillGrid()
    {
        float x, y;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (TokensInGrid[i, j] != null)
                    continue;
                x = Mathf.Lerp(UpperLeftCorner.position.x, LowerRightCorner.position.x, i / 7f);
                y = Mathf.Lerp(UpperLeftCorner.position.y, LowerRightCorner.position.y, j / 7f);
                int randomID = Random.Range(0, TokensInBag.Count);
                TokensInGrid[i, j] = TokensInBag[randomID];
                TokensInBag[randomID].PlaceInGrid(new Finite2DCoord(i, j), new Vector3(x, y, 0));
                TokensInBag.RemoveAt(randomID);
            }
        }
    }
}
