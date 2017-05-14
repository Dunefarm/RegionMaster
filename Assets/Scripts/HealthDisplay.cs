using UnityEngine;
using System.Collections;
using TMPro;

public class HealthDisplay : CustomBehaviour {

    public bool OpponentHealth;
    public int CurrentHealth = 20;
    public TextMeshPro Display;

    public void SetHealth(int newHealth)
    {
        Display.text = newHealth.ToString();
    }

    public override void OnMouseClicked()
    {
        if (OpponentHealth)
            EventManager.ClickedOpponentsHealth();
    }
}
