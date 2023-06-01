using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequnceEvent : MonoBehaviour
{
    public Action OnSequnceEventHandler = null;

    public void OnSequnceEvent()
    {
        if (OnSequnceEventHandler != null)
        {
            OnSequnceEventHandler.Invoke();
        }
    }
}