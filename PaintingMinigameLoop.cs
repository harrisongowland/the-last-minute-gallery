using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaintingMinigameLoop : MonoBehaviour
{
    public static PaintingMinigameLoop instance;

    [SerializeField] private bool m_Playing;
    [SerializeField] private AudioClip m_ClockTick;

    [SerializeField] private CanvasGroup m_AlphaControl; 
    [SerializeField] private CanvasGroup m_GameUI; 
    [SerializeField] private TextMeshProUGUI m_TimeText;
    [SerializeField] private TextMeshProUGUI m_PromptText;
    [SerializeField] private TextMeshProUGUI m_UIPromptText;
    [SerializeField] private Animator m_TimerAnimator;

    [SerializeField] private int m_MaxLoops = 18;
    
    [SerializeField] private EventList m_GlobalEventList; 
    
    private PaintCanvas m_Canvas; 
    private string m_CurrentPrompt;

    private bool m_CompletedLoop = false;
    private int m_CurrentLoop = 0;
    private Coroutine m_Loop;

    public bool Playing
    {
        get { return m_Playing; }
    }

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
        m_Canvas = FindObjectOfType<PaintCanvas>();
    }

    public void Start()
    {
        ArtTimer.instance.OnTimeRanOut += OnTimeRanOut;
    }

    public void OnTimeRanOut()
    {
        Debug.Log("Painting minigame loop: time ran out");
        m_CurrentLoop += 1;
        m_CompletedLoop = true;
    }

    public void Play()
    {
        m_Playing = true;
        m_CurrentLoop = 0;
        m_Loop = StartCoroutine(Loop());
    }

    public IEnumerator Loop()
    {
        while (m_Playing)
        {
            m_CurrentPrompt = ArtRecorder.instance.GeneratePrompt();
            m_UIPromptText.text = m_CurrentPrompt; 
            ShowPromptAndTimer(true);
            
            //Show prompt and timer to start
            for (var i = 3; i > 0; i--)
            {
                AudioManager.instance.PlaySFX(m_ClockTick);
                SetPromptAndTimer(i);
                yield return new WaitForSeconds(1); 
            }
            
            SetPromptAndTimer("Go");
            yield return new WaitForSeconds(1);
            ShowPromptAndTimer(false);
            
            m_Canvas.CanPaint = true;
            m_GameUI.alpha = 1f;
            m_TimerAnimator.SetTrigger("t_Countdown");
            ArtTimer.instance.StartCountingDown();
            Debug.Log("Waiting for m_CompletedLoop");
            yield return new WaitUntil(() => m_CompletedLoop);
            m_GameUI.alpha = 0f;
            
            //Disable canvas, record art, reset canvas
            m_Canvas.CanPaint = false; 
            ArtRecorder.instance.RecordArt(m_Canvas.m_RecordedTex);
            m_Canvas.ResetCanvas();
            m_CompletedLoop = false;

            if (m_CurrentLoop <= m_MaxLoops - 1) continue;
            
            //Otherwise, that was the last one. Break. 
            m_Playing = false;
            EventListProcessor.instance.Proceed = true; 
            break;
        }
    }

    public void ShowPromptAndTimer(bool show)
    {
        m_AlphaControl.alpha = show ? 1 : 0; 
    }

    public void SetPromptAndTimer(int time)
    {
        m_TimeText.text = time.ToString();
        if (m_PromptText.text != m_CurrentPrompt)
        {
            m_PromptText.text = m_CurrentPrompt; 
        }
    }
    
    public void SetPromptAndTimer(string message)
    {
        m_TimeText.text = message;
    }
}