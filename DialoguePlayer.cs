using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    public static DialoguePlayer instance;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        m_AlphaControl = GetComponent<CanvasGroup>();
    }

    [SerializeField] private TextMeshProUGUI m_SpeakerText;
    [SerializeField] private TextMeshProUGUI m_DialogueText;

    private CanvasGroup m_AlphaControl; 
    
    public void ShowDialogue(string speaker, string dialogue)
    {
        m_SpeakerText.text = speaker;
        m_DialogueText.text = dialogue;
    }

    public void SetShowingDialogue(bool showing)
    {
        m_AlphaControl.alpha = showing ? 1 : 0;
    }
}
