using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLinkUtility : MonoBehaviour
{
    public void AccessURL(string URL)
    {
        Application.OpenURL(URL);
    }
}
