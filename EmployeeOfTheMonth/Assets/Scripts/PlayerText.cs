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
        Debug.Log( m_text );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void ShowSpeechBubble(string text, float duration)
    {
        m_singleton.CancelInvoke();
        Debug.Log( "Speech bubble" );
        if (m_singleton.Text.text == text)
        {
            m_singleton.Text.enabled = false;
            m_singleton.Text.text = "";
        }
        else
        {
            m_singleton.Text.enabled = true;
            m_singleton.StartCoroutine( "TextAppearsTyped", text.ToCharArray() );
            //m_singleton.Text.text = text;
            
        }
        m_singleton.Invoke( "clearText", text.Length/10f );
    }
    public void clearText()
    {
        Text.enabled = false;
        Text.text = "";
    }

    public IEnumerator TextAppearsTyped( char[] characters )
    {
        string s = "";
        foreach (char c in characters)
        {
            s += c;
            m_singleton.Text.text = s;
            yield return null;
        }
    }
}
