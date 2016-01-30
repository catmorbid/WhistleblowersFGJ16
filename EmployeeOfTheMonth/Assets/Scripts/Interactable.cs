using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {

    public event ObjectInteractedAction ObjectInteractionEvent;
    public delegate void ObjectInteractedAction( Interactable obj );
    public Goals.Objects InteractableObjectType;
    public string m_description;
    private bool m_interaction;
    public bool Busy
    {
        get
        {
            return m_interaction;
        }
        set
        {
            m_interaction = value;
        }
    }

    protected virtual void Awake()
    {
        if (m_description == "")
        {
            m_description = "Placeholder Description for Object " + gameObject.name;
        }
            
    }

    protected virtual void Start()
    {
        TaskManager.RegisterInteraction( this );
    }
    public virtual void Interact(Transform interactorTransform)
    {
        Debug.Log( "Interaction" );
        if (ObjectInteractionEvent != null)
        {
            Debug.Log( "Firing interaction event" );
            ObjectInteractionEvent( this );
        }
    }
    public virtual void InteractLongPress(Transform interactorTransform)
    {
    }
    public virtual void AltInteract(Transform interactorTransform)
    {
        PlayerText.ShowSpeechBubble( m_description, 5f );
        //Debug.Log("Secondary Interaction not implemented!");
    }
    public virtual void AltInteractLongPress(Transform interactorTransform)
    {
    }

}
