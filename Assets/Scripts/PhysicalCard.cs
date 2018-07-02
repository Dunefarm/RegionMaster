using UnityEngine;
using System.Collections;

public class PhysicalCard : CustomBehaviour {

    public Card Card;
    public bool ZoomedIn = false;

    protected Transform Transform;
    protected bool _draggingCard = false;
    protected PhysicalCardProperties _properties;

    private GameObject ZoomedInCard;
    private Transform ZoomedInTransform;

    public void AssignCard(Card card)
    {
        Card = card;
        Transform tempTrans = transform;
        tempTrans.parent = card.transform;
        tempTrans.localPosition = Vector3.zero;
        tempTrans.localRotation = Quaternion.identity;
        Transform = card.transform;
        SetPhysicalCardProperties();
    }

    void SetPhysicalCardProperties()
    {
        _properties = Transform.GetComponentInChildren<PhysicalCardProperties>();
        _properties.SetName(Card.Name);
        _properties.SetRules(Card.RulesText);
        _properties.SetColor(Card.Color);
        _properties.SetCost(Card.ManaCost);
    }

    public void ToggleZoomIn()
    {
        if (ZoomedIn)
        {
            ZoomedIn = false;
        }
        else
        {
            ZoomedIn = true;
            Card.Owner.Hand.PullCardOutofHand(Card);
            Transform.position = Card.MegaMan.CamMan.CardCloseUpPoint.position;
            Transform.rotation = Card.MegaMan.CamMan.CardCloseUpPoint.rotation;
        }
    }

    public void ReturnToHand()
    {
        Card.Owner.Hand.PutCardInHand(Card);
    }

    public override void OnMouseClicked()
    {
        _properties.SetOutlineActive(!_properties.Outline.activeInHierarchy);
        //ToggleZoomIn(); //I'd rather this works better before I implement it fo real...
    }

    public override void OnMouseUpOff()
    {
        OnMouseUp();
    }

    public override void OnMouseHover()
    {
        ZoomIn();
    }

    public override void OnMouseStopHover()
    {
        ZoomOut();
    }

    protected void ZoomIn()
    {
        if(!ZoomedIn && !_draggingCard)
        {
            if (ZoomedInCard == null)
            {
                ZoomedInCard = Instantiate(gameObject, Vector3.zero, Quaternion.identity, _transform) as GameObject;
                Destroy(ZoomedInCard.GetComponentInChildren<PhysicalCard>());
                ZoomedInTransform = ZoomedInCard.transform;
            }
            ZoomedInTransform.position = MegaManager.Table.ZoomedInCardPlacement.position;
            ZoomedInTransform.rotation = MegaManager.Table.ZoomedInCardPlacement.rotation;
            ZoomedInCard.SetActive(true);
            ZoomedIn = true;
        }

    }

    protected void ZoomOut()
    {
        if (ZoomedIn)
        {
            if (ZoomedInCard != null)
            {
                ZoomedInCard.SetActive(false);
            }
            ZoomedIn = false;
        }
    }
}
