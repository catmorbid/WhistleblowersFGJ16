using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;
using UnityEngine.UI;

public class PlayerControls
{
    private static string m_btnPrimary = "Fire1";
    private static string m_btnSecondary = "Fire2";
    private static bool m_cursorVisible = true;
    private static Camera m_camera;
    private static float m_longPressThreshold = 0.25f;
    private static float m_primaryPressStart;
    private static float m_secondaryPressStart;

    public static Sprite GrabCursorSprite
    {
        get
        {
            return Resources.Load<Sprite>( "sprites/grab_icon" );
        }
    }
    public static Sprite InteractionCursorSprite
    {
        get
        {
            return Resources.Load<Sprite>( "sprites/interact_icon2" );
        }
    }
    public static Vector2 InteractionCursorHotspot = Vector2.zero;

    public PlayerControls()
    {

    }



    //internal static void SetInteractionCursor()
    //{
    //    Texture2D tex = InteractionCursorTexture;
    //    if (tex != null)
    //        Cursor.SetCursor(InteractionCursorTexture, InteractionCursorHotspot, CursorMode.Auto);
    //}

    public static Sprite CursorSprite
    {
        get
        {
            return GameObject.Find( "Cursor" ).GetComponent<UnityEngine.UI.Image>().sprite;
        }
        set
        {
            GameObject.Find( "Cursor" ).GetComponent<UnityEngine.UI.Image>().sprite = value;
        }
    }

    public static void ShowCursor( bool show )
    {
        if ( show != m_cursorVisible )
        {
            GameObject.Find( "Cursor" ).GetComponent<UnityEngine.UI.Image>().enabled = show;
            m_cursorVisible = show;
        }
    }
    private static bool m_locked;
    public static bool IsCursorLocked
    {
        get
        {
            return m_locked;
        }
        set
        {
            LockCursor( value );
        }
    }
    public static void LockCursor( bool locked )
    {
        m_locked = locked;
        if ( locked )
        {
            Debug.Log( "Cursor Locked" );
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Debug.Log( "Cursor Unlocked" );
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private static float deltaPrimaryPressed
    {
        get
        {
            return Time.realtimeSinceStartup - m_primaryPressStart;
        }
    }

    private static float deltaSecondaryPressed
    {
        get
        {
            return Time.realtimeSinceStartup - m_secondaryPressStart;
        }
    }

    public static bool OnPrimaryActionTap
    {
        get
        {
            if ( !PrimaryActionDown && PrimaryActionUp )
            {
                if ( deltaPrimaryPressed < m_longPressThreshold )
                    return true;
            }
            return false;
        }
    }

    public static bool OnPrimaryActionLongPress
    {
        get
        {
            if ( !PrimaryActionDown && CrossPlatformInputManager.GetButton( m_btnPrimary ) )
            {
                return ( deltaPrimaryPressed >= m_longPressThreshold );
            }
            return false;
        }
    }

    public static bool PrimaryActionDown
    {
        get
        {
            if ( CrossPlatformInputManager.GetButtonDown( m_btnPrimary ) )
            {
                Debug.Log( "Primary Action Down" );
                m_primaryPressStart = Time.realtimeSinceStartup;
                return true;

            }
            return false;
        }
        set
        {
            if ( value )
                CrossPlatformInputManager.SetButtonDown( m_btnPrimary );
            else
                CrossPlatformInputManager.SetButtonUp( m_btnPrimary );
        }
    }
    public static bool PrimaryActionUp
    {
        get
        {
            if ( CrossPlatformInputManager.GetButtonUp( m_btnPrimary ) )
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
            if ( CrossPlatformInputManager.GetButtonDown( m_btnSecondary ) )
            {
                Debug.Log( "Secondary Action Down" );
                m_secondaryPressStart = Time.realtimeSinceStartup;
                return true;

            }
            return false;
        }
        set
        {
            if ( value )
                CrossPlatformInputManager.SetButtonDown( m_btnSecondary );
            else
                CrossPlatformInputManager.SetButtonUp( m_btnSecondary );
        }
    }

    public static bool SecondaryActionTap
    {
        get
        {
            if ( !SecondaryActionDown && SecondaryActionUp )
            {
                if ( deltaSecondaryPressed < m_longPressThreshold )
                    return true;
            }
            return false;
        }
    }
    public static bool SecondaryActionLongPress
    {
        get
        {
            if ( !SecondaryActionDown && CrossPlatformInputManager.GetButton( m_btnSecondary ) )
            {
                return ( deltaSecondaryPressed >= m_longPressThreshold );
            }
            return false;
        }

    }
    public static bool SecondaryActionUp
    {
        get
        {
            if ( CrossPlatformInputManager.GetButtonUp( m_btnSecondary ) )
            {
                return true;
            }
            return false;
        }
    }
}
