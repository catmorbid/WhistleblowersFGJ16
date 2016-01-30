using UnityEngine;
using System.Collections;

public class TaskTrigger : MonoBehaviour {

    public Goals.Triggers TriggerType;
    public event TaskTriggeredAction TaskTriggerEvent;
    public delegate void TaskTriggeredAction( TaskTrigger trigger, Interactable triggerObj );
	
    // Use this for initialization
	void Start () {
        TaskManager.RegisterTrigger( this );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        Interactable obj = collider.GetComponentInChildren<Interactable>();
        if (TaskTriggerEvent != null && obj != null)
        {
            Debug.Log( "Task Trigger Event launcher: " + obj.name + " triggered on " + gameObject.name );
            TaskTriggerEvent( this, obj );
        }

    }
}
