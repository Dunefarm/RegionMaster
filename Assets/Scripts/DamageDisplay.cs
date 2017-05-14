using UnityEngine;
using System.Collections;
using TMPro;

public class DamageDisplay : MonoBehaviour {

    public TextMeshPro TextMesh;
    public int Damage
    {
        get { return _damage; }
        set
        {
            SetDamageDisplayed(value);
        }
    }

    private int _damage;

    void Start()
    {
        EventManager.OnActivatePlayer += OnActivatePlayer;
    }

    private void SetDamageDisplayed(int amount)
    {
        _damage = amount;
        TextMesh.text = amount.ToString();
    }

    void OnActivatePlayer(int playerNumber)
    {
        Damage = 0;
    }
}
