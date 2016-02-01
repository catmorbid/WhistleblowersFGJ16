using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class GameClock : MonoBehaviour
{
    public enum DayState
    {
        NewDay,
        Morning,
        WorkReminder,
        WorkStart,
        LunchStart,
        LunchEnd,
        WorkEnd,
        SleepBegin
    }
    private DayState m_state = DayState.NewDay;
    private static GameClock m_singleton;

    private GameObject m_minutesObject;
    private GameObject m_hoursObject;
    private float m_hourOffset = -90;
    private bool m_override = true;

    internal static void MaxTimeOverride()
    {
        m_singleton.m_override = true;
        BaseTimeMultiplier = MaximumTimeMultiplier;
    }

    public static float BaseTimeMultiplier = 48f;
    public static float MinimumTimeMultiplier = 60f;
    public static float MaximumTimeMultiplier = 240f;
    private float m_time = 0f;
    private float m_updateFrequency = 10f;

    public float DayBegin = 6;

    internal static void EndDay()
    {
        Debug.Log( "Day is will end" );
        Application.LoadLevel( "victory" );
        //Fade Out effect, advance time to morning, begin day anew.
    }

    public float WorkReminder = 7;
    public float WorkStarts = 8;
    public float LunchStarts = 11;
    public float LunchEnds = 12;
    public float WorkEnds = 18;
    public float SleepBegin = 20;

    private float Days
    {
        get
        {
            return m_time / ( 24 * 3600 );
        }
        set
        {
            m_time = value * 3600 * 24;
        }
    }

    private float Hours
    {
        get
        {
            return (m_time / 3600f) % 24;
        }
        set
        {
            m_time = Days + value * 3600;
        }
    }

    private float Minutes
    {
        get
        {
            return m_time / 60f;
        }
        set
        {
            m_time = Hours + value * 60f;
        }
    }

    public static DayState State {
        get
        {
            return m_singleton.m_state;
        }
    }

    void Awake()
    {
        m_singleton = this;

    }

    // Use this for initialization

    void Start()
    {
        m_hoursObject = GameObject.Find( "TUNTIVIISARI" );
        m_minutesObject = GameObject.Find( "MINUUTTIVIISARI" );
        Hours = 6;
    }

    void Update()
    {
        if (PlayerControls.PrimaryActionUp)
        {
            m_override = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //if (m_time+Time.time >= m_time + m_updateFrequency)
        m_time += Time.deltaTime * BaseTimeMultiplier;
        //Debug.Log( m_time );

        float hourAngle = (Hours * 30 + m_hourOffset) % 360;
        float minuteAngle = (Minutes *6) % 360;

        //m_hoursObject.transform.Rotate( 0f, hourAngle, 0f );
        //m_minutesObject.transform.Rotate( 0f, minuteAngle, 0f );
        m_minutesObject.transform.localEulerAngles = new Vector3( 0f, minuteAngle, 0f );
        m_hoursObject.transform.localEulerAngles = new Vector3( 0f, hourAngle, 0f );
        //m_minutesObject.transform.Rotate()
        //Debug.Log( string.Format( "Time is: {0:C2}:{1:C3}:{2:C0}", Hours, Minutes, m_time % 60f ) );
        //Debug.Log( "Hour angle " + hourAngle );
        //Debug.Log( "Minute angle " + minuteAngle );

        CheckTimeEvents();
        SetTimeMultiplier();

    }
    private void SetTimeMultiplier()
    {
        if ( !m_override )
        {
            float stress = PlayerScore.GetStressPercentage();
            BaseTimeMultiplier = ( MaximumTimeMultiplier - MinimumTimeMultiplier ) * stress + MinimumTimeMultiplier;
        }
    }

    private void CheckTimeEvents()
    {
        if (Hours >= SleepBegin && m_state == DayState.WorkEnd)
        {
            m_state = DayState.SleepBegin;
            PlayerText.ShowSpeechBubble( "Night is nigh, perhaps I should go to sleep soon...", 5f );
            TaskManager.AddTask( TaskFactory.SleepTasks );
        }
        else if (Hours >= WorkEnds && m_state == DayState.LunchEnd)
        {
            m_state = DayState.WorkEnd;
            PlayerText.ShowSpeechBubble( "Oh, it seems the day is over. I hope I got everything done, since now I've got some free time at last!", 5f );
            TaskManager.AddTask( TaskFactory.FreeTimeTasks );
        }
        else if ( Hours >= LunchEnds && m_state == DayState.LunchStart)
        {
            m_state = DayState.LunchEnd;
            PlayerText.ShowSpeechBubble( "Looks like lunch is over. I better get back to work.", 5f );
            //TaskManager.AddTask( TaskFactory.RandomWorkTask );
        }
        else if ( Hours >= LunchStarts && m_state == DayState.WorkStart)
        {
            m_state = DayState.LunchStart;
            PlayerText.ShowSpeechBubble( "Alas, how time runs by; It's lunch time already!", 5f );
            TaskManager.AddTask( TaskFactory.LunchTask );
        }
        else if ( Hours >= WorkStarts && m_state == DayState.WorkReminder )
        {
            m_state = DayState.WorkStart;
            PlayerText.ShowSpeechBubble( "I should start working now.", 5f );
            TaskManager.AddTask( TaskFactory.BasicWorkTask );
            //TaskManager.AddTask( TaskFactory.RandomWorkTask );
        }
        else if ( Hours >= WorkReminder && m_state == DayState.Morning )
        {
            m_state = DayState.WorkReminder;
            PlayerText.ShowSpeechBubble( "I need to be at work very soon. I better get ready.", 5f );            
        }
        else if ( Hours >= DayBegin && m_state == DayState.NewDay)
        {
            m_state = DayState.Morning;
            PlayerText.ShowSpeechBubble( "*Yawn* Ah, another day of fine work ahead! I better begin with my morning chores immediately!", 5f );
            TaskManager.AddTask( TaskFactory.MorningChores );
        }

    }

    public static float GetTime()
    {
        return m_singleton.m_time;
    }
}
