using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Item
{
    public Define.CardKey Type;
    public int Count;
}

public class GameManager
{
    GameObject _player;
    public GameObject GetPlayer() 
    {
        if (_player == null)
            _player =GameObject.Find("Player");

        return _player; 
    }
    public GameObject Key;
    public List<EnemyController> EnemyList = new List<EnemyController>();
}
