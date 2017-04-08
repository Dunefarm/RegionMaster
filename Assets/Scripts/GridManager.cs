using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[SerializeField]
public class GridManager {

    public GridManager(Grid newGrid)
    {
        grid = newGrid;
    }

    public Token[,] TokensInGrid;
    public GridCell[,] GridCells;

    [HideInInspector]
    public TokenHolder TokenBag = new TokenHolder();

    private Grid grid;
    private int TokenAmount = 30;
    private int GridSize = 8;

    public void BeginNewGame()
    {
        CreateStartingTokenBag();
        SetupGrid();

        GridCell testCell = new GridCell(new Finite2DCoord(0,0), new Vector3(2, -7, 0));
        testCell.CreateGridToken();
    }

    void CreateStartingTokenBag()
    {
        for(int i = 0; i < TokenAmount; i++)
        {
            SpawnTokenInBag(Token.ColorType.Red);
            SpawnTokenInBag(Token.ColorType.Green);
            SpawnTokenInBag(Token.ColorType.Blue);
        }
    }

    void SpawnTokenInBag(Token.ColorType color)
    {
        Token token = TokenBag.InstantiateAndPoolToken(color);
    }

    public void SetupGrid()
    {
        TokensInGrid = new Token[GridSize, GridSize];
        GridCells = new GridCell[GridSize, GridSize];
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                Finite2DCoord coord = new Finite2DCoord(i, j);
                GridCells[i, j] = new GridCell(coord, grid.GridCoordinateToVector3(coord));
            }
        }
                RefillGrid();
    }

    public void RefillGrid()
    {
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                if (TokensInGrid[i, j] != null)
                    continue;
                Token randomToken = TokenBag.PullRandomToken();
                TokensInGrid[i, j] = randomToken;
                PlaceTokenInGrid(randomToken, new Finite2DCoord(i, j));
            }
        }
    }

    void PlaceTokenInGrid(Token token, Finite2DCoord coord)
    {
        token.PlaceInGrid(coord, grid.GridCoordinateToVector3(coord.x, coord.y));
    }

    //public Vector3 GridCoordinateToVector3(int i, int j)
    //{
    //    float x = Mathf.Lerp(grid.UpperLeftCorner.position.x, grid.LowerRightCorner.position.x, i / (float)(GridSize - 1));
    //    float y = Mathf.Lerp(grid.UpperLeftCorner.position.y, grid.LowerRightCorner.position.y, j / (float)(GridSize - 1));
    //    return new Vector3(x, y, 0);
    //}

    public Token PullTokenFromGrid(Token token)
    {
        if (token.GridCoord.x == -1 || token.GridCoord.y == -1)
            return null;
        TokensInGrid[token.GridCoord.x, token.GridCoord.y] = null;
        token.GridCoord = new Finite2DCoord(-1, -1);
        return token;
    }

    public List<Token> GetCompletedRegions(Player currentPlayer)
    {
        List<Token> currentTokens = new List<Token>();
        List<Token> collectedTokens = new List<Token>();
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                if (TokensInGrid[i,j] != null && !TokensInGrid[i, j].CheckedInGrid && TokensInGrid[i,j].Owner == currentPlayer)
                {
                    currentTokens.Clear();
                    if(RecursiveCheckNeighbors(i, j, TokensInGrid[i, j].Color, ref currentTokens, currentPlayer))
                    {
                        collectedTokens.AddRange(currentTokens);
                    }
                }
            }
        }
        foreach(Token token in TokensInGrid)
        {
            if(token != null)
                token.CheckedInGrid = false;
        }
        return collectedTokens;
    }

    bool RecursiveCheckNeighbors (int i, int j, Token.ColorType color, ref List<Token> currentTokens, Player currentPlayer)
    {
        TokensInGrid[i, j].CheckedInGrid = true;
        bool hasOwner = true;
        if (TokensInGrid[i, j].Owner == currentPlayer)
            currentTokens.Add(TokensInGrid[i, j]);
        else if (!TokensInGrid[i, j].HasOwner())
            return false;
        else
            return true;

        if (i > 0)
        {
            if(TokensInGrid[i - 1, j] != null && !TokensInGrid[i-1,j].CheckedInGrid && TokensInGrid[i-1,j].Color == color)
            {
                if (!RecursiveCheckNeighbors(i - 1, j, color, ref currentTokens, currentPlayer))
                    hasOwner = false;
            }
        }
        if (i < (GridSize - 1))
        {
            if (TokensInGrid[i + 1, j] != null && !TokensInGrid[i + 1, j].CheckedInGrid && TokensInGrid[i + 1, j].Color == color)
            {
                if (!RecursiveCheckNeighbors(i + 1, j, color, ref currentTokens, currentPlayer))
                    hasOwner = false;
            }
        }
        if(j > 0)
        {
            if (TokensInGrid[i, j - 1] != null && !TokensInGrid[i, j - 1].CheckedInGrid && TokensInGrid[i, j - 1].Color == color)
            {
                if (!RecursiveCheckNeighbors(i, j - 1, color, ref currentTokens, currentPlayer))
                    hasOwner = false;
            }
        }
        if (j < (GridSize - 1))
        {
            if (TokensInGrid[i, j + 1] != null && !TokensInGrid[i, j + 1].CheckedInGrid && TokensInGrid[i, j + 1].Color == color)
            {
                if (!RecursiveCheckNeighbors(i, j + 1, color, ref currentTokens, currentPlayer))
                    hasOwner = false;
            }
        }

        return hasOwner;
    }
}
