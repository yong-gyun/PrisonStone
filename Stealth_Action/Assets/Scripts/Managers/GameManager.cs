using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public GameObject Player;
    public GameObject Key;
    public List<EnemyController> EnemyList = new List<EnemyController>();
}
