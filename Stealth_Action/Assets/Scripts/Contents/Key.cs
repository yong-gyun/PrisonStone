using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    GameObject _player;
    Collider col;
    float _range;
    bool _isView;

    private void Start()
    {
        _player = Managers.Game.Player;
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        float distance = (_player.transform.position - transform.position).sqrMagnitude;

        if(distance <= _range)
        {
            if(!_isView)
            {
                _isView = true;
                //Show Get item hud
            }
        }
        else
        {
            _isView = false;
        }
    }

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
    }
}
