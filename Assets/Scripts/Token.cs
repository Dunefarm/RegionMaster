using UnityEngine;
using System.Collections;

public class Token : CustomBehaviour {

    public enum Placement {Bag, Grid, PlayerPool}
    public enum OwnerTypes {None, White, Black}
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

    public OwnerTypes Owner
    {
        get { return _owner; }
        set
        {
            _owner = value;
            switch(value)
            {
                case OwnerTypes.Black:
                    BlackMarker.enabled = true;
                    WhiteMarker.enabled = false;
                    break;
                case OwnerTypes.White:
                    BlackMarker.enabled = false;
                    WhiteMarker.enabled = true;
                    break;
                case OwnerTypes.None:
                    BlackMarker.enabled = false;
                    WhiteMarker.enabled = false;
                    break;
            }
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

    

    Transform _transform;
    Renderer _renderer;
    ColorType _color;
    OwnerTypes _owner = OwnerTypes.None;

    MegaManager _megaMan;
    private TurnPhases _turnPhases;

    // Use this for initialization
    void Awake ()
    {
        _transform = transform;
        _renderer = GetComponentInChildren<Renderer>();
        _megaMan = FindObjectOfType<MegaManager>();
        _turnPhases = _megaMan.TurnPhases;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaceInGrid(Finite2DCoord newGridCoord, Vector3 pos)
    {
        _transform.position = pos;
        _renderer.enabled = true;
        GridCoord = newGridCoord;
    }

    public void PlaceInBag()
    {
        Owner = OwnerTypes.None;
        Place = Placement.Bag;
        _transform.position = Vector3.one * 1000;
        _renderer.enabled = false;
        _megaMan.GridMan.TokensInBag.Add(this);
        GridCoord = new Finite2DCoord(-1, -1);
    }

    public void PlaceInPlayerPool(Vector3 pos)
    {
        Place = Placement.PlayerPool;
        _transform.position = pos;
        GridCoord = new Finite2DCoord(-1, -1);
    }

    public OwnerTypes GetCurrentPlayer()
    {
        int playerNo = _megaMan.CurrentPlayerNumber;
        switch(playerNo)
        {
            case 0:
                return OwnerTypes.White;
            case 1:
                return OwnerTypes.Black;
        }
        return OwnerTypes.None;
    }

    public override void OnMouseClicked()
    {
        if (_megaMan.TurnPhases.CurrentTurnPhase == TurnPhase.Place && Owner == Token.OwnerTypes.None)
        {
            switch (Color)
            {
                case ColorType.Red:
                    print("It was red!");
                    if (_megaMan.Markers.Amount.r > 0)
                    {
                        _megaMan.Markers.AddMarkers(new TokenMarkers(-1, 0, 0));
                        AssignOwner();
                    }
                    break;
                case ColorType.Green:
                    if (_megaMan.Markers.Amount.g > 0)
                    {
                        _megaMan.Markers.AddMarkers(new TokenMarkers(0, -1, 0));
                        AssignOwner();
                    }
                    break;
                case ColorType.Blue:
                    if (_megaMan.Markers.Amount.b > 0)
                    {
                        _megaMan.Markers.AddMarkers(new TokenMarkers(0, 0, -1));
                        AssignOwner();
                    }
                    break;
            }
        }
    }

    void AssignOwner()
    {
        Owner = GetCurrentPlayer();
    }
}
