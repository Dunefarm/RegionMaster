using UnityEngine;
using System.Collections;

public class Player
{
    
    public int PlayerNumber;
    public Deck Deck;
    public DiscardPile DiscardPile;
    public Hand Hand;
    public Transform DiscardPileTranform;

    private Vector3 DECK_PLACEMENT = new Vector3(5.22f, -6.7f, 0);
    private Vector3 DISCARD_PILE_PLACEMENT = new Vector3(8.76f, -6.7f, 0);
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

        EventManager.OnActivatePlayer += ActivatePlayer;
    }

    private void SetupDeck()
    {
        GameObject deckPrefab = Resources.Load("Prefabs/Deck") as GameObject;
        Deck = (MonoBehaviour.Instantiate(deckPrefab, DECK_PLACEMENT, Quaternion.identity) as GameObject).GetComponent<Deck>();
        Deck.SetOwner(this);
    }

    private void SetupDiscardPile(GameObject discardPilePrefab)
    {
        DiscardPile = (MonoBehaviour.Instantiate(discardPilePrefab, DISCARD_PILE_PLACEMENT, Quaternion.identity) as GameObject).GetComponent<DiscardPile>();
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

    public void DrawCard(int numberOfCards)
    {
        Deck.DrawCard(numberOfCards);
    }

    public void StartPlayersTurn()
    {
        Deck.gameObject.SetActive(true);
        DiscardPile.gameObject.SetActive(true);
    }

    public void EndPlayersTurn()
    {
        Deck.gameObject.SetActive(false);
        DiscardPile.gameObject.SetActive(false);
    }

    public bool IsActualPlayer()
    {
        return PlayerNumber != -1;
    }
}
