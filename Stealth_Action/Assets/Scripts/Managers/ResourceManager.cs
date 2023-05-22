using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    Dictionary<string, UnityEngine.Object> _dict = new Dictionary<string, UnityEngine.Object>();

    public T Load<T>(string path) where T : UnityEngine.Object
    {
        if(_dict.ContainsKey(path))
        {
            return _dict[path] as T;
        }

        T origin = Resources.Load<T>(path);
        
        if(origin == null)
        {
            return null;
        }
        
        _dict.Add(path, origin);
        return origin;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject origin = Load<GameObject>($"Prefabs/{path}");
        
        if(origin == null)
        {
            Debug.Log(path);
            return null;
        }
        
        GameObject go = Object.Instantiate(origin, parent);
        go.name = origin.name;

        return go;
    }

    public GameObject Instantiate(string path, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject go = Instantiate(path, parent);

        if (go == null)
            return null;

        go.transform.position = position; 
        go.transform.rotation = rotation;
        return go;
    }

    public void Destroy(GameObject go, float time = 0)
    {
        Object.Destroy(go, time);
    }
}
