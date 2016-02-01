using UnityEngine;
using System.Collections;

public class TaskTriggerOven : TaskTrigger {

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter( other );
        Interactable obj = other.GetComponent<Interactable>();
        if (obj != null && obj.InteractableObjectType == Goals.Objects.Food)
        {
            AudioSrc.Play();
            obj.InteractableObjectType = Goals.Objects.CookedFood;
            obj.Description = "Cooked food has much better flavour. I like cooked food better";
            
            if (Random.Range(0,3)==0)
            {
                PlayerText.ShowSpeechBubble( "I'm pretty sure this food is not healthy. I could just throw it away instead of eating it", 3f );
                TaskManager.AddTask( new Task( "Throw the food into the trash bin", Task.TaskType.Optional, new Objective( Goals.Objects.CookedFood, Goals.Triggers.Trashbin ), new Reward( -75, 0 ), new TimeConstraint( new TaskTime( 0, 30 ) ) ) );
            }
            else
            {
                PlayerText.ShowSpeechBubble( "Oh boy, food! I'm going to enjoy this cooked meal. Bless you Father for you gifts!", 2f );
            }
        }
    }
    	
}
