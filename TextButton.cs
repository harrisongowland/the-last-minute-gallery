using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextButton : TextObject, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private UnityEvent m_UnityEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Text.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Text.color = Color.white; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.PlaySFX(SystemLibrary.instance.m_ButtonClickEffect);
        m_UnityEvent.Invoke(); 
    }

    private void OnDisable()
    {
        m_Text.color = Color.white; 
    }
}
