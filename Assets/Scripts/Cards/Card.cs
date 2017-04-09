using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Card : CustomBehaviour {


    //TEMPORARY STUFF!!
    public bool PutInHandYesYes = false;
    public int HandPosition = -1;







    public Player Owner;
    public enum CardLocation { OutOfGame, Deck, Hand, Play, Discard, Shop}
    public int OwnerNo = -1;
    public string NameOfCard = "N/A";
    public string RulesText = "N/A";
    public bool Displayed = false;
    public LayerMask TableLayerMask;
    public List<CardAbility> Abilities = new List<CardAbility>();
    public ManaCost ManaCost = new ManaCost();

    [HideInInspector] public Finite2DCoord ShopCoord = new Finite2DCoord(-1, -1);
    [HideInInspector] public MegaManager MegaMan;

    
    Vector3 _originalHandPosition = Vector3.zero;
    CardLocation _currentLocation = CardLocation.Hand;
    bool _draggingCard = false;

    public Card(Player player)
    {
        Owner = player;
    }

    public CardLocation CurrentLocation
    {
        get { return _currentLocation; }
        set { _currentLocation = value; }
    }

    protected override void CustomAwake()
    {
        MegaMan = FindObjectOfType<MegaManager>();
    }

    protected override void CustomStart()
    {
        Abilities = transform.GetComponentsInChildren<CardAbility>().ToList();
        foreach(CardAbility abi in Abilities)
        {
            abi.Card = this;
        }

        //TEMPORARY STUFF
        if(PutInHandYesYes)
            PutInHand();
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
        PutInDiscardPile();
    }

    public void PutInShop(Finite2DCoord coord)
    {
        CurrentLocation = CardLocation.Shop;
        ShopCoord = coord;
        transform.position = MegaMan.Shop.CoordToVector3(coord);
        transform.rotation = MegaMan.Shop.transform.rotation;
    }

    public void PutInHand()
    {
        Owner.Hand.PutCardInHand(this);
        CurrentLocation = CardLocation.Hand;
        ShopCoord = new Finite2DCoord(-1, -1);
    }

    public void PutInDiscardPile()
    {
        HandPosition = -1;
        CurrentLocation = CardLocation.Discard;
        Owner.DiscardPile.PutCardInDiscardPile(this);
    }

    public void BuyCard()
    {
        Owner = MegaManager.CurrentPlayer;
        PutInDiscardPile();
    }

    public void PutInDeck()
    {
        HandPosition = -1;
        CurrentLocation = CardLocation.Deck;
        Owner.Deck.PutCardOnBottomOfDeck(this);
    }

    public void ToggleDisplay()
    {
        if(Displayed)
        {
            Displayed = false;
            ReturnToLocation();
            //PutInHand();
        }
        else
        {
            Displayed = true;
            Owner.Hand.RemoveCardInHand(this);
            transform.position = MegaMan.CamMan.CardCloseUpPoint.position;
            transform.rotation = MegaMan.CamMan.CardCloseUpPoint.rotation;
        }
    }

    public void ReturnToLocation()
    {
        switch(CurrentLocation)
        {
            case CardLocation.Hand:
                PutInHand();
                break;
            case CardLocation.Shop:
                transform.position = MegaMan.Shop.CoordToVector3(ShopCoord);
                transform.rotation = MegaMan.Shop.transform.rotation;
                break;

        }
    }

    public override void OnMouseClicked()
    {
        ToggleDisplay();
    }

    public override void OnMouseDown()
    {
        if (!Displayed)
            _originalHandPosition = transform.position;
    }

    public override void OnMouseUpOff()
    {
        OnMouseUp();
    }

    public override void OnMouseUp()
    {
        if(_draggingCard)
        {
            if (CurrentLocation == CardLocation.Shop)
            {
                if (Vector3.Distance(transform.position, MegaManager.CurrentPlayer.DiscardPileTranform.position) < 10)
                {
                    MegaMan.Shop.RemoveFromShop(this);
                    BuyCard();
                }
                ReturnToLocation();
            }
            else if(CurrentLocation == CardLocation.Hand)
            {
                if (Camera.main.WorldToViewportPoint(transform.position).y < 0.25f) //Return to hand
                {
                    ReturnToLocation();
                }
                else
                {
                    PlayCard();
                }
            }
        }
        _draggingCard = false;
    }

    public override void OnMouseHold(Vector3 mousePos, Camera cam)
    {
        if(!Displayed)
        {
            _draggingCard = true;
            RaycastHit hit;
            Debug.DrawRay(cam.transform.position, cam.ScreenToWorldPoint(mousePos + Vector3.forward * 1000000));
            if(Physics.Raycast(cam.ScreenPointToRay(mousePos), out hit, Mathf.Infinity, TableLayerMask))
            {
                transform.position = hit.point + Vector3.back * 1;
            }
            //Vector3 mousePlusDepth = mousePos + Vector3.forward * Vector3.Distance(cam.transform.position, originalHandPosition);
        }
    }
}
