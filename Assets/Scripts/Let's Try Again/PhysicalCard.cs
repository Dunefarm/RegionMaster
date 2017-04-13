using UnityEngine;
using System.Collections;

public class PhysicalCard : CustomBehaviour {

    public enum CardLocation { OutOfGame, Deck, Hand, Play, Discard, Shop }

    public Card Card;
    public bool ZoomedIn = false;

    protected Transform Transform;

    [HideInInspector]
    public Finite2DCoord ShopCoord = new Finite2DCoord(-1, -1);

    public CardLocation CurrentLocation
    {
        get { return _currentLocation; }
        set { _currentLocation = value; }
    }

    CardLocation _currentLocation = CardLocation.Hand;
    protected bool _draggingCard = false;

    public void AssignCard(Card card)
    {
        Card = card;
        Transform tempTrans = transform;
        tempTrans.parent = card.transform;
        tempTrans.localPosition = Vector3.zero;
        tempTrans.localRotation = Quaternion.identity;
        Transform = card.transform;
    }

    public void ToggleZoomIn()
    {
        if (ZoomedIn)
        {
            ZoomedIn = false;
            //ReturnToLocation();
            //PutInHand();
        }
        else
        {
            ZoomedIn = true;
            Card.Owner.Hand.RemoveCardInHand(Card);
            Transform.position = Card.MegaMan.CamMan.CardCloseUpPoint.position;
            Transform.rotation = Card.MegaMan.CamMan.CardCloseUpPoint.rotation;
        }
    }

    public void ReturnToHand()
    {
        Card.Owner.Hand.PutCardInHand(Card);
        CurrentLocation = CardLocation.Hand;
        ShopCoord = new Finite2DCoord(-1, -1);
    }

    public override void OnMouseClicked()
    {
        //ToggleZoomIn(); //I'd rather this works better before I implement it fo real...
    }

    public override void OnMouseUpOff()
    {
        OnMouseUp();
    }

    //public override void OnMouseUp()
    //{
    //    if (_draggingCard)
    //    {
    //        if (CurrentLocation == CardLocation.Shop)
    //        {
    //            if (Vector3.Distance(Transform.position, MegaManager.CurrentPlayer.DiscardPileTranform.position) < 10)
    //            {
    //                Card.MegaMan.Shop.RemoveFromShop(Card);
    //                BuyCard();
    //            }
    //            ReturnToLocation();
    //        }
    //        else if (CurrentLocation == CardLocation.Hand)
    //        {
    //            if (Camera.main.WorldToViewportPoint(Transform.position).y < 0.25f) //Return to hand
    //            {
    //                ReturnToLocation();
    //            }
    //            else
    //            {
    //                PlayCard();
    //            }
    //        }
    //    }
    //    _draggingCard = false;
    //}

    public void BuyCard() //Put in an inherited class for shop cards
    {
        Card.Owner = MegaManager.CurrentPlayer;
        PutInDiscardPile();
    }

    public void PutInDiscardPile() //No! This needs to go whereever it was discarded from!
    {
        CurrentLocation = CardLocation.Discard;
        Card.Owner.DiscardPile.PutCardInDiscardPile(Card);
    }

    public virtual void PlayCard() //This needs to be split up between Card.cs and whatever plays the card
    {
        if (CurrentLocation != CardLocation.Hand)
            return;
        CurrentLocation = CardLocation.Play;
        Card.Owner.Hand.RemoveCardInHand(Card);
        AbilityResolver.AddCardAbilities(Card.Abilities, true);
        PutInDiscardPile();
    }
}
