using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    GameObject originBullet;
    Transform firePos;
    float rotSpeed = 30;
    float currentTime = 0;

    public int maxCount = 4;
    public int currentCount = 0;
    
    protected override void Init()
    {
        originBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        moveSpeed = 7.5f;
        firePos = transform.Find("FirePos");
        currentCount = maxCount;
    }

    protected override void Update()
    {
        base.Update();
        UpdateAttack();

        if(currentCount < maxCount)
        {
            currentTime += Time.deltaTime;

            if(currentTime >= 4f)
            {
                currentTime = 0;
                currentCount++;
            }
        }

    }

    protected override void UpdateMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = (Vector3.back * horizontal) + (Vector3.right * vertical);

        if (dir != Vector3.zero)
        {
            transform.position += dir.normalized * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
        }
    }

    //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //Vector3 dir = mousePos - transform.position;
    //dir.y = 0;
    //float theta = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //float degree = theta * Mathf.Rad2Deg; 
    //Quaternion qua = Quaternion.AngleAxis(degree, Vector3.up);
    //transform.rotation = qua;

    protected override void UpdateAttack()
    {
        if (currentCount == 0)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                Vector3 dir = hit.point - transform.position;
                Quaternion qua = Quaternion.LookRotation(dir);
                transform.rotation = qua;
            }
            Instantiate(originBullet, firePos.position, transform.rotation);
            currentCount--;
        }
    }
}