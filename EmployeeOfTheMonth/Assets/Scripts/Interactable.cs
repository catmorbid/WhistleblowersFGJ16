using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {

    public abstract void Interact(Transform interactorTransform);
    public virtual void AltInteract(Transform interactorTransform)
    {
        Debug.Log("Alternate Interaction not implemented!");
    }

}
