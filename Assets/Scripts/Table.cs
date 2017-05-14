using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {

    public Shop Shop;
    public Grid Grid;
    public Markers MarkerHolder;
    public HealthDisplay YourHealth;
    public HealthDisplay OpponentsHealth;
    public DamageDisplay DamageDisplay;
    public Transform DeckPlacement;
    public Transform DiscardPlacement;

    // Use this for initialization
    void Awake ()
    {
        DeckPlacement.gameObject.SetActive(false);
        DiscardPlacement.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
