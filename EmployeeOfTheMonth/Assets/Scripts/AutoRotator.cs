using UnityEngine;
using System.Collections;

public class AutoRotator : MonoBehaviour {
    public float Speed = 10f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate( 0f, Time.deltaTime*Speed, 0f );
	}
}
