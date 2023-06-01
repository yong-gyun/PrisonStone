using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T GetOrAddComponent<T>(this GameObject go)  where T : Component
    {
        if(go == null)
            return null;

        T component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static T FindChild<T>(this GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(recursive)
        {
            foreach (T component in go.GetComponentsInChildren<T>(true))
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
                T component = transform.GetComponent<T>();

                if(component != null)
                {
                    if (string.IsNullOrEmpty(name) || component.name == name)
                        return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindChild(this GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static DataContents.ViewCastInfo ViewCast(this GameObject go, float angle, float range)
    {
        Vector3 dir = DirFromAngle(angle);
        RaycastHit hit;

        if (Physics.Raycast(go.transform.position, dir, out hit, range))
            return new DataContents.ViewCastInfo(true, angle, hit.distance, hit.point);
        else
            return new DataContents.ViewCastInfo(false, angle, hit.distance, go.transform.position + dir * range);
    }

    public static Vector3 DirFromAngle(this float angle)
    {
        return new Vector3(Mathf.Cos((-angle + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angle + 90) * Mathf.Deg2Rad));
    }

    public static SequnceEvent BindSequnceEvent(this GameObject go, Action action)
    {
        SequnceEvent evt = go.GetOrAddComponent<SequnceEvent>();
        evt.OnSequnceEventHandler += action;
        return evt;
    }
}
