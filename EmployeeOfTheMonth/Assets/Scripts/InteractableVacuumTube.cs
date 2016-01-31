using UnityEngine;
using System.Collections;

public class InteractableVacuumTube : Interactable {

    private bool m_hasMail;
    private GameObject m_mailLight;

    protected override void Awake()
    {
        base.Awake();
        m_mailLight = GetComponentInChildren<Light>().gameObject;
    }
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Random.Range(0,100)<=1)
        {
            m_hasMail = true;

        }

        if (m_hasMail)
        {
            m_mailLight.SetActive( true );            
        }
        else
        {
            m_mailLight.SetActive( false );
        }
    }
    public override void Interact( Transform interactorTransform )
    {
        base.Interact( interactorTransform );
        if (m_hasMail)
        {
            m_hasMail = false;
            TaskManager.AddTask( TaskFactory.RandomWorkTask );

        }
    }
}
