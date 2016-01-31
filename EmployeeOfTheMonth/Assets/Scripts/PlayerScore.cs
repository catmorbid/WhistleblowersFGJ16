using System;
using UnityEngine;
using System.Collections.Generic;

class PlayerScore
{
    private static PlayerScore m_singleton = new PlayerScore();

    private float m_stress = 0;
    private float m_maxStress = 1000;

    public float Stress
    {
        get
        {
            return Mathf.Max(Mathf.Min(m_stress,m_maxStress),0f);
        }
        set
        {
            m_stress = Mathf.Max(Mathf.Min(value,m_maxStress),0f);
        }
    }

    public static void ModifyStress(float value)
    {
        m_singleton.m_stress += value;
        Debug.Log( "Stress Level is now " + m_singleton.Stress );
    }
    public static float GetStress()
    {
        return Mathf.Max( m_singleton.Stress );
    }

    public static float GetStressPercentage()
    {
        return m_singleton.Stress / m_singleton.m_maxStress;
    }

}
