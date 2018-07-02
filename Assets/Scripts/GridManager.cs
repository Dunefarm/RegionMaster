using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class GridManager {

    public static GridCell[,] GridCells;

    [HideInInspector]
    public static TokenHolder TokenBag = new TokenHolder();

    private static Grid grid;
    private static int TOKEN_AMOUNT = 30;
    private static int GridSize;
    private static bool[,] CellsChecked;

    public static void AssignGrid(Grid newGrid)
    {
        grid = newGrid;
    }

    public static void SetupGridManager()
    {
        SetupGrid();
        EventManager.OnStartGame += SetupNewGame;
        EventManager.OnCollectTokens += CollectTokens;
        EventManager.Phases.MainPhase_OnEnter += CollectTokens;
    }

    public static void SetupNewGame()
    {
        CreateStartingTokenBag();
        RefillGrid();
    }

    static void CreateStartingTokenBag()
    {
        for(int i = 0; i < TOKEN_AMOUNT; i++)
        {
            SpawnTokenInBag(Token.ColorType.Red);
            SpawnTokenInBag(Token.ColorType.Green);
            SpawnTokenInBag(Token.ColorType.Blue);
        }
    }

    static void SpawnTokenInBag(Token.ColorType color)
    {
        TokenBag.InstantiateAndPoolToken(color);
    }

    public static void SetupGrid()
    {
        GridSize = grid.GridSize;
        CellsChecked = new bool[GridSize, GridSize];
        GridCells = new GridCell[GridSize, GridSize];

        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                Finite2DCoord coord = new Finite2DCoord(i, j);
                GridCells[i, j] = new GridCell(grid.GridCoordinateToVector3(coord));
            }
        }
        //RefillGrid();
    }

    public static void RefillGrid()
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

    static void PlaceTokenInGrid(Token token, Finite2DCoord coord)
    {
        GridCells[coord.x, coord.y].AddToken(token);
    }

    public static List<Token> PullTokensFromGrid(List<GridCell> cells)
    {
        List<Token> tokens = new List<Token>();
        for (int cellNo = 0; cellNo < cells.Count; cellNo++)
        {
            tokens.Add(cells[cellNo].PullToken());
        }
        return tokens;
    }

    public static List<GridCell> GetCompletedRegions(Player currentPlayer)
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

    static bool RecursiveCheckNeighbors (int i, int j, Token.ColorType color, ref List<GridCell> currentCells, Player currentPlayer)
    {
        if (!GridCells[i, j].HasOwner())
            return false;

        CellsChecked[i, j] = true;

        if (GridCells[i, j].Owner == currentPlayer)
            currentCells.Add(GridCells[i, j]);
        else
            return true;      

        bool hasOwner = true;

        if (IsGridCellViable(i - 1, j, color))
        {
            if (!RecursiveCheckNeighbors(i - 1, j, color, ref currentCells, currentPlayer))
                hasOwner = false;
        }
        if (IsGridCellViable(i + 1, j, color))
        {
            if (!RecursiveCheckNeighbors(i + 1, j, color, ref currentCells, currentPlayer))
                hasOwner = false;
        }
        if(IsGridCellViable(i, j - 1, color))
        {
            if (!RecursiveCheckNeighbors(i, j - 1, color, ref currentCells, currentPlayer))
                hasOwner = false;
        }
        if (IsGridCellViable(i, j + 1, color))
        {
            if (!RecursiveCheckNeighbors(i, j + 1, color, ref currentCells, currentPlayer))
                hasOwner = false;
        }

        return hasOwner;
    }

    private static bool IsGridCellViable(int i, int j, Token.ColorType color)
    {
        if (i < 0 || i > GridSize - 1 || j < 0 || j > GridSize - 1)
            return false;

        return (!GridCells[i, j].IsEmpty() && !CellsChecked[i, j] && GridCells[i, j].Token.Color == color);
    }

    public static void CollectTokens()
    {
        List<GridCell> cells = GetCompletedRegions(Player.GetCurrentPlayer); //TODO: Make into event
        List<Token> tokens = PullTokensFromGrid(cells);
        MegaManager.CollectionManager.AddTokensToPool(tokens);
        if (tokens.Count > 0)
            RefillGrid();
    }
}
