using UnityEngine;
using System.Collections;

public class InteractableDoor : Interactable
{

    public Vector3 OpenPosition;
    public Vector3 ClosePosition;
    private bool m_open = false;

    public override void Interact( Transform interactorTransform )
    {
        Debug.Log( "Interacting with door" );
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

    public IEnumerator SetDoorStatus( bool open )
    {
        Vector3 target;
        if ( open )
            target = OpenPosition;
        else
            target = ClosePosition;

        m_open = !m_open;

        float d = (target - transform.localPosition).magnitude;
        while ( d > 0.01f )
        {
            Debug.Log( "Distance " + d + "Transform " + transform.localPosition + " target " + target );
            Vector3 dif = target - transform.localPosition;
            transform.Translate( dif * Time.deltaTime, Space.Self );
            yield return null;
            d = (target - transform.localPosition).magnitude;
        }
        Debug.Log( "Done" );
        
    }

}
