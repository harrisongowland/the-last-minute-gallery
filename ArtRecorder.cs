using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArtRecorder : MonoBehaviour
{

    public static ArtRecorder instance;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        m_DonePrompts = new List<string>(); 
    }

    [SerializeField] private MeshRenderer[] m_Paintings; 
    [SerializeField] private string[] m_Prompts;
    private List<string> m_DonePrompts; 
    private string m_CurrentPrompt;

    [System.Serializable]
    public class CompletedArt
    {
        public string prompt;
        public Texture2D art;

        public CompletedArt(string _prompt, Texture2D _art)
        {
            prompt = _prompt;
            art = _art; 
        }
    }

    public List<CompletedArt> playerArt;
    
    public string GeneratePrompt()
    {
        while (true)
        {
            string prompt = m_Prompts[Random.Range(0, m_Prompts.Length - 1)];

            if (m_DonePrompts.Contains(prompt))
            {
                continue;
            }

            m_CurrentPrompt = prompt; 
            m_DonePrompts.Add(prompt);
            return prompt;
            break;
        }
    }

    public void RecordArt(Texture2D art)
    {
        Debug.Log("Recording art");
        CompletedArt completedArt = new CompletedArt(m_CurrentPrompt, art);
        playerArt.Add(completedArt);
    }

    public void ClearDonePromptsList()
    {
        m_DonePrompts.Clear(); 
    }

    public void FramePaintings()
    {
        int index = 0; 
        foreach (var art in playerArt)
        {
            m_Paintings[index].material.mainTexture = art.art;
            index++;
        }
    }
    
    
}
