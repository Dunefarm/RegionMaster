using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GUIManager : MonoBehaviour {

    public MegaManager MegaMan;

    public GameObject ButtonCollectObj;
    public GameObject ButtonEndTurnObj;

    public GameObject ButtonConfirmObj;
    public Text ConfirmText;
    private bool _confirmButtonInteractable;
    private Button _confirmButton;

    public Image PlayerTurnIcon;

    private void SetConfirmButtonInteractable(bool interactable)
    {
        _confirmButtonInteractable = interactable;
        _confirmButton.interactable = interactable;
        ConfirmText.color = interactable ? new Color32(50, 50, 50, 255): new Color32(150, 150, 150, 255);
    }

    void Awake()
    {
        //EventManager.Phases.MainPhase_OnEnter += EnableButtonCollect;
        //EventManager.Phases.MainPhase_OnExit += DisableButtonCollect;

        _confirmButton = ButtonConfirmObj.GetComponent<Button>();

        EventManager.Phases.MainPhase_OnEnter += EnableButtonEndTurn;
        EventManager.GUI.EndTurnButtonEnabled += EnableButtonEndTurn;
        EventManager.Phases.MainPhase_OnExit += DisableButtonEndTurn;
        EventManager.GUI.EndTurnButtonDisabled += DisableButtonEndTurn;

        EventManager.GUI.ConfirmButtonEnabled += EnableButtonConfirm;
        EventManager.GUI.ConfirmButtonDisabled += DisableButtonConfirm;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //GUI button
    public void EndTurn()
    {
        MegaMan.EndTurn();
    }

    public void Confirm()
    {
        if(_confirmButtonInteractable)
        {
            EventManager.GUI.PressConfirmButton();
        }
    }

    public void EnableButtonCollect()
    {
        ButtonCollectObj.SetActive(true);
    }

    public void DisableButtonCollect()
    {
        ButtonCollectObj.SetActive(false);
    }

    public void EnableButtonEndTurn()
    {
        ButtonEndTurnObj.SetActive(true);
    }

    public void DisableButtonEndTurn()
    {
        ButtonEndTurnObj.SetActive(false);
    }

    public void EnableButtonConfirm(bool interactable)
    {
        ButtonConfirmObj.SetActive(true);
        SetConfirmButtonInteractable(interactable);
    }

    public void DisableButtonConfirm()
    {
        ButtonConfirmObj.SetActive(false);
        SetConfirmButtonInteractable(false);
    }

    //public void CollectAndGoToBuyPhase()
    //{
    //    //List<GridCell> cells = GridManager.GetCompletedRegions(MegaManager.CurrentPlayer); //TODO: Make into event
    //    //List<Token> tokens = GridManager.PullTokensFromGrid(cells);
    //    //MegaManager.CollectionManager.AddTokensToPool(tokens);
    //    //EventManager.CollectTokens();
    //    EventManager.TryChangeTurnPhase(TurnPhase.Buy);
    //}
}
