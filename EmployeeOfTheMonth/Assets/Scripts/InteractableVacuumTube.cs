using UnityEngine;
using System.Collections;
using System;

public class InteractableVacuumTube : Interactable {

    private bool m_hasMail;
    private GameObject m_mailLight;
    public AudioClip NotificationClip;
    public float NotificationVolume =0.6f;

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
        if ( (int) GameClock.State >= (int) GameClock.DayState.WorkStart && (int) GameClock.State <= (int) GameClock.DayState.WorkEnd )
        {
            if ( UnityEngine.Random.Range( 0, 500 ) == 0 )
            {
                m_hasMail = true;
                TaskManager.AddTask( TaskFactory.TubeTask );
                if (NotificationClip != null)
                {
                    m_src.PlayOneShot( NotificationClip, NotificationVolume );
                }
            }

            if ( m_hasMail )
            {
                m_mailLight.SetActive( true );
            }
            else
            {
                m_mailLight.SetActive( false );
            }
        }
    }
    public override void Interact( Transform interactorTransform )
    {
        base.Interact( interactorTransform );
        if (m_hasMail)
        {
            m_hasMail = false;
            Task task = TaskFactory.RandomWorkTask;
            if ( task.hasObjective( Goals.Objects.Document ) )
            {
                GetSpawner().Spawn();
            }
            TaskManager.AddTask( task );

        }
    }

    public Spawner GetSpawner()
    {
        return GetComponent<Spawner>();
    }
   
}
