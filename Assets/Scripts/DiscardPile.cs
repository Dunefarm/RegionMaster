using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiscardPile : MonoBehaviour {

    public List<Card> Cards = new List<Card>();

    public void PutCardInDiscardPile(Card card)
    {
        card.transform.position = Vector3.one * 1000;
        Cards.Add(card);
        ResizeDiscardPile();
    }

    void ResizeDiscardPile()
    {
        transform.localScale = new Vector3(1, 1, Cards.Count);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
