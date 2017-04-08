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

    public static GridCell[,] GridCells;

    [HideInInspector]
    public TokenHolder TokenBag = new TokenHolder();

    private Grid grid;
    private int TokenAmount = 30;
    private int GridSize = 8;
    private bool[,] CellsChecked;

    public void BeginNewGame()
    {
        CreateStartingTokenBag();
        SetupGrid();
    }

    void CreateStartingTokenBag()
    {
        for(int i = 0; i < TokenAmount; i++)
        {
            SpawnTokenInBag(Token.ColorType.Red);
            SpawnTokenInBag(Token.ColorType.Green);
            SpawnTokenInBag(Token.ColorType.Blue);
        }
        MonoBehaviour.print(TokenBag.Tokens.Count);
    }

    void SpawnTokenInBag(Token.ColorType color)
    {
        Token token = TokenBag.InstantiateAndPoolToken(color);
    }

    public void SetupGrid()
    {
        CellsChecked = new bool[GridSize, GridSize];
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
                if (!GridCells[i,j].IsEmpty())
                    continue;
                Token randomToken = TokenBag.PullRandomToken();
                PlaceTokenInGrid(randomToken, new Finite2DCoord(i, j));
            }
        }
    }

    void PlaceTokenInGrid(Token token, Finite2DCoord coord)
    {
        GridCells[coord.x, coord.y].AddToken(token);
    }

    public static List<Token> PullTokensFromGrid(List<GridCell> cells)
    {
        List<Token> tokens = new List<Token>();
        for (int cellNo = 0; cellNo < cells.Count; cellNo++)
        {
            MonoBehaviour.print(cells[cellNo].Token);
            tokens.Add(cells[cellNo].PullToken());
        }
        return tokens;
    }

    public List<GridCell> GetCompletedRegions(Player currentPlayer)
    {
        List<GridCell> currentCells = new List<GridCell>();
        List<GridCell> collectedCells = new List<GridCell>();
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                if (!GridCells[i, j].IsEmpty() && !CellsChecked[i, j] && GridCells[i, j].Owner == currentPlayer)
                {
                    currentCells.Clear();
                    if (RecursiveCheckNeighbors(i, j, GridCells[i, j].Token.Color, ref currentCells, currentPlayer))
                    {
                        collectedCells.AddRange(currentCells);
                    }
                }
            }
        }
        CellsChecked = new bool[GridSize, GridSize];
        return collectedCells;
    }

    bool RecursiveCheckNeighbors (int i, int j, Token.ColorType color, ref List<GridCell> currentCells, Player currentPlayer)
    {
        CellsChecked[i,j] = true;
        bool hasOwner = true;
        if (GridCells[i, j].Owner == currentPlayer)
            currentCells.Add(GridCells[i, j]);
        else if (!GridCells[i, j].HasOwner())
            return false;
        else
            return true;

        if (i > 0)
        {
            if(!GridCells[i - 1, j].IsEmpty() && !CellsChecked[i - 1, j] && GridCells[i - 1, j].Token.Color == color)
            {
                if (!RecursiveCheckNeighbors(i - 1, j, color, ref currentCells, currentPlayer))
                    hasOwner = false;
            }
        }
        if (i < (GridSize - 1))
        {
            if (!GridCells[i + 1, j].IsEmpty() && !CellsChecked[i + 1, j] && GridCells[i + 1, j].Token.Color == color)
            {
                if (!RecursiveCheckNeighbors(i + 1, j, color, ref currentCells, currentPlayer))
                    hasOwner = false;
            }
        }
        if(j > 0)
        {
            if (!GridCells[i, j - 1].IsEmpty() && !CellsChecked[i, j - 1] && GridCells[i, j - 1].Token.Color == color)
            {
                if (!RecursiveCheckNeighbors(i, j - 1, color, ref currentCells, currentPlayer))
                    hasOwner = false;
            }
        }
        if (j < (GridSize - 1))
        {
            if (!GridCells[i, j + 1].IsEmpty() && !CellsChecked[i, j + 1] && GridCells[i, j + 1].Token.Color == color)
            {
                if (!RecursiveCheckNeighbors(i, j + 1, color, ref currentCells, currentPlayer))
                    hasOwner = false;
            }
        }

        return hasOwner;
    }
}
