using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rand_SpawnPool : SpawnPool
{
    Vector3 spawnPos;

    protected override IEnumerator CoSpawn()
    {
        float randRange = Random.Range(8, 11);
        Vector3 randDir = Random.insideUnitSphere * randRange;
        randDir.y = 0;
        Vector3 randPos = spawnPos + randDir;
        GameObject go = Instantiate(keyOrigin, randPos, Quaternion.identity);
        NavMeshAgent nma = go.GetComponent<NavMeshAgent>();

        Collider col = go.GetComponent<Collider>();        
        col.enabled = false;
        GameManager.Instance.Key = go;

        while (true)
        {
            NavMeshPath path = new NavMeshPath();

            if (nma.CalculatePath(randPos, path))
                break;

            yield return null;
        }

        col.enabled = true;
    }
}
