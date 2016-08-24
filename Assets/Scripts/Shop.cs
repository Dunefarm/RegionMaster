using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public GameObject CardPrefab; // for now...

    public Card[,] Cards = new Card[3, 2];
    public Vector3[,] CardPlacements = new Vector3[3, 2];

	// Use this for initialization
	void Start () {
	    for(int i = 0; i < Cards.GetLength(0); i++)
        {
            for (int j = 0; j < Cards.GetLength(1); j++)
            {
                CardPlacements[i, j] = new Vector3(i * 3.35f, j * -4.45f, 0) + transform.position;
                GameObject card = (GameObject)Instantiate(CardPrefab, CardPlacements[i, j], transform.rotation);
                Cards[i, j] = card.GetComponent<Card>();
                Cards[i, j].PutInShop(new Finite2DCoord(i, j));
                card.transform.parent = transform; // for now...
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 CoordToVector3(Finite2DCoord coord)
    {
        return new Vector3(coord.x * 3.35f, coord.y * -4.45f, 0) + transform.position;
    }

    public void RemoveFromShop(Card card)
    {
        Cards[card.ShopCoord.x, card.ShopCoord.y] = null;
    }
}
