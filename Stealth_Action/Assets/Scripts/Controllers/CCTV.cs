using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CCTV : MonoBehaviour
{
    MeshFilter _meshFilter;
    Mesh _mesh;
    Transform _findPoint;

    float _rotRange = 40f;
    float _rotSpeed = 0.25f;
    float _minAngle = 0;
    float _maxAngle = 0;
    int _polygon = 15;
    float _width = 3f;
    float _height = 13f;

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

        RaycastHit hit;

        if (Physics.Raycast(_findPoint.position, (transform.forward + Vector3.down), out hit, _height))
        {
            float distance = Vector3.Distance(Managers.Game.GetPlayer().transform.position, hit.point);

            if (distance <= _width)
                Debug.Log("D");
        }

        Debug.DrawRay(_findPoint.position, (transform.forward + Vector3.down) * _height, Color.red, 0.1f);
        Renderer();
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

    void Renderer()
    {
        Vector3[] vertices = new Vector3[_polygon * 2 + 2];
        int[] triangles = new int[_polygon * 6];

        vertices[0] = Vector3.zero;

        //시계 방향
        {
            for (int i = 1; i <= _polygon; i++)
            {
                float angle = -i * (Mathf.PI * 2.0f) / _polygon;
                Vector3 vertex = DirFromAngle(angle) * _width;
                vertex.y = -_height;
                vertices[i] = vertex;
            }

            for (int i = 0; i < _polygon - 1; i++)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 2;
                triangles[i * 3 + 2] = i + 1;
            }

            triangles[3 * _polygon - 3] = 0;
            triangles[3 * _polygon - 2] = 1;
            triangles[3 * _polygon - 1] = _polygon;
        }

        //반시계방향
        {
            int vIdx = _polygon + 1;
            vertices[vIdx++] = vertices[0];

            for (int i = 1; i <= _polygon; i++)
            {
                vertices[vIdx++] = vertices[i];
            }

            int tIdx = 3 * _polygon;

            for (int i = 0; i < _polygon - 1; i++)
            {
                triangles[tIdx++] = (_polygon + 1) + i + 1;
                triangles[tIdx++] = (_polygon + 1) + i + 2;
                triangles[tIdx++] = (_polygon + 1);
            }

            triangles[tIdx++] = (_polygon + 1) + _polygon;
            triangles[tIdx++] = (_polygon + 1) + 1;
            triangles[tIdx++] = (_polygon + 1);
        }

        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }

    Vector3 DirFromAngle(float angle)
    {
        return new Vector3(Mathf.Cos(angle) * Mathf.Rad2Deg, 0, Mathf.Sin(angle) * Mathf.Rad2Deg).normalized;
    }
}
