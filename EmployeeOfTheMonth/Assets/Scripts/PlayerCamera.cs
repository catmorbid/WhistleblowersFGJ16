using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour {

    private static PlayerCamera m_singleton;

    public static Camera ActiveCamera
    {
        get
        {
            return m_singleton.gameObject.GetComponent<Camera>();
        }
    }
        
	// Use this for initialization
	void Awake () {
        m_singleton = this;
	}

    void Start()
    {
        PlayerControls.ShowCursor(false);
        PlayerControls.LockCursor(true);
    }

}
