using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemFinder : MonoBehaviour
{
    public void FindSystemLibrary()
    {
        SystemLibrary.instance.ToggleDyslexiaFont();
    }

    public void Startup()
    {
        EventListProcessor.instance.ExecuteEventList(0);
    }
}
