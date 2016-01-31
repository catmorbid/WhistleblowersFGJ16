using UnityEngine;
using System.Collections;

public class InteractableDoor : Interactable
{

    public Vector3 OpenPosition;
    public Vector3 ClosePosition;
    private bool m_open = false;
    private bool m_busy = false;

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
        base.Interact(interactorTransform);
        Debug.Log( "Interacting with door" );
        if ((int)GameClock.State < (int) GameClock.DayState.WorkStart)
        {
            PlayerText.ShowSpeechBubble("No, I can't go to work yet. I must finish my chores. And be on time. Yes, on time!", 5f);
        }
        else
        if ( !m_busy )
        {
            if ( m_open )
            {
                Debug.Log( "Door Closing" );
                StartCoroutine( "SetDoorStatus", false );
            }
            else
            {
                Debug.Log( "Door Opening" );
                StartCoroutine( "SetDoorStatus", true );
            }
        }
    }

    public IEnumerator SetDoorStatus( bool open )
    {
        m_busy = true;
        Vector3 target;
        if ( open )
            target = OpenPosition;
        else
            target = ClosePosition;

        m_open = !m_open;

        float d = (target - transform.localPosition).magnitude;
        while ( d > 0.01f )
        {
            //Debug.Log( "Distance " + d + "Transform " + transform.localPosition + " target " + target );
            Vector3 dif = target - transform.localPosition;
            transform.Translate( dif * Time.deltaTime, Space.Self );
            yield return null;
            d = ( target - transform.localPosition ).magnitude;
        }
        m_busy = false;
        //Debug.Log( "Done" );

    }

}
