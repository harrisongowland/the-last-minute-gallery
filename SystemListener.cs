using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemListener : MonoBehaviour
{
    internal bool m_UsingDyslexiaFont;

    internal virtual void Start()
    {
        SystemLibrary.instance.OnToggleDyslexiaFont += OnToggledDyslexiaFont;
    }

    private void OnEnable()
    {
        //Catch up with state of game
        OnToggledDyslexiaFont();
    }
    
    internal virtual void OnToggledDyslexiaFont()
    {
        Debug.Log("Toggling 2");
        m_UsingDyslexiaFont = SystemLibrary.instance.UsingDyslexiaFont;
    }
}
