using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy.state != Define.State.Follow)
            {
                enemy.state = Define.State.Move;
            }

            Destroy(gameObject);
        }

        if(other.CompareTag("Player"))
            Destroy(gameObject);
    }
}
