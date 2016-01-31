using UnityEngine;
using System.Collections;

public class DraggableAltStateInteractable : DraggableRigidbody {

    
    public string altString;
    public Goals.Objects altObjectState;
    public string altDescription;
    public DefaultAction AltAction = DefaultAction.ResetState;
    
    private string defaultString;
    private string defaultDescription;
    private Goals.Objects defaultState;

    protected override void Start()
    {
        defaultString = interactionMessage;
        defaultDescription = Description;
        defaultState = InteractableObjectType;
        base.Start();
    }
    public override void Interact( Transform interactorTransform )
    {
        if (InteractableObjectType == altObjectState)
        {
            interactionMessage = altString;
            Description = altDescription;
            AlternateAction();
        }
        else
        {
            interactionMessage = defaultString;
            Description = defaultDescription;
        }
        base.Interact( interactorTransform );
    }

    public virtual void AlternateAction()
    {
        Debug.Log( gameObject.name + " Alternate action: " + altObjectState );
        switch (AltAction)
        {
            case DefaultAction.ResetState:
                InteractableObjectType = defaultState;
                break;
            case DefaultAction.DestroyObject:
                Destroy( this.gameObject, 1f );
                break;
        }
    }

    public void SetAlternateState(bool altState)
    {
        if (altState)
        {
            InteractableObjectType = altObjectState;
        }
    }

}
