using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Card : MonoBehaviour {


    //TEMPORARY STUFF!!
    public int HandPosition = -1;

    public enum Color { Colorless, Red, Green, Blue };





    public Player Owner;
    public enum CardLocation { OutOfGame, Deck, Hand, Play, Discard, Shop}
    public int OwnerNo = -1;
    public string NameOfCard = "N/A";
    public string RulesText = "N/A";
    public Color CardColor = Color.Colorless;
    public bool Displayed = false;
    public LayerMask TableLayerMask;
    public List<CardAbility> Abilities = new List<CardAbility>();
    public ManaCost ManaCost = new ManaCost();

    [HideInInspector] public Finite2DCoord ShopCoord = new Finite2DCoord(-1, -1);
    [HideInInspector] public MegaManager MegaMan;

    
    Vector3 _originalHandPosition = Vector3.zero;
    CardLocation _currentLocation = CardLocation.Hand;
    bool _draggingCard = false;
    PhysicalCard _physicalCard;

    public Card(Player player)
    {
        Owner = player;
    }

    public CardLocation CurrentLocation
    {
        get { return _currentLocation; }
        set { _currentLocation = value; }
    }

    public PhysicalCard PhysicalCard
    {
        get { return _physicalCard; }
        set
        {
            _physicalCard = value;
            if(_physicalCard != null)
            {
                _physicalCard.AssignCard(this);
            }
        }
    }

    void Awake()
    {
        MegaMan = FindObjectOfType<MegaManager>();
    }

    void Start()
    {
        Abilities = transform.GetComponentsInChildren<CardAbility>().ToList();
        foreach(CardAbility abi in Abilities)
        {
            abi.Card = this;
        }
    }

    public void DestroyPhysicalRepresentation()
    {
        if (PhysicalCard != null)
            Destroy(PhysicalCard.gameObject);
    }

    public void AssignPhysicalRepresentation(PhysicalCard physicalCard)
    {
        DestroyPhysicalRepresentation();
        PhysicalCard = physicalCard;
    }

    public void SetOwner(Player player)
    {
        Owner = player;
        OwnerNo = Owner.PlayerNumber;
    }

    public virtual void PlayCard()
    {
        if (CurrentLocation != CardLocation.Hand)
            return;
        CurrentLocation = CardLocation.Play;
        Owner.Hand.RemoveCardInHand(this);
        AbilityResolver.AddCardAbilities(Abilities, true);
        MegaManager.CurrentPlayer.DiscardPile.PutCardInDiscardPile(this);
    }

    public void PutInShop(Finite2DCoord coord)
    {
        CurrentLocation = CardLocation.Shop;
        ShopCoord = coord;
        transform.position = MegaMan.Shop.CoordToVector3(coord);
        transform.rotation = MegaMan.Shop.transform.rotation;
    }
}
