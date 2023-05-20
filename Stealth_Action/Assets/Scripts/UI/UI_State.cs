using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_State : MonoBehaviour
{
    EnemyController enemy;
    Image image;

    void Start()
    {
        enemy = transform.parent.GetComponent<EnemyController>();
        GameObject go = transform.Find("Image").gameObject;
        image = go.GetComponent<Image>();
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);

        if(enemy.IsWarining)
        {
            image.gameObject.SetActive(true);
        }
        else
        {
            image.gameObject.SetActive(false);
        }
    }
}
