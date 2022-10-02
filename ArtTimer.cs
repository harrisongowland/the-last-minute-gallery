using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtTimer : MonoBehaviour
{
    public static ArtTimer instance;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);
        instance = this; 
    }
    
    [SerializeField] private float m_StartTime; 
    [SerializeField] private float m_CurrentTime;
    [SerializeField] private float m_ResetInterval = 10; 
    
    [SerializeField] private PaintCanvas m_Canvas; 
    private bool m_CountingDown = false;

    public delegate void TimeRanOut();
    public event TimeRanOut OnTimeRanOut; 

    public void StartCountingDown()
    {
        Debug.Log("Starting countdown");
        m_StartTime = Time.time; 
        m_CountingDown = true; 
    }
    
    public void Update()
    {
        if (m_CountingDown)
        {
            m_CurrentTime = Time.time;
            if (m_CurrentTime - m_StartTime > m_ResetInterval)
            {
                Debug.Log("Invoking time ran out");
                OnTimeRanOut?.Invoke();
                Reset(); 
            }
        }
    }

    public void Reset()
    {
        m_StartTime = Time.time;
        m_CountingDown = false; 
    }
}
