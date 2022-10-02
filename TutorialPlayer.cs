using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{

    public static TutorialPlayer instance;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this; 
        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField] private bool m_NextSlide;

    public bool NextSlide
    {
        get
        {
            return m_NextSlide;
        }
        set
        {
            m_NextSlide = value; 
        }
    }

    [SerializeField] private TextMeshProUGUI m_TutorialText;

    public void GoToNextSlide()
    {
        m_NextSlide = true; 
    }

    public void SetTutorialDisplaying(bool display)
    {
        GetComponent<CanvasGroup>().alpha = display ? 1 : 0; 
    }
    
    public void ReadTutorial(string tutorial)
    {
        m_TutorialText.text = tutorial; 
    }
}
