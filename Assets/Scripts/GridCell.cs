using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridCell {

    public GridCell(Finite2DCoord newCoord, Vector3 newPos)
    {
        Coordinate = newCoord;
        Position = newPos;
    }

    public Finite2DCoord Coordinate;
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

    public void Clear()
    {
        if(GridToken != null)
            MonoBehaviour.Destroy(GridToken);
        Token = null;
    }

    public void CreateGridToken()
    {
        GridToken = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/GridToken") as GameObject, Position, Quaternion.identity)).GetComponent<GridToken>();
        GridTokenOwnership.Add(GridToken, this);
    }

    public void OnMouseClicked()
    {
        if (_owner == null)
            SetOwner(MegaManager.CurrentPlayer);
    }

    public void SetOwner(Player player)
    {
        GameObject playerMarkerPrefab = Resources.Load("Prefabs/PlayerMarker_" + player.PlayerNumber) as GameObject;
        Transform playerMarker = (MonoBehaviour.Instantiate(playerMarkerPrefab, Position, Quaternion.identity) as GameObject).transform;
        playerMarker.parent = GridToken.transform;
        _owner = player;
    }

    public static void GridTokenClicked(GridToken token)
    {
        GridCell cell = GridTokenOwnership[token];
        cell.OnMouseClicked();
    }
}
