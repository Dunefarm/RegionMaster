using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridCell {

    public GridCell(Vector3 newPos)
    {
        Position = newPos;
    }

    public Vector3 Position;
    public Token Token;
    public GridToken GridToken;
    public Player Owner
    {
        set { _owner = value; }
        get
        {
            return _owner ?? new Player(); //"??" seems to mean "return if not null, else..."
        }
    }

    private Player _owner;
    private static Dictionary<GridToken, GridCell> GetCellFromToken = new Dictionary<GridToken, GridCell>();
    //private static GameObject Player1Marker, Player2Marker;

    public void AddToken(Token token)
    {
        Clear();
        Token = token;
        CreateGridToken();
    }

    public Token PullToken()
    {
        Token token = Token;
        Clear();
        return token;
    }

    public void Clear()
    {
        if(GridToken != null)
        {
            GetCellFromToken.Remove(GridToken);
            MonoBehaviour.Destroy(GridToken.gameObject);
        }

        Token = null;
        _owner = null;
    }

    public bool IsEmpty()
    {
        return Token == null;
    }

    public bool HasOwner()
    {
        return _owner != null;
    }

    public void CreateGridToken()
    {
        string color = Token.Color.ToString();
        GridToken = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/" + color + "GridToken") as GameObject, Position, Quaternion.identity)).GetComponent<GridToken>();
        GridToken.Color = Token.Color;
        GetCellFromToken.Add(GridToken, this);
    }

    void OnMouseClicked()
    {
        if (CanPlaceCurrentPlayerMarker())
            AssignOwner(MegaManager.CurrentPlayer);        
    }

    private bool CanPlaceCurrentPlayerMarker()
    {
        return (TurnPhases.CurrentTurnPhase == TurnPhase.Main
            && _owner == null
            && MegaManager.Markers.ColorAmount(Token.Color) > 0);
    }

    public void AssignOwner(Player player)
    {
        MegaManager.Markers.UseMarker(Token.Color);
        AssignPlayerMarker(player);
        _owner = player;
        EventManager.CollectTokens();
    }

    void AssignPlayerMarker(Player player)
    {
        GameObject playerMarkerPrefab = Resources.Load("Prefabs/PlayerMarker_" + player.PlayerNumber) as GameObject;
        Transform playerMarker = (MonoBehaviour.Instantiate(playerMarkerPrefab, Position, Quaternion.identity) as GameObject).transform;
        playerMarker.parent = GridToken.transform;
    }

    public static void GridTokenClicked(GridToken token)
    {
        GridCell cell = GetCellFromToken[token];
        cell.OnMouseClicked();
    }
}
