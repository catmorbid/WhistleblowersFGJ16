using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class PlayerControls {
    private static string m_btnPrimary = "Fire1";
    private static string m_btnSecondary = "Fire2";
    private static Camera m_camera;

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

    public static void HideCursor(bool hide)
    {
        Cursor.visible = !hide;
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

    public static bool PrimaryActionUp
    {
        get
        {
            if (CrossPlatformInputManager.GetButtonUp(m_btnPrimary))
            {
                Debug.Log("Primary Action Up");
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
}
