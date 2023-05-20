using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float range = 15f; 
    float moveSpeed = 15f;
    Vector3 originPos;

    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
        originPos = transform.position;
    }

    void Update()
    {
        float distance = (transform.position - originPos).magnitude;

        if(distance >= range)
            Destroy(gameObject);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAround"))
            other.GetComponentInParent<EnemyController>().OnWarning(5);

        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().OnHit();
            Destroy(gameObject);
        }
    }
}
