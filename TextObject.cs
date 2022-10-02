using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextObject : SystemListener
{
    internal TextMeshProUGUI m_Text;
    internal TextMeshPro m_TextReal;
    internal bool m_UsingGUIText = true;

    [SerializeField] private bool m_Bold;
    [SerializeField] private TMP_FontAsset m_DefaultFont;

    [SerializeField] private float m_DefaultTextSize;
    [SerializeField] private float m_OpenDyslexicTextSize;

    private void Awake()
    {
        if (GetComponent<TextMeshProUGUI>() != null)
        {
            m_Text = GetComponent<TextMeshProUGUI>();
            return;
        }

        m_TextReal = GetComponent<TextMeshPro>();
        m_UsingGUIText = false;
    }

    internal override void OnToggledDyslexiaFont()
    {
        base.OnToggledDyslexiaFont();
        Debug.Log("Toggling...");
        m_UsingDyslexiaFont = SystemLibrary.instance.UsingDyslexiaFont;
        TMP_FontAsset assetToUse = SystemLibrary.instance.m_DyslexiaFonts[m_Bold ? 0 : 1];
        if (m_UsingDyslexiaFont)
        {
            if (m_UsingGUIText)
            {
                m_Text.font = assetToUse;
                if (m_OpenDyslexicTextSize != 0)
                {
                    m_Text.fontSize = m_OpenDyslexicTextSize;
                }

                return;
            }

            m_TextReal.font = assetToUse;
            if (m_OpenDyslexicTextSize != 0)
            {
                m_TextReal.fontSize = m_OpenDyslexicTextSize;
            }

            return;
        }

        if (m_UsingGUIText)
        {
            m_Text.font = m_DefaultFont;
            if (m_DefaultTextSize != 0)
            {
                m_Text.fontSize = m_DefaultTextSize;
            }
        }
        else
        {
            m_TextReal.font = m_DefaultFont;
            if (m_DefaultTextSize != 0)
            {
                m_TextReal.fontSize = m_DefaultTextSize;
            }
        }
    }
}