using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemLibrary : MonoBehaviour
{
    public static SystemLibrary instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        
        Destroy(this.gameObject);
    }

    public TMP_FontAsset[] m_DyslexiaFonts;
    public AudioClip m_ButtonClickEffect;

    private bool m_UsingDyslexiaFont;

    public delegate void SystemUpdate();
    public event SystemUpdate OnToggleDyslexiaFont; 
    
    public bool UsingDyslexiaFont
    {
        get
        {
            return m_UsingDyslexiaFont;
        }
        set
        {
            m_UsingDyslexiaFont = value;
            Debug.Log("Toggled dyslexia font");
            OnToggleDyslexiaFont?.Invoke(); 
        }
    }

    public void ToggleDyslexiaFont()
    {
        UsingDyslexiaFont = !UsingDyslexiaFont;
    }
}
