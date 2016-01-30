using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Camera))]
public class InteractionDetector : MonoBehaviour
{

    public Camera MyCamera
    {
        get
        {
            return GetComponent<Camera>();
        }
    }
    public LayerMask InteractableLayer;
    public float range = 10f;
    public bool InteractableInRange
    {
        get
        {
            return (m_interactableCollider != null);
        }
    }

    private Collider m_interactableCollider;
    private bool AllowPrimaryInteraction;
    private bool AllowSecondaryInteraction;

    public Interactable Interactable
    {
        get
        {
            return m_interactableCollider.GetComponent<Interactable>();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (PlayerControls.PrimaryActionUp)
        {
            AllowPrimaryInteraction = true;
        }
        if (PlayerControls.SecondaryActionUp)
        {
            AllowSecondaryInteraction = true;
        }

        if (InteractableInRange )
        {
            PlayerControls.ShowCursor(true);
            if (PlayerControls.OnPrimaryActionLongPress && AllowPrimaryInteraction)
            {
                Debug.Log("Primary Interaction long press");
                Interactable.InteractLongPress(transform);
                AllowPrimaryInteraction = false;
            }
            else if (PlayerControls.OnPrimaryActionTap && AllowPrimaryInteraction)
            {
                Debug.Log("Primary Interaction");
                Interactable.Interact(transform);
            }
            if (PlayerControls.SecondaryActionLongPress && AllowSecondaryInteraction)
            {
                AllowSecondaryInteraction = false;
            }
            else if (PlayerControls.SecondaryActionTap && AllowSecondaryInteraction)
            {
                Debug.Log("Secondary Interaction");
                Interactable.AltInteract(transform);
            }
        }
        else
        {
            PlayerControls.ShowCursor(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_interactableCollider = getInteractableCollider();
    }

    private Collider getInteractableCollider()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, MyCamera.transform.forward * range, out hit, InteractableLayer))
        {
            return hit.collider;
        }
        return null;
    }
}
