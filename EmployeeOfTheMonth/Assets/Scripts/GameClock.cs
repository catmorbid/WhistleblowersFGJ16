using UnityEngine;
using System.Collections;

public class GameClock : MonoBehaviour
{

    private static GameClock m_singleton;

    private GameObject m_minutesObject;
    private GameObject m_hoursObject;
    private float m_hourOffset = -90;
    public static float BaseTimeMultiplier = 240;
    private float m_time = 0f;
    private float m_updateFrequency = 10f;

    public float WakeTimeHours = 6;
    public float WorkReminder = 7;
    public float WorkStarts = 8;
    public float LunchStarts = 11;
    public float LunchEnds = 12;
    public float WorkEnds = 17;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (m_time+Time.time >= m_time + m_updateFrequency)
        m_time += Time.deltaTime * BaseTimeMultiplier;
        Debug.Log( m_time );

        float hourAngle = (Hours * 30 + m_hourOffset) % 360;
        float minuteAngle = (Minutes *6) % 360;

        //m_hoursObject.transform.Rotate( 0f, hourAngle, 0f );
        //m_minutesObject.transform.Rotate( 0f, minuteAngle, 0f );
        m_minutesObject.transform.localEulerAngles = new Vector3( 0f, minuteAngle, 0f );
        m_hoursObject.transform.localEulerAngles = new Vector3( 0f, hourAngle, 0f );
        //m_minutesObject.transform.Rotate()
        Debug.Log( string.Format( "Time is: {0:C2}:{1:C3}:{2:C0}", Hours, Minutes, m_time % 60f ) );
        Debug.Log( "Hour angle " + hourAngle );
        Debug.Log( "Minute angle " + minuteAngle );

    }
    public static float GetTime()
    {
        return m_singleton.m_time;
    }
}
