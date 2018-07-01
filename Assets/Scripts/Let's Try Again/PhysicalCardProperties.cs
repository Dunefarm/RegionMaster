using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PhysicalCardProperties : MonoBehaviour {

    [System.Serializable]
    public class Frames
    {
        public GameObject ColorlessFrame;
        public GameObject RedFrame;
        public GameObject GreenFrame;
        public GameObject BlueFrame;

        public void TurnOffAllFrames()
        {
            ColorlessFrame.SetActive(false);
            RedFrame.SetActive(false);
            GreenFrame.SetActive(false);
            BlueFrame.SetActive(false);
        }
    }

    public TextMeshPro CardCost;
    public TextMeshPro CardName;
    public TextMeshPro CardRules;
    public Frames frames;
    public GameObject Outline;

    private Dictionary<CardColor, GameObject> _framesByColor = new Dictionary<CardColor, GameObject>();

    void Awake()
    {
        _framesByColor.Add(CardColor.Colorless, frames.ColorlessFrame);
        _framesByColor.Add(CardColor.Red, frames.RedFrame);
        _framesByColor.Add(CardColor.Green, frames.GreenFrame);
        _framesByColor.Add(CardColor.Blue, frames.BlueFrame);
    }

    public void SetName(string name)
    {
        CardName.text = name;
    }

    public void SetRules(string rules)
    {
        CardRules.text = rules;
    }

    public void SetColor(CardColor color)
    {
        frames.TurnOffAllFrames();
        _framesByColor[color].SetActive(true);
    }

    public void SetCost(ManaCost manaCost)
    {
        string cost = "<b><color=#D23F3FFF>";
        cost += new string('R', manaCost.Red);
        cost += "</color></b>";

        cost += "<b><color=#23701DFF>";
        cost += new string('G', manaCost.Green);
        cost += "</color></b>";

        cost += "<b><color=#3E51D2FF>";
        cost += new string('B', manaCost.Blue);
        cost += "</color></b>";

        CardCost.text = cost;
    }

    public void SetOutlineActive(bool newState)
    {
        Outline.SetActive(newState);
    }

}
