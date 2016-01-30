using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Camera))]
public class InteractionDetector : MonoBehaviour {

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
            m_interactableCollider = getInteractableCollider();
            return (m_interactableCollider != null);
        }
    }

    private Collider m_interactableCollider;

    public Interactable Interactable
    {
        get
        {
            Interactable n = m_interactableCollider.GetComponent<Interactable>();
            return n;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (InteractableInRange)
        {
            if (PlayerControls.PrimaryActionDown)
            {
                Debug.Log("Interactable in range");
                Interactable.Interact(transform);
            }
            
        }
	
	}

    private Collider getInteractableCollider()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, MyCamera.transform.forward * range, out hit, InteractableLayer )) {
            return hit.collider;
        }
        return null;
    }
}
