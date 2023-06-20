using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Define.CardKey _type;
    GameObject _player;
    Collider col;
    float _range;
    bool _isView;
    Vector3 _offset = new Vector3(2, 2.25f, 1);
    UI_Interaction _interactionUI;

    private void Start()
    {
        switch(gameObject.name)
        {
            case "RedCardKey":
                _type = Define.CardKey.Red;
                break;
            case "WhiteCardKey":
                _type = Define.CardKey.White;
                break;
            case "YellowCardKey":
                _type = Define.CardKey.Yellow;
                break;
            case "BlueCardKey":
                _type = Define.CardKey.Blue;
                break;
            case "GreenCardKey":
                _type = Define.CardKey.Green;
                break;

        }

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

            if (enemy.State != Define.EnemyState.Follow)
            {
                enemy.State = Define.EnemyState.Move;
            }

            Managers.Resource.Destroy(gameObject);

            if(_interactionUI != null)
                Managers.Resource.Destroy(_interactionUI.gameObject);
        }

        if(other.CompareTag("Player"))
        {
            _interactionUI = Managers.UI.MakeWorldSpaceUI<UI_Interaction>();
            _interactionUI.transform.position = transform.position + _offset;
            _interactionUI.SetInfo("»πµÊ«œ±‚");

            _interactionUI.OnInteractionHandler += GetKey;
            Debug.Log("Check");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Managers.Resource.Destroy(_interactionUI.gameObject);
        }
    }

    void GetKey()
    {
        Managers.Game.KeyInventory[(int)_type]++;
        Managers.Resource.Destroy(_interactionUI.gameObject);
        Managers.Resource.Destroy(gameObject);
        Debug.Log(_type);
    }
}
