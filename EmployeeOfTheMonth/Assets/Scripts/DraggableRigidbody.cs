using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class DraggableRigidbody : Interactable {

    public Rigidbody MyRigidbody
    {
        get
        {
            return GetComponent<Rigidbody>();
        }
    }

    const float k_Spring = 200f;
    const float k_Damper = 1.0f;
    const float k_Drag = 10.0f;
    const float k_AngularDrag = 5.0f;
    const float k_Distance = 0.1f;
    const bool k_AttachToCenterOfMass = true;

    private float m_grabDistance;

    private SpringJoint m_SpringJoint;

    public override void Interact(Transform otherTransform)
    {
        Debug.Log(gameObject.name + " interacting");
        if (!m_SpringJoint)
        {
            GameObject go = new GameObject("Rigidbody dragger");
            Rigidbody body = go.AddComponent<Rigidbody>();
            m_SpringJoint = go.AddComponent<SpringJoint>();
            body.isKinematic = true;
            m_SpringJoint.spring = k_Spring;
            m_SpringJoint.damper = k_Damper;
            m_SpringJoint.maxDistance = k_Distance;
        }

        m_SpringJoint.transform.position = transform.position;
        m_SpringJoint.anchor = Vector3.zero;
        
        m_SpringJoint.connectedBody = MyRigidbody;

        m_grabDistance = Vector3.Magnitude(otherTransform.position - gameObject.transform.position);
        
        StartCoroutine("dragObject", m_grabDistance);
        //return true;
    }

    private IEnumerator dragObject(float distance)        
    {
        Debug.Log("drag object coroutine started, distance "+distance);
        float oldDrag = m_SpringJoint.connectedBody.drag;
        float oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
        m_SpringJoint.connectedBody.drag = k_Drag;
        m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;
        Camera mainCamera = PlayerCamera.ActiveCamera;
        while (!PlayerControls.PrimaryActionUp)
        {
            Debug.Log("drag");
            Transform cam = PlayerCamera.ActiveCamera.transform;
            m_SpringJoint.transform.position = cam.position + cam.forward * distance + cam.up*0.2f;
            yield return null;
        }
        if (m_SpringJoint.connectedBody)
        {
            m_SpringJoint.connectedBody.drag = oldDrag;
            m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
            m_SpringJoint.connectedBody = null;
        }
    }
}
