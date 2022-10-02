using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EventList : MonoBehaviour
{
    [System.Serializable]
    public class Event
    {
        public enum EventType
        {
            DIALOGUE, MUSIC_CHANGE, SOUND_PLAY, CAMERA_SHOT_CHANGE, ANIMATION, TUTORIAL, UNITY_EVENT, HOLD_ON, COMPLETE
        }

        [HideInInspector]
        public EventType eventType;
        [HideInInspector]
        public string speaker; 
        [HideInInspector]
        [TextArea(3, 10)]
        public string dialogue;
        [HideInInspector]
        public AudioClip audio;
        [HideInInspector]
        public Vector3 newCameraPosition;
        [HideInInspector]
        public Vector3 newCameraRotation;
        [HideInInspector]
        public GameObject animationTarget;
        [HideInInspector]
        public string animationEvent;
        [HideInInspector]
        public int customEventIndex; 

        public enum ContinueType
        {
            ON_CLICK, WAIT, CUSTOM, IMMEDIATE
        }
        
        [HideInInspector]
        public ContinueType continueType;
        [HideInInspector]
        public float waitTime; 
    }

    public List<Event> events;

    public List<UnityEvent> customEvents;

    public enum OnComplete
    {
        START_GAME, QUIT
    }

    public OnComplete OnEventListCompleted;
}
