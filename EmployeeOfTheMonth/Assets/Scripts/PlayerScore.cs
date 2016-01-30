using System;
using UnityEngine;
using System.Collections.Generic;

class PlayerScore
{
    private static PlayerScore m_singleton = new PlayerScore();

    private float m_stress = 0;

    public float Stress
    {
        get
        {
            return m_stress;
        }
        set
        {
            m_stress = value;
        }
    }

    public static void ModifyStress(float value)
    {
        m_singleton.m_stress += value;
        Debug.Log( "Stress Level is now " + m_singleton.Stress );
    }

}
