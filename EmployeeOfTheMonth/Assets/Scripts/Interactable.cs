using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {

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

    public virtual void Interact(Transform interactorTransform)
    {
        Debug.Log("Interaction not implemented!");
    }
    public virtual void InteractLongPress(Transform interactorTransform)
    {
        Debug.Log("Long Press Interaction not implemented!");
    }
    public virtual void AltInteract(Transform interactorTransform)
    {
        PlayerText.ShowSpeechBubble( m_description, 10f );
        //Debug.Log("Secondary Interaction not implemented!");
    }
    public virtual void AltInteractLongPress(Transform interactorTransform)
    {
        Debug.Log("Secondary Long Press Interaction not implemented!");
    }

}
