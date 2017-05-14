using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnnouncementDisplay : MonoBehaviour {

    public GameObject AnouncementTextPrefab;

    private static List<AnnouncementTextFade> _texts = new List<AnnouncementTextFade>();
    private static Transform _transform;

	// Use this for initialization
	void Awake ()
    {
        EventManager.OnDisplayTextInAnnouncement += DisplayText;
        _transform = transform;
    }

    private void DisplayText(string text)
    {
        Transform textObj = (Instantiate(AnouncementTextPrefab, _transform, false) as GameObject).transform;
        textObj.position = _transform.position + Vector3.down * _texts.Count * 1.5f; //MAGIC NUMBER!
        AnnouncementTextFade textFade = textObj.GetComponentInChildren<AnnouncementTextFade>();
        textFade.SetText(text);
        _texts.Add(textFade);
    }

    public static void TextHasFaded(AnnouncementTextFade text)
    {
        _texts.Remove(text);
        Destroy(text.gameObject);
        RearrangeText();
    }

    private static void RearrangeText()
    {
        for(int i = 0; i < _texts.Count; i++)
        {
            _texts[i].Transform.position = _transform.position + Vector3.down * i * 1.5f; //MAGIC NUMBER!
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
