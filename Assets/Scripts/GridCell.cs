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
    private static Dictionary<GridToken, GridCell> GridTokenOwnership = new Dictionary<GridToken, GridCell>();

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
            MonoBehaviour.Destroy(GridToken.gameObject);
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
        GridTokenOwnership.Add(GridToken, this);
    }

    void OnMouseClicked()
    {
        if (MegaManager.TurnPhases.CurrentTurnPhase == TurnPhase.Place)
        {
            if (_owner == null)
                SetOwner(MegaManager.CurrentPlayer);
        }        
    }

    public void SetOwner(Player player)
    {
        if (MegaManager.Markers.ColorAmount(Token.Color) > 0)
        {
            MegaManager.Markers.UseMarker(Token.Color);
            GameObject playerMarkerPrefab = Resources.Load("Prefabs/PlayerMarker_" + player.PlayerNumber) as GameObject;
            Transform playerMarker = (MonoBehaviour.Instantiate(playerMarkerPrefab, Position, Quaternion.identity) as GameObject).transform;
            playerMarker.parent = GridToken.transform;
            _owner = player;
        }
            
    }

    public static void GridTokenClicked(GridToken token)
    {
        GridCell cell = GridTokenOwnership[token];
        cell.OnMouseClicked();
    }
}
