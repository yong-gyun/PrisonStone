using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _player;
    Vector3 _delta = new Vector3(0, 7.25f, -9f);
    Vector3 _zoomDelta = new Vector3(1.25f, 4.25f, -4f);
    Quaternion _rotOrigin;
    float _rotSpeed = 180f;

    void Start()
    {
        transform.rotation = Quaternion.identity;
        _player = Managers.Game.GetPlayer().transform;
        _rotOrigin = transform.rotation;
    }

    float _distance = 10f;

    void LateUpdate()
    {
        //플레이어의 뒤를 따라감

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = (Vector3.right * horizontal) + (Vector3.up * vertical);
        _player.forward = dir;

        //나중에 오픈
        //if (_player == null)
        //    return;

        //if (Input.GetMouseButton(0))
        //{
        //    _mouseX += Input.GetAxis("Mouse X");

        //    {
        //        float angle = (Mathf.PI * 2) * _mouseX + 180f;
        //        Vector3 dir = Util.DirFromAngle(angle);
        //        Vector3 dest = _player.transform.position + (dir * (_delta.magnitude * .8f)) + (Vector3.up * _delta.y);
        //        transform.position = dest;
        //    }
        //    {
        //        Vector3 dir = _player.position - transform.position;
        //        Quaternion qua = Quaternion.LookRotation(dir.normalized);
        //        transform.rotation = qua;
        //    }

        //    return;
        //}

        //if (Input.GetMouseButton(1))
        //{
        //    Vector3 dest = _player.position + _zoomDelta;
        //    transform.position = Vector3.Lerp(transform.position, dest, 180f * Time.deltaTime);

        //    _mouseX += Input.GetAxis("Mouse X");

        //    float angle = (Mathf.PI * 2) * _mouseX + 180f;
        //    Vector3 dir = Util.DirFromAngle(angle);

        //    return;
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    _mouseX = 0;
        //    transform.rotation = _rotOrigin;
        //}

        //RaycastHit hit;

        //if (Physics.Raycast(_player.transform.position, _delta.normalized, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
        //{
        //    float distance = (hit.point - _player.transform.position).magnitude * 0.8f;
        //    transform.position = _player.transform.position + _delta.normalized * distance;
        //}
        //else
        //{
        //    transform.position = _player.transform.position + _delta;
        //}

        //Debug.DrawRay(_player.transform.position, _delta.normalized * _delta.magnitude, Color.red, .1f);
    }

    [SerializeField] float _mouseX = 0;
}
