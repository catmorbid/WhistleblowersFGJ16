using UnityEngine;
using System.Collections;

public class TaskTrigger : MonoBehaviour {

    public AudioClip audioClip;
    public UnityEngine.Audio.AudioMixerGroup MixerGroup;
    public float audioSpatialBlend = 1.0f;
    private AudioSource m_src;
    public Goals.Triggers TriggerType;
    public event TaskTriggeredAction TaskTriggerEvent;
    public delegate void TaskTriggeredAction( TaskTrigger trigger, Interactable triggerObj );

    public AudioSource AudioSrc {
        get
        {
            return m_src;
        }
    }
    protected virtual void Start()
    {
        m_src = gameObject.AddComponent<AudioSource>();
        m_src.clip = audioClip;
        m_src.spatialBlend = audioSpatialBlend;
        m_src.playOnAwake = false;
        m_src.outputAudioMixerGroup = MixerGroup;
        m_src.loop = false;
        TaskManager.RegisterTrigger( this );
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        Interactable obj = collider.GetComponentInChildren<Interactable>();
        if (TaskTriggerEvent != null && obj != null)
        {
            Debug.Log( "Task Trigger Event launcher: " + obj.name + " triggered on " + gameObject.name );
            TaskTriggerEvent( this, obj );
        }
        //if ( m_src != null )
        //{
        //    m_src.Play();
        //}
    }
}
