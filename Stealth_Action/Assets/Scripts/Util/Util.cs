using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T FindChild<T>(this GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(recursive)
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                T component = go.GetComponent<T>();

                if(component != null)
                {
                    if (string.IsNullOrEmpty(name) || component.name == name)
                        return component;
                }
            }
        }

        return null;
    }
}
