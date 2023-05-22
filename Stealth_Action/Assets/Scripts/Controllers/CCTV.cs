using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    MeshFilter _meshFilter;
    Mesh _mesh;
    List<Vector3> _points = new List<Vector3>();
    Transform _findPoint;
    public float Radius { get { return _findRadius; } }
    public float Angle { get { return _findAngle; } }

    float _findRadius = 5f;
    float _height = 7.5f;
    float _findAngle = 75f;
    float _rotRange = 40f;
    float _rotSpeed = 0.25f;
    float _minAngle = 0;
    float _maxAngle = 0;
    
    private void Awake()
    {
        _minAngle = transform.eulerAngles.y - _rotRange;
        _maxAngle = transform.eulerAngles.y + _rotRange;

        _findPoint = transform.Find("FindPoint");

        _meshFilter = _findPoint.GetComponent<MeshFilter>();
        _mesh = new Mesh { name = "ViewMesh" };
        _meshFilter.mesh = _mesh;
        
        StartCoroutine(CoRotation());
    }

    private void Update()
    {
        if (Managers.Game.GetPlayer() == null)
            return;

        Vector3 interval = Managers.Game.GetPlayer().transform.position - _findPoint.position;
        interval.y = 0;
        float distance = interval.magnitude; 

        if(distance <= _findRadius)
        {
            float dotProduct = Vector3.Dot(interval.normalized, transform.forward + Vector3.down);
            float theta = Mathf.Acos(dotProduct);
            float degree = Mathf.Rad2Deg * theta;

            if (degree < _findAngle / 2)
            {
                RaycastHit hit;

                if(Physics.Raycast(transform.position, interval.normalized, out hit, _findRadius))
                {
                    if (!Managers.Game.EnemyList[0].IsWarining)
                    {
                        Instantiate(Resources.Load<GameObject>("Prefabs/UI_Warning"));

                        for (int i = 0; i < Managers.Game.EnemyList.Count; i++)
                        {
                            Managers.Game.EnemyList[i].OnWarning(10);
                        }
                    }

                }
            }
        }

        DrawMesh();
    }

    IEnumerator CoRotation()
    {
        WaitForSeconds wfs = new WaitForSeconds(2f);

        while(true)
        {
            float degree = 0;
            float t = 0;

            while (degree < _maxAngle)
            {
                t += _rotSpeed * Time.deltaTime;
                degree = Mathf.Lerp(transform.eulerAngles.y, _maxAngle, t);
                Quaternion qua = Quaternion.Euler(0, degree, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, qua, t);
                yield return null;
            }

            yield return wfs;
            t = 0;

            while (degree > _minAngle)
            {
                t += _rotSpeed * Time.deltaTime;
                degree = Mathf.Lerp(transform.eulerAngles.y, _minAngle, t); 
                Quaternion qua = Quaternion.Euler(0, degree, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, qua, t);
                yield return null;
            }

            yield return wfs;
        }
    }


    float _meshResolution = 0.1f;

    void DrawMesh()
    {
        _points.Clear();

        int count = Mathf.RoundToInt(_findAngle * _meshResolution);
        float size = _findAngle / count;

        for (int i = 0; i <= count; i++)
        {
            float angle = transform.eulerAngles.y - _findAngle / 2 + size * i;
            Vector3 end = _findPoint.position + DirFromAngle(angle) * _findRadius;
            end.y = transform.position.y - 2;
            _points.Add(end);
        }

        int vertexCount = _points.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(_points[i]);

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

    public Vector3 DirFromAngle(float angle)
    {
        return new Vector3(Mathf.Cos((-angle + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angle + 90) * Mathf.Deg2Rad));
    }

    //void OnDrawGizmos()
    //{
    //    Handles.color = new Color(1, 0, 0, 0.1f);
    //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, _findAngle, _findRange);
    //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -_findAngle, _findRange);
    //}
}
