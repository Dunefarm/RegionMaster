using UnityEngine;
using System.Collections;
using TMPro;

public class AnnouncementTextFade : MonoBehaviour {

    public float FadeTime = 5;

    [HideInInspector]
    public Transform Transform;

    private float _timePassed = 0;
    private TextMeshPro TextMesh;

    public void SetText(string text)
    {
        TextMesh.text = text;
    }

	// Use this for initialization
	void Awake ()
    {
        Transform = transform;
        TextMesh = gameObject.GetComponent<TextMeshPro>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        _timePassed += Time.deltaTime;
        float weight = 1 - (_timePassed / FadeTime);
        TextMesh.color = new Color(TextMesh.color.r, TextMesh.color.g, TextMesh.color.b, weight);
        if (_timePassed >= FadeTime)
            AnnouncementDisplay.TextHasFaded(this);
	}
}
