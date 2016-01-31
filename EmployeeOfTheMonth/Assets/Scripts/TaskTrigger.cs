using UnityEngine;
using System.Collections;

public class TaskTrigger : MonoBehaviour {

    public Goals.Triggers TriggerType;
    public event TaskTriggeredAction TaskTriggerEvent;
    public delegate void TaskTriggeredAction( TaskTrigger trigger, Interactable triggerObj );

    protected virtual void Start()
    {
        TaskManager.RegisterTrigger( this );
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        Interactable obj = collider.GetComponentInChildren<Interactable>();
        if (TaskTriggerEvent != null && obj != null)
        {
            Debug.Log( "Task Trigger Event launcher: " + obj.name + " triggered on " + gameObject.name );
            TaskTriggerEvent( this, obj );
        }

    }
}
