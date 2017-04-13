using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Card : MonoBehaviour {

    public Player Owner;
    public string Name = "N/A";
    public string RulesText = "N/A";
    public CardColor Color = CardColor.Colorless;
    public ManaCost ManaCost = new ManaCost();

    [HideInInspector] public List<CardAbility> Abilities = new List<CardAbility>();
    [HideInInspector] public MegaManager MegaMan;
    [HideInInspector] public bool Displayed = false;
    [HideInInspector] public int OwnerNumber = -1;

    private bool _draggingCard = false;
    private PhysicalCard _physicalCard;

    public PhysicalCard PhysicalCard
    {
        get { return _physicalCard; }
        set
        {
            _physicalCard = value;
            if(_physicalCard != null)
                _physicalCard.AssignCard(this);
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
        OwnerNumber = Owner.PlayerNumber;
    }

    public virtual void PlayCard()
    {
        Card card = Owner.Hand.PullCardOutofHand(this);
        AbilityResolver.AddCardAbilities(Abilities, true);
        MegaManager.CurrentPlayer.DiscardPile.PutCardInDiscardPile(card);
    }
}
