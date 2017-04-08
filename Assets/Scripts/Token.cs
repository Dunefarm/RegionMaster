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
        _turnPhases = MegaManager.TurnPhases;
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
