using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player
{
    
    public int PlayerNumber;
    public Deck Deck;
    public DiscardPile DiscardPile;
    public Hand Hand;
    public Transform DiscardPileTranform;
    public PlayerHealth Health;
    public DamageDisplay DamageDisplay;
    public Damage Damage;

    private Transform HAND_TRANSFORM;

    public Player()
    {
        PlayerNumber = -1;
        //empty placeholder object
    }

    public Player(int playerNo, GameObject discardPilePrefab, CameraManager camMan)
    {
        PlayerNumber = playerNo;

        SetupDeck();
        SetupDiscardPile(discardPilePrefab);
        SetupHand(camMan);
        SetupHealth();

        Damage = new Damage();

        EventManager.OnActivatePlayer += ActivatePlayer;
    }

    public static Player GetCurrentPlayer
    {
        get { return MegaManager.CurrentPlayer; }
    }

    public static Player GetCurrentOpponent
    {
        get { return MegaManager.CurrentOpponent; }
    }

    private void SetupDeck()
    {
        GameObject deckPrefab = Resources.Load("Prefabs/Deck") as GameObject;
        Vector3 deckPlacement = MegaManager.Table.DeckPlacement.position;
        Deck = (MonoBehaviour.Instantiate(deckPrefab, deckPlacement, Quaternion.identity) as GameObject).GetComponent<Deck>();
        Deck.SetOwner(this);
    }

    private void SetupDiscardPile(GameObject discardPilePrefab)
    {
        Vector3 discardPlacement = MegaManager.Table.DiscardPlacement.position;
        DiscardPile = (MonoBehaviour.Instantiate(discardPilePrefab, discardPlacement, Quaternion.identity) as GameObject).GetComponent<DiscardPile>();
        DiscardPile.SetOwner(this);
        DiscardPileTranform = DiscardPile.transform;
    }

    private void SetupHand(CameraManager camMan)
    {
        HAND_TRANSFORM = new GameObject().transform;
        HAND_TRANSFORM.position = camMan.transform.TransformPoint(-7.2f, -5.6f, 33.8f);
        HAND_TRANSFORM.rotation = camMan.transform.rotation;
        HAND_TRANSFORM.Rotate(30.12f, 0, 0, Space.Self);
        Hand = new Hand(HAND_TRANSFORM);
    }

    private void SetupHealth()
    {
        Health = new PlayerHealth();
    }

    public void ActivatePlayer(int number)
    {
        if (number == PlayerNumber)
        {
            StartPlayersTurn();
        }
        else
        {
            EndPlayersTurn();
        }
    }

    public void DrawCards(int numberOfCards)
    {
        List<Card> drawnCards = Deck.DrawCards(numberOfCards);
        Hand.PutCardsInHand(drawnCards);
    }

    public void StartPlayersTurn()
    {
        Deck.gameObject.SetActive(true);
        DiscardPile.gameObject.SetActive(true);
        Health.SetDisplay(MegaManager.Table.YourHealth);
    }

    public void EndPlayersTurn()
    {
        Deck.gameObject.SetActive(false);
        DiscardPile.gameObject.SetActive(false);
        if (Health == null)
            MonoBehaviour.print("Health is null");
        if (MegaManager.Table == null)
            MonoBehaviour.print("Table is null");
        if (MegaManager.Table.OpponentsHealth == null)
            MonoBehaviour.print("Opponent health is null");
        Health.SetDisplay(MegaManager.Table.OpponentsHealth);
    }

    public bool IsActualPlayer()
    {
        return PlayerNumber != -1;
    }

    public bool TryToBuyCard(Card card)
    {
        if(MegaManager.CollectionManager.CheckIfCanAfford(card.ManaCost))
        {
            BuyCard(card);
            return true;
        }

        MegaManager.Shop.ReturnCard(card);
        return false;
    }

    private void BuyCard(Card card)
    {
        MegaManager.CollectionManager.Subtract(card.ManaCost);
        MegaManager.Shop.PullCardFromShop(card);
        DiscardPile.PutCardInDiscardPile(card);
    }

    private void AddDamageToPool(int amount)
    {
        //DamageDisplay.SetDamageDisplayed(DamageDisplay.Damage + amount);
    }

    private void OnOpponentHealthClicked()
    {
        if (TurnPhases.IsCurrentPhase(TurnPhase.Main))
        {
           // DealDamageToOpponent(DamageDisplay.Damage);
            //DamageDisplay.SetDamageDisplayed(0);
        }
    }

    private void DealDamageToOpponent(int amount)
    {
        MegaManager.Players[(PlayerNumber + 1) % 2].TakeDamage(amount);
    }

    private void TakeDamage(int amount)
    {
        Health.TakeDamage(amount);
    }
}
