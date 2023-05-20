using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnPool : MonoBehaviour
{
    protected GameObject keyOrigin;
    protected float currentTime = 0;
    protected float spawnTime = 5f;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (GameManager.Instance.Key == null)
        {
            currentTime += Time.deltaTime;

            if (currentTime > spawnTime)
            {
                currentTime = 0;
                StartCoroutine(CoSpawn());
            }
        }
    }

    protected virtual void Init()
    {
        keyOrigin = Resources.Load<GameObject>("Prefabs/Key");
    }

    protected abstract IEnumerator CoSpawn();
}
