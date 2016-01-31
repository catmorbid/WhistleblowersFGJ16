using UnityEngine;
using System.Collections;
using System;

public class TaskTriggerCoffeeMaker : TaskTrigger {

    protected override void OnTriggerEnter( Collider collider )
    {
        base.OnTriggerEnter( collider );
        Interactable obj = collider.GetComponent<Interactable>();
        if (obj != null && obj.InteractableObjectType == Goals.Objects.EmptyMug)
        {
            try {
                ( (DraggableAltStateInteractable) obj ).SetAlternateState( true );
            } catch (Exception e )
            {
                Debug.LogException( e );
            }
            if ( UnityEngine.Random.Range( 0, 3 ) == 0 )
            {
                TaskManager.AddTask( new Task( "Pour the coffee down the sink", Task.TaskType.Optional, new Objective( Goals.Objects.FilledCoffeeMug, Goals.Triggers.Sink ), new Reward( -50, 0 ), new TimeConstraint( new TaskTime( 0, UnityEngine.Random.Range( 10, 30 ) ) ) ) );
            }            
        }
    }
}
