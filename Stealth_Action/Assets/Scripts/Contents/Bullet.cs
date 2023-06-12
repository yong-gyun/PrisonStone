using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _range = 30f; 
    float _moveSpeed = 30f;
    Vector3 _originPos;
    Vector3 _destPos;

    public void Init(Vector3 destPos)
    {
        _destPos = destPos;
    }

    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
        _originPos = transform.position;
        Managers.Resource.Destroy(gameObject, 2f);
    }

    void Update()
    {
        float distance = (transform.position - _originPos).magnitude;

        if(distance >= _range)
            Destroy(gameObject);

        Vector3 dir = (_destPos - transform.position).normalized;
        transform.position += dir * _moveSpeed * Time.deltaTime;
        transform.LookAt(_destPos);
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

        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
