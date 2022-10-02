using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer m_Mixer;
    [SerializeField] private string m_Parameter;
    [SerializeField] private Slider m_Slider;

    public void SetLevel()
    {
        m_Mixer.SetFloat(m_Parameter, Mathf.Log10(m_Slider.value) * 20);
    }
}