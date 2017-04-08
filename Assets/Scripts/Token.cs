using UnityEngine;
using System.Collections;
using UnityEditor;

public class Token : CustomBehaviour {

    public enum Placement {Bag, Grid, PlayerPool}
    public enum ColorType {Red, Green, Blue}

    public Placement Place = Placement.Bag;
    public Material Red;
    public Material Green;
    public Material Blue;
    public Renderer Renderer;
    public Renderer WhiteMarker;
    public Renderer BlackMarker;
    [HideInInspector] public bool CheckedInGrid = false;
    public Finite2DCoord GridCoord = new Finite2DCoord(-1, -1);

    private Player _owner;
    Transform _transform;
    Renderer _renderer;
    ColorType _color;
    MegaManager _megaMan;
    private TurnPhases _turnPhases;

    public Player Owner
    {
        set { _owner = value; }
        get
        {
            return _owner ?? new Player(); //"??" seems to mean "return if not null, else..."
        }
    }

    public ColorType Color
    {
        get { return _color; }
        set
        {
            _color = value;
            switch(value)
            {
                case ColorType.Blue:
                    Renderer.material = Blue;
                    break;
                case ColorType.Green:
                    Renderer.material = Green;
                    break;
                case ColorType.Red:
                    Renderer.material = Red;
                    break;
            }
        }
    }

    public Material Material
    {
        get { return Renderer.material; }
        set
        {
            Renderer.material = value;
        }
    }


    // Use this for initialization
    void Awake ()
    {
        _transform = transform;
        _renderer = GetComponentInChildren<Renderer>();
        _megaMan = FindObjectOfType<MegaManager>();
        _turnPhases = _megaMan.TurnPhases;
    }

    public void PlaceInGrid(Finite2DCoord newGridCoord, Vector3 pos)
    {
        _transform.position = pos;
        _renderer.enabled = true;
        GridCoord = newGridCoord;
    }

    public void PlaceInBag()
    {
        RemoveOwnerAndMarker();
        Place = Placement.Bag;
        _transform.position = Vector3.one * 1000;
        _renderer.enabled = false;
        _megaMan.GridMan.TokenBag.AddToken(this);
        //_megaMan.GridMan.TokensInBag.Add(this);
        GridCoord = new Finite2DCoord(-1, -1);
    }

    public void PlaceInPlayerPool(Vector3 pos)
    {
        Place = Placement.PlayerPool;
        _transform.position = pos;
        GridCoord = new Finite2DCoord(-1, -1);
    }

    public override void OnMouseClicked()
    {
        if (_megaMan.TurnPhases.CurrentTurnPhase == TurnPhase.Place && _owner == null)
        {
            switch (Color)
            {
                case ColorType.Red:
                    if (_megaMan.Markers.Amount.r > 0)
                    {
                        _megaMan.Markers.AddMarkers(new TokenMarkers(-1, 0, 0));
                        AssignOwnerAndMarker();
                    }
                    break;
                case ColorType.Green:
                    if (_megaMan.Markers.Amount.g > 0)
                    {
                        _megaMan.Markers.AddMarkers(new TokenMarkers(0, -1, 0));
                        AssignOwnerAndMarker();
                    }
                    break;
                case ColorType.Blue:
                    if (_megaMan.Markers.Amount.b > 0)
                    {
                        _megaMan.Markers.AddMarkers(new TokenMarkers(0, 0, -1));
                        AssignOwnerAndMarker();
                    }
                    break;
            }
        }
    }

    void AssignOwnerAndMarker()
    {
        Owner = GetCurrentPlayer();
        AssignMarker(Owner);
    }

    void RemoveOwnerAndMarker()
    {
        Owner = null;
        AssignMarker(null);
    }

    public Player GetCurrentPlayer()
    {
        return MegaManager.CurrentPlayer;
    }

    void AssignMarker(Player newOwner)
    {
        if (newOwner != null)
        {
            if (newOwner.PlayerNumber == 0)
            {
                BlackMarker.enabled = false;
                WhiteMarker.enabled = true;
            }
            else if (newOwner.PlayerNumber == 1)
            {
                BlackMarker.enabled = true;
                WhiteMarker.enabled = false;
            }
        }
        else
        {
            BlackMarker.enabled = false;
            WhiteMarker.enabled = false;
        }
    }

    public bool HasOwner()
    {
        return _owner != null;
    }
}
