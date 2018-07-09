using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_DiscardACard : CardAbility
{
    public int AmountToDiscard = 1;

    HashSet<Card> _selectedCards = new HashSet<Card>();
    private int _calculatedAmountToDiscard;

    public override void ActivateAbility()
    {
        _activated = true;

        EventManager.Cards.SelectedCardInHand += CardSelected;
        EventManager.Cards.DeselectedCardInHand += CardDeselected;
        EventManager.GUI.ConfirmButtonPressed += ConfirmButtonPressed;

        EventManager.DisplayTextInAnnouncement("Discard " + AmountToDiscard + (AmountToDiscard == 1 ? " card." : " cards."));
        EventManager.Cards.SetCardsInHandToSelectable();
        EventManager.GUI.DisableEndTurnButton();

        if(Player.GetCurrentPlayer.Hand.CardCount <= AmountToDiscard)
        {
            foreach(Card card in Player.GetCurrentPlayer.Hand.Cards)
            {
                _selectedCards.Add(card);
            }
            ResolveAbility();
        }
        else
            EventManager.GUI.EnableConfirmButton(ResolveConditionsMet());
        

        //if (ResolveConditionsMet())
        //    ResolveAbility();
    }

    bool ResolveConditionsMet()
    {
        _calculatedAmountToDiscard = Mathf.Min(AmountToDiscard, Player.GetCurrentPlayer.Hand.CardCount);
        return (_selectedCards.Count == _calculatedAmountToDiscard);// || _selectedCards.Count == Player.GetCurrentPlayer.Hand.CardCount);
    }

    void CardSelected(Card card)
    {
        _selectedCards.Add(card);
        EventManager.GUI.EnableConfirmButton(ResolveConditionsMet());
    }

    void CardDeselected(Card card)
    {
        _selectedCards.Remove(card);
        EventManager.GUI.EnableConfirmButton(ResolveConditionsMet());
    }

    public override void ResolveAbility()
    {
        DiscardCards();
        EventManager.GUI.DisableConfirmButton();
        EventManager.GUI.EnableEndTurnButton();
        EventManager.Cards.SetCardsInHandToStandard();

        EventManager.Cards.SelectedCardInHand -= CardSelected;
        EventManager.Cards.DeselectedCardInHand -= CardDeselected;
        EventManager.GUI.ConfirmButtonPressed -= ConfirmButtonPressed;

        base.ResolveAbility();
    }

    void DiscardCards()
    {
        foreach (Card selectedCard in _selectedCards)
        {
            MegaManager.CurrentPlayer.DiscardPile.PutCardInDiscardPile(selectedCard);
        }
    }

    void ConfirmButtonPressed()
    {
        ResolveAbility();
    }
}
