using UnityEngine;
using System.Collections;

public class InteractableCabinet : Interactable {

    string defaultInteractionMsg;
    protected override void Start()
    {
        defaultInteractionMsg = interactionMessage;
        base.Start();
    }
    public override void Interact( Transform interactorTransform )
    {
        if (TaskManager.HasTaskWithGoal(Goals.Objects.Cabinet))
        {
            GetSpawner().Spawn();
            interactionMessage = "Ok, I'll find the documents I need to.";
        }
        else
        {
            interactionMessage = defaultInteractionMsg;
        }
        base.Interact( interactorTransform );
    }

    public Spawner GetSpawner()
    {
        return GetComponent<Spawner>();
    }
}
