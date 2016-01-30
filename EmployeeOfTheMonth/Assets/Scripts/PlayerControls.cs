using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class PlayerControls {
    private static string m_btnPrimary = "Fire1";
    private static string m_btnSecondary = "Fire2";
    private static bool m_cursorVisible = true;
    private static Camera m_camera;
    private static float m_longPressThreshold = 0.5f;
    private static float m_primaryPressStart;
    private static float m_secondaryPressStart;
    public static Texture2D InteractionCursorTexture
    {
        get
        {
            return (Texture2D) Resources.Load("sprites/interact_icon");
        }
    }
    public static Vector2 InteractionCursorHotspot = Vector2.zero;

    public PlayerControls()
    {

    }

    public static bool PrimaryActionDown
    {
        get
        {
            if (CrossPlatformInputManager.GetButtonDown(m_btnPrimary))
            {
                Debug.Log("Primary Action Down");
                m_primaryPressStart = Time.realtimeSinceStartup;
                return true;
                
            }
            return false;
        }
        set
        {
            if (value)
                CrossPlatformInputManager.SetButtonDown(m_btnPrimary);
            else
                CrossPlatformInputManager.SetButtonUp(m_btnPrimary);
        }
    }

    //internal static void SetInteractionCursor()
    //{
    //    Texture2D tex = InteractionCursorTexture;
    //    if (tex != null)
    //        Cursor.SetCursor(InteractionCursorTexture, InteractionCursorHotspot, CursorMode.Auto);
    //}

    public static void ShowCursor(bool show)
    {
        if (show != m_cursorVisible)
        {
            GameObject.Find("Cursor").GetComponent<UnityEngine.UI.Image>().enabled = show;
            m_cursorVisible = show;
        }
    }
    public static void LockCursor(bool locked)
    {
        if (locked)
            Cursor.lockState = CursorLockMode.Locked;
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private static float deltaPrimaryPressed
    {
        get
        {
            return Time.realtimeSinceStartup - m_primaryPressStart;
        }
    }

    public static bool OnPrimaryActionTap
    {
        get
        {
            if (!PrimaryActionDown && PrimaryActionUp)
            {
                if (deltaPrimaryPressed < m_longPressThreshold)
                    return true;
            }
            return false;
        }
    }
    
    public static bool OnPrimaryActionLongPress
    {
        get
        {
            if (!PrimaryActionDown && CrossPlatformInputManager.GetButton(m_btnPrimary))
            {
                return (deltaPrimaryPressed >= m_longPressThreshold);
            }
            return false;
        }
    }

    public static bool PrimaryActionUp
    {
        get
        {
            if (CrossPlatformInputManager.GetButtonUp(m_btnPrimary))
            {
                //Debug.Log("Primary Action Up");
                return true;

            }
            return false;
        }
    }

    public static bool SecondaryActionDown
    {
        get
        {
            if (CrossPlatformInputManager.GetButtonDown(m_btnSecondary))
            {
                Debug.Log("Secondary Action Down");
                return true;
                
            }
            return false;
        }
        set
        {
            if (value)
                CrossPlatformInputManager.SetButtonDown(m_btnSecondary);
            else
                CrossPlatformInputManager.SetButtonUp(m_btnSecondary);
        }
    }

    public static bool SecondaryActionTap { get; internal set; }
    public static bool SecondaryActionLongPress { get; internal set; }
    public static bool SecondaryActionUp { get; internal set; }
}
