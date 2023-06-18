using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseController
{
    public Define.EnemyState State { get; set; }
    public bool IsWarining { get; private set; }
    NavMeshAgent _nma;
    Transform _target;
    MeshFilter _meshFilter;
    Mesh _mesh;

    [SerializeField] Transform[] _points;
    [SerializeField] Transform _findPoint;

    float _playerFindRange = 6f;
    float _playerFindAngle = 60f;
    float _keyFindAngle = 75f;
    float _keyFindRange = 8f;

    [SerializeField] int _pointIdx = 0;
    bool isStun;
    
    public override void Init()
    {
        _minSpeed = 6f;
        _maxSpeed = 8f;
        _moveSpeed = _minSpeed;

        _nma = GetComponent<NavMeshAgent>();
        _nma.speed = _moveSpeed;
        
        _playerFindAngle = 60f;
        _playerFindRange = 6f;
        Managers.Game.EnemyList.Add(this);

        _findPoint = gameObject.FindChild("FindPoint", true).transform;
        _meshFilter = _findPoint.GetComponent<MeshFilter>();
        _mesh = new Mesh { name = "ViewMesh" };
        _meshFilter.mesh = _mesh;
    }

    protected override void Update()
    {
        if (isStun)
            return;

        switch(State)
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

        DrawMesh();
        _nma.SetDestination(_target.position);
    }

    protected override void UpdateMove()
    {
        int count = Mathf.RoundToInt(_keyFindAngle * 0.25f); 
        float size = _keyFindAngle / count;
        
        for (int i = 0; i <= count; i++)
        {
            float angle = _findPoint.eulerAngles.y - _keyFindAngle / 2 + size * i;
            Vector3 dir = angle.DirFromAngle();

            RaycastHit hit;

            if(Physics.Raycast(_findPoint.position, dir, out hit, _keyFindRange))
            {
                if(hit.collider.CompareTag("Key"))
                {
                    _target = hit.transform;
                    State = Define.EnemyState.Find;
                    return;
                }
            }
        }

        if (Managers.Game.GetPlayer() != null)
        {
            Vector3 interval = (Managers.Game.GetPlayer().transform.position - _findPoint.position);
          
            if (interval.magnitude <= _playerFindRange)
            {
                float dotProduct = Vector3.Dot(interval.normalized, _findPoint.forward);
                float degree = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
                
                if (degree <= _playerFindAngle)
                {
                    State = Define.EnemyState.Follow;
                    return;
                }
            }
        }

        _target = _points[_pointIdx % _points.Length];
        
        if ((_target.position - transform.position).magnitude <= 5f)
        {
            _pointIdx++;
        }
    }
    

    void UpdateFind()
    {
        if(_target == null)
        {
            State = Define.EnemyState.Move;
        }
    }

    void UpdateFollow()
    {
        _target = Managers.Game.GetPlayer().transform;
        Vector3 dir = _target.position - transform.position;
        dir.y = 0;

        if (Physics.Raycast(transform.position, dir.normalized, 1f, LayerMask.GetMask("Wall")))
            State = Define.EnemyState.Move;

        _nma.speed = _moveSpeed + 2f;
        float distance = dir.magnitude;
        NavMeshPath path = new NavMeshPath();
        
        if (distance > 10f || !_nma.CalculatePath(_target.position, path))
        {
            State = Define.EnemyState.Move;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            State = Define.EnemyState.Move;
            Destroy(other.gameObject);
            Managers.UI.ShowPopupUI<UI_GameOver>();
        }
    }

    public void OnHit()
    {
        StartCoroutine(CoStun());
    }

    IEnumerator CoStun()
    {
        _nma.SetDestination(transform.position);
        isStun = true;
        yield return new WaitForSeconds(10f);
        isStun = false;
    }

    IEnumerator CoWarning(float time)
    {
        yield return new WaitForSeconds(time);
        IsWarining = false;
        _playerFindRange = 6f;
        _playerFindAngle = 60f;
        _nma.speed = _moveSpeed;
    }

    public void OnWarning(float time)
    {
        if (IsWarining)
            return;

        IsWarining = true;
        _playerFindRange = 15f;
        _playerFindAngle = 150f;
        
        _nma.speed = _moveSpeed * 1.5f;
        StartCoroutine(CoWarning(time));
    }

    List<Vector3> _vertex = new List<Vector3>();
    float _meshResolution = 0.1f;

    void DrawMesh()
    {
        _vertex.Clear();

        int count = Mathf.RoundToInt(_playerFindAngle * _meshResolution);
        float size = _playerFindAngle / count;

        for (int i = 0; i <= count; i++)
        {
            float angle = transform.eulerAngles.y - _playerFindAngle / 2 + size * i;
            DataContents.ViewCastInfo viewCastInfo = gameObject.ViewCast(angle, _playerFindRange);
            _vertex.Add(viewCastInfo.Point);
        }

        int vertexCount = _vertex.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(_vertex[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        _meshFilter.mesh.Clear();
        _meshFilter.mesh.vertices = vertices;
        _meshFilter.mesh.triangles = triangles;
        _meshFilter.mesh.RecalculateNormals();
    }
}