using UnityEngine;
using System.Collections;
using System;

public class Interactable : MonoBehaviour {

    public AudioClip InteractionAudio;
    public UnityEngine.Audio.AudioMixerGroup MixerGroup;
    public float audioSpatialBlend = 1.0f;
    public float volume = 0.8f;
    public bool LoopAudio = false;
    protected UnityEngine.AudioSource m_src;
    public enum DefaultAction
    {
        None,
        ResetState,
        DestroyObject
    }
    public DefaultAction BasicActionType = DefaultAction.None;
    public event ObjectInteractedAction ObjectInteractionEvent;
    public delegate void ObjectInteractedAction( Interactable obj );
    public Goals.Objects InteractableObjectType;
    public string m_description;
    public string interactionMessage;
    private bool m_interaction;
    public string Description
    {
        get
        {
            return m_description;
        }
        set
        {
            m_description = value;
        }
    }
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

    protected virtual void Awake()
    {
        if (m_description == "")
        {
            m_description = "Placeholder Description for Object " + gameObject.name;
        }
            
    }

    protected virtual void Start()
    {
        m_src = gameObject.AddComponent<AudioSource>();
        m_src.outputAudioMixerGroup = MixerGroup;
        m_src.spatialBlend = audioSpatialBlend;
        m_src.clip = InteractionAudio;
        m_src.volume = volume;
        TaskManager.RegisterInteraction( this );
    }
    public virtual void Interact(Transform interactorTransform)
    {
        Debug.Log( "Interaction" );
        if (ObjectInteractionEvent != null)
        {
            Debug.Log( "Firing interaction event" );
            ObjectInteractionEvent( this );
        }
        if (interactionMessage != null)
        {
            PlayerText.ShowSpeechBubble( interactionMessage, interactionMessage.Length / 60f );
        }
        TakeAction();
    }

    public  virtual void TakeAction()
    {
        if (InteractionAudio != null)
        {
            if ( !LoopAudio )
            {
                m_src.PlayOneShot( InteractionAudio );
            }
            else
            {
                if ( m_src.isPlaying )
                {
                    m_src.Stop();
                }
                else
                {
                    m_src.loop = true;
                    m_src.Play();
                }
            }
            
        }
        switch (BasicActionType)
        {
            case DefaultAction.DestroyObject:
                Destroy( this.gameObject, 1f );
                break;
        }
    }

    public virtual void InteractLongPress(Transform interactorTransform)
    {
    }
    public virtual void AltInteract(Transform interactorTransform)
    {
        PlayerText.ShowSpeechBubble( m_description, 5f );
        //Debug.Log("Secondary Interaction not implemented!");
    }
    public virtual void AltInteractLongPress(Transform interactorTransform)
    {
    }

}
