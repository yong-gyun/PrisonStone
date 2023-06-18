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
        Managers.Resource.Destroy(gameObject, 2f);
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        Vector3 dir = (_destPos - transform.position).normalized;

        if (dir.sqrMagnitude <= 0.2f)
            Destroy(gameObject);

        transform.position += dir * _moveSpeed * Time.deltaTime;
        transform.LookAt(_destPos);
    }

    bool _isHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAround"))
        {
            EnemyController enemy = other.GetComponentInParent<EnemyController>();
            StartCoroutine(CoMeasure(enemy));
            Debug.Log("Check");
        }

        if(other.CompareTag("Enemy"))
        {
            _isHit = true;
            other.GetComponent<EnemyController>().OnHit();
            Managers.Resource.Instantiate("Sleep", other.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator CoMeasure(EnemyController enemy)
    {
        float currentTime = 0;

        while (currentTime <= 0.75f)
        {
            currentTime += Time.deltaTime;

            if (_isHit)
                yield break;

            yield return null;
        }

        enemy.OnWarning(5);
        Destroy(gameObject);
    }
}
