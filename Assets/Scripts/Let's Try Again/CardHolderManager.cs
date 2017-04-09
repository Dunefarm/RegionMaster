using UnityEngine;
using System.Collections;

public class CardHolderManager : MonoBehaviour {

    public GameObject PhysicalCardPrefab;

    public CardHolder_Deck Deck = new CardHolder_Deck();
    public CardHolder Hand = new CardHolder();
    public CardHolder DiscardPile = new CardHolder();

	// Use this for initialization
	void Start ()
    {
        Deck.AddCard(New_Card.CreatePlaceholderCard(1));
        Deck.AddCard(New_Card.CreatePlaceholderCard(2));
        Deck.AddCard(New_Card.CreatePlaceholderCard(3));
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            New_Card newCard = Deck.DrawCard();
            if(newCard != null)
            {
                Hand.AddCard(newCard);
                PhysicalCard physCard = Instantiate(PhysicalCardPrefab).GetComponent<PhysicalCard>();
                physCard.AssignCard(newCard);
            }
            foreach(New_Card card in Hand.Cards)
            {
                print("Card cost: " + card.Cost);
            }
        }
	}
}
