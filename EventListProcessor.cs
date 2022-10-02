using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EventListProcessor : MonoBehaviour
{

    public static EventListProcessor instance;

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
    
    [SerializeField] private EventList[] m_EventListList;
    private EventList m_CurrentList;
    private Coroutine m_ExecuteList;

    private bool m_Proceed = false;

    public bool Proceed
    {
        get
        {
            return m_Proceed;
        }
        set
        {
            m_Proceed = value; 
        }
    }
    
    public void ExecuteEventList(int list)
    {
        m_CurrentList = m_EventListList[list];
        m_ExecuteList = StartCoroutine(ExecuteList());
    }

    private IEnumerator ExecuteList()
    {
        foreach (EventList.Event e in m_CurrentList.events)
        {
            //Because for most events, we aren't showing dialogue, let's just switch it off here and then switch it on in the appropriate case
            DialoguePlayer.instance.SetShowingDialogue(false);
            TutorialPlayer.instance.SetTutorialDisplaying(false);
            switch (e.eventType)
            {
                case EventList.Event.EventType.DIALOGUE:
                    DialoguePlayer.instance.SetShowingDialogue(true);
                    DialoguePlayer.instance.ShowDialogue(e.speaker, e.dialogue);
                    break;
                case EventList.Event.EventType.ANIMATION:
                    e.animationTarget.GetComponent<Animator>().SetTrigger(e.animationEvent);
                    break;
                case EventList.Event.EventType.MUSIC_CHANGE:
                    AudioManager.instance.PlayMusic(e.audio);
                    break;
                case EventList.Event.EventType.SOUND_PLAY:
                    AudioManager.instance.PlaySFX(e.audio);
                    break; 
                case EventList.Event.EventType.CAMERA_SHOT_CHANGE:
                    Camera.main.gameObject.transform.position = e.newCameraPosition;
                    Camera.main.gameObject.transform.eulerAngles = e.newCameraRotation;
                    break;
                case EventList.Event.EventType.UNITY_EVENT:
                    m_CurrentList.customEvents[e.customEventIndex].Invoke();
                    break; 
                case EventList.Event.EventType.TUTORIAL:
                    TutorialPlayer.instance.SetTutorialDisplaying(true);
                    TutorialPlayer.instance.ReadTutorial(e.dialogue);
                    break;
                case EventList.Event.EventType.HOLD_ON:
                    Debug.Log("Waiting...");
                    break; 
                case EventList.Event.EventType.COMPLETE:
                    Application.Quit();
                    break;
            }

            switch (e.continueType)
            {
                case EventList.Event.ContinueType.IMMEDIATE:
                    continue;
                case EventList.Event.ContinueType.WAIT:
                    yield return new WaitForSeconds(e.waitTime);
                    break;
                case EventList.Event.ContinueType.CUSTOM:
                    yield return new WaitUntil(() => m_Proceed);
                    m_Proceed = false;
                    break; 
                case EventList.Event.ContinueType.ON_CLICK:
                    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                    yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                    break;
            }
        }
    }
}