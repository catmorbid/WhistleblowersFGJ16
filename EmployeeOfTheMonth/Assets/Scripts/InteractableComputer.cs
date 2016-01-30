using UnityEngine;
using System.Collections;

public class InteractableComputer : Interactable {

    protected override void Awake()
    {
        base.Awake();
    }
    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	}
    public override void Interact( Transform interactorTransform )
    {
        base.Interact( interactorTransform );
    }
}
