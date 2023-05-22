using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseController
{
    public Define.EnemyState state { get; set; }
    public bool IsWarining { get; private set; }
    NavMeshAgent nma;
    Transform target;

    [SerializeField] Transform[] points;
    [SerializeField] float playerFindRange;
    [SerializeField] float playerFindAngle;
    [SerializeField] int pointIdx = 0;

    bool isStun;
    
    protected override void Init()
    {
        moveSpeed = 6f;
        nma = GetComponent<NavMeshAgent>();
        nma.speed = moveSpeed;

        playerFindAngle = 60f;
        playerFindRange = 6f;
        Managers.Game.EnemyList.Add(this);
    }

    protected override void Update()
    {
        if (isStun)
            return;

        switch(state)
        {
            case Define.EnemyState.Move:
                UpdateMove();
                break;
            case Define.EnemyState.Find:
                UpdateFind();
                break;
            case Define.EnemyState.Follow:
                UpdateFollow();
                break;
        }

        nma.SetDestination(target.position);
    }

    protected override void UpdateMove()
    {


        //if (Managers.Game.GetPlayer() != null)
        //{
        //    Vector3 interval = (Managers.Game.GetPlayer().transform.position - transform.position);
            
        //    if (interval.magnitude <= playerFindRange)
        //    {
        //        float dotProduct = Vector3.Dot(interval.normalized, transform.forward);
                
        //        if (playerFindAngle / 2 > dotProduct && dotProduct > 0)
        //        {
        //            state = Define.EnemyState.Follow;
        //            return;
        //        }
        //    }
        //}

        //if (Managers.Game.Key != null)
        //{
        //    Vector3 interval = (Managers.Game.Key.transform.position - transform.position);

        //    float keyFindRange = 5f;
        //    float keyFindAngle = 140f;

        //    if (interval.magnitude <= keyFindRange)
        //    {
        //        float dotProduct = Vector3.Dot(interval.normalized, transform.forward);

        //        if (keyFindAngle / 2 > dotProduct)
        //        {
        //            state = Define.EnemyState.Find;
        //            return;
        //        }
        //    }

        target = points[pointIdx % points.Length];
        float distance = (target.position - transform.position).magnitude;
        
        if (distance <= 1f)
        {
            pointIdx++;
        }
    }
    

    void UpdateFind()
    {
        //target = Managers.Game.Key.transform;
    }

    void UpdateFollow()
    {
        target = Managers.Game.GetPlayer().transform;
        Vector3 dir = target.position - transform.position;
        dir.y = 0;

        if (Physics.Raycast(transform.position, dir.normalized, 1f, LayerMask.GetMask("Wall")))
            state = Define.EnemyState.Move;

        float distance = dir.magnitude;
        NavMeshPath path = new NavMeshPath();
        
        if (distance > 10f || !nma.CalculatePath(target.position, path))
        {
            state = Define.EnemyState.Move;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            state = Define.EnemyState.Move;
            Destroy(other.gameObject);
        }
    }

    public void OnHit()
    {
        StartCoroutine(CoStun());
    }

    IEnumerator CoStun()
    {
        nma.SetDestination(transform.position);
        isStun = true;
        yield return new WaitForSeconds(10f);
        isStun = false;
    }

    IEnumerator CoWarning(float time)
    {
        yield return new WaitForSeconds(time);
        IsWarining = false;
        playerFindRange = 6f;
        playerFindAngle = 60f;
        nma.speed = moveSpeed;
    }

    public void OnWarning(float time)
    {
        if (IsWarining)
            return;

        IsWarining = true;
        playerFindRange = 15f;
        playerFindAngle = 150f;
        
        nma.speed = moveSpeed * 1.5f;
        StartCoroutine(CoWarning(time));
    }

    void OnDrawGizmos()
    {
        Handles.color = new Color(1, 0, 0, 0.1f);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, playerFindAngle / 2, playerFindRange);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -playerFindAngle / 2, playerFindRange);
    }
}