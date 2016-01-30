using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerText : MonoBehaviour {

    private static PlayerText m_singleton;

    private Text m_text;
	
    public Text Text
    {
        get
        {
            return m_text;
        }
    }

    void Awake()
    {
        m_singleton = this;
    }
    // Use this for initialization
	void Start () {
        m_text = GameObject.Find( "InfoTextBox" ).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void ShowSpeechBubble(string text, float duration)
    {
        if (m_singleton.Text.text == text)
        {
            m_singleton.Text.enabled = false;
        }
        else
        {
            m_singleton.Text.text = text;
            m_singleton.Text.enabled = true;
        }

    }
}
