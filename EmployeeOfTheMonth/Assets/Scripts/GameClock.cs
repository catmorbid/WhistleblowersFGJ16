using UnityEngine;
using System.Collections;

public class GameClock : MonoBehaviour {

    private static GameClock m_singleton;

    private GameObject m_minutesObject;
    private GameObject m_hoursObject;
    private float m_hourOffset = -90;
    public float BaseTimeMultiplier = 1f;
    private float m_time = 0f;
    private float m_updateFrequency = 10f;

    private float Hours
    {
        get
        {
            return m_time / 3600f;
        }
    }

    private float Minutes
    {
        get
        {
            return m_time / 60f;
        }
    }

    void Awake()
    {
        m_singleton = this;
        
    }

    // Use this for initialization

    void Start () {
        m_hoursObject = GameObject.Find( "TUNTIVIISARI" );
        m_minutesObject = GameObject.Find( "MINUUTTIVIISARI" );
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //if (m_time+Time.time >= m_time + m_updateFrequency)
        if (true)
        {
            m_time = Time.time;

            int hourAngle = (int)((Hours) + m_hourOffset) / 360;
            int minuteAngle = (int)Minutes;

            //m_hoursObject.transform.Rotate( 0f, hourAngle, 0f );

            //m_minutesObject.transform.Rotate( 0f, minuteAngle, 0f );
            //m_minutesObject.transform.Rotate()
            Debug.Log( string.Format( "Time is: {0:C2}:{1:C3}:{2:C0}", Hours, Minutes, m_time % 60f ) );
            Debug.Log( "Hour angle " + hourAngle );
            Debug.Log( "Minute angle " + minuteAngle );
        }
        
	}
    public static float GetTime()
    {
        return m_singleton.m_time;
    }
}
