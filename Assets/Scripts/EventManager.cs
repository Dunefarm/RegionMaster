using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{

    public delegate void d_NoArgVoid();
    public delegate void d_Bool(bool value);
    public delegate void d_TurnPhase(TurnPhase turnPhase);
    public delegate void d_TurnPhaseTwice(TurnPhase turnPhase, TurnPhase previousTurnPhase);
    public delegate void d_int(int value);
    public delegate bool d_TurnPhaseBool(TurnPhase turnPhase);
    public delegate void d_Manacost(ManaCost manaCost);
    public delegate void d_IntPlayer(int value, Player player);
    public delegate void d_string(string text);
    public delegate void d_Card(Card card);
    public delegate void d_CardAbility(CardAbility cardAbility);

    public static class Phases
    {
        public static event d_NoArgVoid BeginningOfTurn_OnEnter;
        public static event d_NoArgVoid BeginningOfTurn_OnExit;

        public static event d_NoArgVoid MainPhase_OnEnter;
        public static event d_NoArgVoid MainPhase_OnExit;

        //public static event d_NoArgVoid Collect_OnEnter;
        //public static event d_NoArgVoid Collect_OnExit;

        //public static event d_NoArgVoid BuyFromShop_OnEnter;
        //public static event d_NoArgVoid BuyFromShop_OnExit;

        public static event d_NoArgVoid EndOfTurn_OnEnter;
        public static event d_NoArgVoid EndOfTurn_OnExit;

        public static void Enter_BeginningOfTurn()
        {
            if (BeginningOfTurn_OnEnter != null)
            {
                BeginningOfTurn_OnEnter();
            }
        }

        public static void Exit_BeginningOfTurn()
        {
            if (BeginningOfTurn_OnExit != null)
            {
                BeginningOfTurn_OnExit();
            }
        }

        public static void Enter_MainPhase()
        {
            if (MainPhase_OnEnter != null)
            {
                MainPhase_OnEnter();
            }
        }

        public static void Exit_MainPhase()
        {
            if (MainPhase_OnExit != null)
            {
                MainPhase_OnExit();
            }
        }

        //public static void Enter_Collect()
        //{
        //    if (Collect_OnEnter != null)
        //    {
        //        Collect_OnEnter();
        //    }
        //}

        //public static void Exit_Collect()
        //{
        //    if (Collect_OnExit != null)
        //    {
        //        Collect_OnExit();
        //    }
        //}

        //public static void Enter_BuyFromShop()
        //{
        //    if (BuyFromShop_OnEnter != null)
        //    {
        //        BuyFromShop_OnEnter();
        //    }
        //}

        //public static void Exit_BuyFromShop()
        //{
        //    if (BuyFromShop_OnExit != null)
        //    {
        //        BuyFromShop_OnExit();
        //    }
        //}

        public static void Enter_EndOfTurn()
        {
            if (EndOfTurn_OnEnter != null)
            {
                EndOfTurn_OnEnter();
            }
        }

        public static void Exit_EndOfTurn()
        {
            if (EndOfTurn_OnExit != null)
            {
                EndOfTurn_OnExit();
            }
        }
    }

    public static class Cards
    {
        public static event d_NoArgVoid CardsInHandSetToSelectable;
        public static event d_NoArgVoid CardsInHandSetToStandard;
        public static event d_Card SelectedCardInHand;
        public static event d_Card DeselectedCardInHand;

        public static void SelectCardInHand(Card card)
        {
            if (SelectedCardInHand != null)
            {
                SelectedCardInHand(card);
            }
        }

        public static void DeselectCardInHand(Card card)
        {
            if (DeselectedCardInHand != null)
            {
                DeselectedCardInHand(card);
            }
        }

        public static void SetCardsInHandToSelectable()
        {
            if (CardsInHandSetToSelectable != null)
            {
                CardsInHandSetToSelectable();
            }
        }

        public static void SetCardsInHandToStandard()
        {
            if (CardsInHandSetToStandard != null)
            {
                CardsInHandSetToStandard();
            }
        }
    }

    public static class GUI
    {
        public static event d_Bool ConfirmButtonEnabled;
        public static event d_NoArgVoid ConfirmButtonDisabled;
        public static event d_NoArgVoid ConfirmButtonPressed;

        public static event d_NoArgVoid EndTurnButtonEnabled;
        public static event d_NoArgVoid EndTurnButtonDisabled;

        public static void EnableConfirmButton(bool clickable)
        {
            if (ConfirmButtonEnabled != null)
            {
                ConfirmButtonEnabled(clickable);
            }
        }

        public static void DisableConfirmButton()
        {
            if (ConfirmButtonDisabled != null)
            {
                ConfirmButtonDisabled();
            }
        }

        public static void PressConfirmButton()
        {
            if (ConfirmButtonPressed != null)
            {
                ConfirmButtonPressed();
            }
        }

        public static void EnableEndTurnButton()
        {
            if (EndTurnButtonEnabled != null)
            {
                EndTurnButtonEnabled();
            }
        }

        public static void DisableEndTurnButton()
        {
            if (EndTurnButtonDisabled != null)
            {
                EndTurnButtonDisabled();
            }
        }
    }

        public static class Abilities
    {
        public static event d_CardAbility CardAbilityResolved;

        public static void ResolveCardAbility(CardAbility cardAbility)
        {
            if (CardAbilityResolved != null)
            {
                CardAbilityResolved(cardAbility);
            }
        }
    }

    public static event d_NoArgVoid OnStartGame;
    public static event d_TurnPhase OnTurnPhaseBegin;
    public static event d_TurnPhase OnTurnPhaseEnd;
    public static event d_TurnPhase OnTryTurnPhaseChange;
    public static event d_int OnTryDrawCard;
    public static event d_int OnActivatePlayer;
    public static event d_Manacost OnAddMarkersToMarkerPool;
    public static event d_NoArgVoid OnOpponentHealthClicked;
    public static event d_int OnAddDamageToPool;
    public static event d_IntPlayer OnHealPlayer;
    public static event d_IntPlayer OnDealDamageToPlayer;
    public static event d_NoArgVoid OnTurnEnd;
    public static event d_string OnDisplayTextInAnnouncement;
    public static event d_NoArgVoid OnCollectTokens;

    public static void StartGame()
    {
        if (OnStartGame != null)
        {
            OnStartGame();
        }
    }

    public static void TryChangeTurnPhase(TurnPhase turnPhase)
    {
        TurnPhase previousTurnPhase = TurnPhases.CurrentTurnPhase;
        if (OnTryTurnPhaseChange != null)
        {
            OnTryTurnPhaseChange(turnPhase);
        }
    }

    //public static void ChangeTurnPhase(TurnPhase turnPhase, TurnPhase previousTurnPhase)
    //{
    //    if (OnTurnPhaseEnd != null)
    //        OnTurnPhaseEnd(previousTurnPhase);

    //    //if (previousTurnPhase == TurnPhase.End)
    //    //    EndTurn();

    //    if (OnTurnPhaseBegin != null)
    //        OnTurnPhaseBegin(turnPhase);
    //}

    public static void TryDrawCard(int value)
    {
        if (OnTryDrawCard != null)
        {
            OnTryDrawCard(value);
        }
    }

    public static void ActivatePlayer(int number)
    {
        DisplayTextInAnnouncement("Player " + (number+1));

        if (OnActivatePlayer != null)
        {
            OnActivatePlayer(number);
        }
    }

    public static void AddMarkersToMarkerPool(ManaCost manaCost)
    {
        if (OnAddMarkersToMarkerPool != null)
            OnAddMarkersToMarkerPool(manaCost);
    }

    public static void ClickedOpponentsHealth()
    {
        if (OnOpponentHealthClicked != null)
            OnOpponentHealthClicked();
    }

    public static void AddDamageToPool(int damageAmount)
    {
        if (OnAddDamageToPool != null)
            OnAddDamageToPool(damageAmount);
    }

    public static void HealPlayer(int healAmount, Player player)
    {
        if (OnHealPlayer != null)
            OnHealPlayer(healAmount, player);
    }

    public static void DealDamageToPlayer(int damageAmount, Player player)
    {
        if (OnDealDamageToPlayer != null)
            OnDealDamageToPlayer(damageAmount, player);
    }

    public static void EndTurn()
    {
        if (OnTurnEnd != null)
            OnTurnEnd();
    }

    public static void DisplayTextInAnnouncement(string text)
    {
        if (OnDisplayTextInAnnouncement != null)
            OnDisplayTextInAnnouncement(text);
    }

    public static void CollectTokens()
    {
        if (OnCollectTokens != null)
            OnCollectTokens();
    }
    
    
    /* Attempt at making an event wrapper
    public class CustomEvent
    {
        private readonly bool _unlimitedSubscriptions = true;
        bool _alreadySubscribed = false;

        public CustomEvent(bool canHaveUnlimitedSubscribers = true)
        {
            _unlimitedSubscriptions = canHaveUnlimitedSubscribers;
        }

        System.Action<object> _singleArgSubscriptions;
        System.Action _noArgSubscriptions;

        public void Subscribe(System.Action method)
        {
            if (!_unlimitedSubscriptions && _alreadySubscribed)
                return;
            _alreadySubscribed = true;
            _noArgSubscriptions = method;
        }

        public void Subscribe(System.Action<object> method)
        {
            if (!_unlimitedSubscriptions && _alreadySubscribed)
                return;
            _alreadySubscribed = true;
            _singleArgSubscriptions = method;
        }

        public void Unsubscribe(System.Action<object> method)
        {
            _singleArgSubscriptions -= method;
        }

        public void Invoke()
        {
            if (_singleArgSubscriptions != null)
            {
                _singleArgSubscriptions();
            }
            if (_noArgSubscriptions != null)
            {
                _noArgSubscriptions
            }
        }

        public void Clear()
        {
            _singleArgSubscriptions = null;
        }
    }
    */
}
