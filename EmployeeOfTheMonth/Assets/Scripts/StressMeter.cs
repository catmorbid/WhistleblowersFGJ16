using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StressMeter : MonoBehaviour {

    Image m_image;
	// Use this for initialization
	void Start () {
        m_image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        m_image.fillAmount = PlayerScore.GetStressPercentage();
	}
}
