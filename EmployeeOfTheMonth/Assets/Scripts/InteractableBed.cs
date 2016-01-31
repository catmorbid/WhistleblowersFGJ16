using UnityEngine;
using System.Collections;

public class InteractableBed : Interactable {

    protected override void Awake()
    {
        base.Awake();
    }
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    public override void Interact( Transform interactorTransform )
    {
        base.Interact( interactorTransform );
        if (GameClock.State != GameClock.DayState.SleepBegin)
        {
            PlayerText.ShowSpeechBubble( "No, it's not time to sleep yet. No I cannot. I will not.", 5f );                
        }
        else
        {
            PlayerText.ShowSpeechBubble( "Yes, it's time to sleep now. Yes. I will do that.", 5f );
            GameClock.EndDay();
        }
    }

}
