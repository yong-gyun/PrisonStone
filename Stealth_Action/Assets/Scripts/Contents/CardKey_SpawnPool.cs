using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardKey_SpawnPool : MonoBehaviour
{
    [SerializeField] Define.CardKey _type;

    float _currentTime = 0;
    float _spawnTime = 10;
    GameObject _key = null;

    private void Start()
    {
        Spawn();
    }


    private void Update()
    {
        if(_key == null)
        {
            _currentTime += Time.deltaTime;

            if(_currentTime >= _spawnTime)
            {
                Spawn();
                _currentTime = 0;
            }
        }
    }

    void Spawn() 
    {
        int randValue = Random.Range(-20, -360);

        _key = Managers.Resource.Instantiate($"Key/{_type}CardKey", transform.position, Quaternion.Euler(0, randValue, 0)); 
    }
}
