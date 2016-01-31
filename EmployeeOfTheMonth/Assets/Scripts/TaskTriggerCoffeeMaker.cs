using UnityEngine;
using System.Collections;

public class TaskTriggerCoffeeMaker : TaskTrigger {

    protected override void OnTriggerEnter( Collider collider )
    {
        base.OnTriggerEnter( collider );
        Interactable obj = collider.GetComponent<Interactable>();
        if (obj != null && obj.InteractableObjectType == Goals.Objects.EmptyMug)
        {
            obj.InteractableObjectType = Goals.Objects.FilledCoffeeMug;
            obj.Description = "Ahh, fresh coffee. I hope my stomach can handle it today. It keeps me going, so I guess It's good for me...";
            if ( Random.Range( 0, 3 ) == 0 )
            {
                TaskManager.AddTask( new Task( "Pour the coffee down the sink", Task.TaskType.Optional, new Objective( Goals.Objects.FilledCoffeeMug, Goals.Triggers.Sink ), new Reward( -50, 0 ), new TimeConstraint( new TaskTime( 0, Random.Range( 10, 30 ) ) ) ) );
            }            
        }
    }
}
