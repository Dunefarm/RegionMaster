using UnityEngine;
using System.Collections;

public class PhysicalCard_Hand : PhysicalCard {

    public enum InteractState { Standard, Selectable, Noninteractable}

    public InteractState CurrentState = InteractState.Standard;

    new protected bool _draggingCard = false;
    protected bool _selected = false;

    protected override void CustomAwake()
    {
        EventManager.Phases.MainPhase_OnEnter += SetStateToStandard;
        EventManager.Phases.MainPhase_OnExit += SetStateToNoninteractable;
        EventManager.Cards.SelectedCardInHand += SelectCard;
        EventManager.Cards.DeselectedCardInHand += DeselectCard;
        EventManager.Cards.CardsInHandSetToSelectable += SetStateToSelectable;
        EventManager.Cards.CardsInHandSetToStandard += SetStateToStandard;
    }

    public void OnDisable()
    {
        EventManager.Phases.MainPhase_OnEnter -= SetStateToStandard;
        EventManager.Phases.MainPhase_OnExit -= SetStateToNoninteractable;
        EventManager.Cards.SelectedCardInHand -= SelectCard;
        EventManager.Cards.DeselectedCardInHand -= DeselectCard;
        EventManager.Cards.CardsInHandSetToSelectable -= SetStateToSelectable;
        EventManager.Cards.CardsInHandSetToStandard -= SetStateToStandard;
    }

    void DropCard()
    {
        if (_draggingCard)
        {
            if (Camera.main.WorldToViewportPoint(Transform.position).y < 0.25f) //MAGIC NUMBER
            {
                ReturnToHand();
            }
            else
            {
                Card.PlayCard();
            }
        }
        _draggingCard = false;
    }

    public override void OnMouseUp()
    {
        DropCard();
    }

    public override void OnMouseHold(Vector3 mousePos, Camera cam)
    {
        if (CurrentState != InteractState.Standard)
            return;

        ZoomOut();
        _draggingCard = true;
        RaycastHit hit;
        Debug.DrawRay(cam.transform.position, cam.ScreenToWorldPoint(mousePos + Vector3.forward * 1000000));
        if (Physics.Raycast(cam.ScreenPointToRay(mousePos), out hit, Mathf.Infinity, Card.MegaMan.TableLayerMask))
        {
            Transform.position = hit.point + Vector3.back * 1;
        }
        //Vector3 mousePlusDepth = mousePos + Vector3.forward * Vector3.Distance(cam.transform.position, originalHandPosition);
    }

    public override void OnMouseClicked()
    {
        print("Clicked " + Card.Name + ". State: " + CurrentState + ". Selected: " + _selected + ".");
        if (CurrentState == InteractState.Selectable)
        {
            if(_selected)
                EventManager.Cards.DeselectCardInHand(Card);
            else
                EventManager.Cards.SelectCardInHand(Card);
        }
        //    _properties.SetOutlineActive(!_properties.Outline.activeInHierarchy);
        //ToggleZoomIn(); //I'd rather this works better before I implement it fo real...
    }

    public void ReturnToHand()
    {
        Card.Owner.Hand.PutCardInHand(Card);
    }

    public void SetStateToStandard()
    {
        CurrentState = InteractState.Standard;
    }

    public void SetStateToNoninteractable()
    {
        CurrentState = InteractState.Noninteractable;
        //DropCard();
    }

    public void SetStateToSelectable()
    {
        CurrentState = InteractState.Selectable;
        //DropCard();
    }

    public void SelectCard(Card card)
    {
        if(card == Card)
        {
            _selected = true;
            _properties.SetOutlineActive(true);
        }
    }

    public void DeselectCard(Card card)
    {
        if (card == Card)
        {
            _selected = false;
            _properties.SetOutlineActive(false);
        }
    }
}
