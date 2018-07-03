using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_DiscardACardTest : CardAbility
{
    public int AmountToDiscard = 1;

    HashSet<Card> _selectedCards = new HashSet<Card>();

    public override void ActivateAbility()
    {
        EventManager.Cards.SelectedCardInHand += CardSelected;
        EventManager.Cards.DeselectedCardInHand += CardDeselected;

        EventManager.DisplayTextInAnnouncement("Discard " + AmountToDiscard + (AmountToDiscard == 1 ? " card." : " cards."));
        EventManager.Cards.SetCardsInHandToSelectable();
        _activated = true;

        if (ResolveConditionsMet())
            ResolveAbility();
    }

    bool ResolveConditionsMet()
    {
        return (_selectedCards.Count == AmountToDiscard || _selectedCards.Count >= Player.GetCurrentPlayer.Hand.CardCount);
    }

    void CardSelected(Card card)
    {
        _selectedCards.Add(card);
        if(ResolveConditionsMet())
            ResolveAbility();
    }

    void CardDeselected(Card card)
    {
        _selectedCards.Remove(card);
    }

    public override void ResolveAbility()
    {
        DiscardCards();
        EventManager.Cards.SetCardsInHandToStandard();

        EventManager.Cards.SelectedCardInHand -= CardSelected;
        EventManager.Cards.DeselectedCardInHand -= CardDeselected;

        base.ResolveAbility();
    }

    void DiscardCards()
    {
        foreach (Card selectedCard in _selectedCards)
        {
            MegaManager.CurrentPlayer.DiscardPile.PutCardInDiscardPile(selectedCard);
        }
    }
}
