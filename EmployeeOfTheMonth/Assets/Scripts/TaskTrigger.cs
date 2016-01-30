using UnityEngine;
using System.Collections;

public class TaskTrigger : MonoBehaviour {

    public event TaskTriggeredAction TaskTriggerEvent;
    public delegate void TaskTriggeredAction( TaskTrigger trigger, TaskObject triggerObj );
	// Use this for initialization
	void Start () {
        TaskManager.RegisterTrigger( this );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        TaskObject obj = collider.GetComponentInChildren<TaskObject>();
        if (TaskTriggerEvent != null && obj != null)
        {
            TaskTriggerEvent( this, obj );
        }

    }
}
