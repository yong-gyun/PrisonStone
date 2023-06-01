using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; set; }

    private void Awake()
    {
        Init();
    }

    protected abstract void Init();
}
