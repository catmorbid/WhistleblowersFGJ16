using UnityEngine;
using System.Collections;

public class TaskTriggerDestroyer : TaskTrigger {

    public Goals.Objects[] DestroyableObjects;

    protected override void OnTriggerEnter( Collider collider )
    {
        base.OnTriggerEnter( collider );
        Interactable obj = collider.GetComponent<Interactable>();
        if ( obj != null )
        {
            foreach ( Goals.Objects o in DestroyableObjects )
            {
                if ( obj.InteractableObjectType == o )
                {
                    GameObject.Destroy( obj );
                    return;
                }
            }
        }
    }
}
