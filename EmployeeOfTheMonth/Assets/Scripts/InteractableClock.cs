using UnityEngine;
using System.Collections;

public class InteractableClock : Interactable {

    public override void Interact( Transform interactorTransform )
    {
        base.Interact( interactorTransform );
        PlayerText.ShowSpeechBubble( "Current Time Multiplier: " + GameClock.BaseTimeMultiplier, 4f );
    }

    public override void InteractLongPress( Transform interactorTransform )
    {
        base.InteractLongPress( interactorTransform );
        GameClock.MaxTimeOverride();        
    }
}
