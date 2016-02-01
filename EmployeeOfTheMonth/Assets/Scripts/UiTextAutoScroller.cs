using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiTextAutoScroller : MonoBehaviour {

    public float max = 5000f;
    public float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RectTransform rt = (RectTransform)gameObject.transform;
        rt.sizeDelta = rt.sizeDelta + new Vector2( 0f, Time.deltaTime * speed);
        Debug.Log( rt.sizeDelta );
        if (rt.sizeDelta.y >= max)
        {
            Debug.Log( "QUIT!!!" );
            Application.Quit();
        }
	}
}
