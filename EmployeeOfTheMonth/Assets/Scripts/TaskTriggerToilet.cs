using UnityEngine;
using System.Collections;

public class TaskTriggerToilet : TaskTriggerDestroyer {

    public void Interact( Transform interactorTransform )
    {
        PlayerText.ShowSpeechBubble( "Ahhh....", 1f );
    }

    protected override void OnTriggerEnter( Collider collider )
    {

        // Call after because thing will be destroyed...
        base.OnTriggerEnter( collider );
    }
}
