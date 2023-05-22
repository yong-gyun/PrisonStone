using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    GameObject _player;
    Collider col;
    float _range;
    bool _isView;
    Vector3 _offset = new Vector3(2, 2.25f, 1);
    UI_Interaction _interactionUI;

    private void Start()
    {
        _player = Managers.Game.GetPlayer();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Managers.Game.GetPlayer() == null)
            return;

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

            if (enemy.state != Define.EnemyState.Follow)
            {
                enemy.state = Define.EnemyState.Move;
            }

            Destroy(gameObject);
        }

        if(other.CompareTag("Player"))
        {
            _interactionUI = Managers.UI.MakeWorldSpaceUI<UI_Interaction>();
        }
    }

    void GetKey()
    {

    }
}
