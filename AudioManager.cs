using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource m_Music; 
    [SerializeField] private AudioSource m_SFX; 

    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            return; 
        }
        
        Destroy(this.gameObject);
    }

    public void PlayMusic(AudioClip music)
    {
        if (music == null)
        {
            m_Music.Stop();
            return; 
        }
        
        m_Music.clip = music;
        m_Music.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        m_SFX.clip = sfx;
        m_SFX.Play();
    }
}
