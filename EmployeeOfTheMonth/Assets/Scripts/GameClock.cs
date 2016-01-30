using UnityEngine;
using System.Collections;

public class GameClock : MonoBehaviour {

    private static GameClock m_singleton;

    private GameObject m_minutesObject;
    private GameObject m_hoursObject;
    private float m_hourOffset = -90;
    public float BaseTimeMultiplier = 1f;
    private float m_time;

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
        m_time = Time.time;

        float hourAngle = (Hours) % 12 * 30  + m_hourOffset;
        float minuteAngle = (Minutes % 60) * 6;

        m_hoursObject.transform.Rotate( 0f, hourAngle, 0f );
        m_minutesObject.transform.Rotate( 0f, minuteAngle, 0f);
        Debug.Log( string.Format( "Time is: {0}:{1}:{2}", Hours, Minutes, m_time % 60f) );
	}
    public static float GetTime()
    {
        return m_singleton.m_time;
    }
}
