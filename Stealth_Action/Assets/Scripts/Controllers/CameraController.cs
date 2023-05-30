using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _player;
    Vector3 _delta = new Vector3(0, 12, -9f);
    Quaternion _rotOrigin;

    float _rotSpeed = 180f;
    void Start()
    {   
        _player = Managers.Game.GetPlayer().transform;
        _rotOrigin = transform.rotation;
    }

    void LateUpdate()
    {
        if (_player == null)
            return;

        RaycastHit hit;

        if (Physics.Raycast(_player.transform.position, _delta.normalized, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
        {
            float distance = (hit.point - _player.transform.position).magnitude * 0.8f;
            transform.position = _player.transform.position + _delta.normalized * distance;
        }
        else
        {
            transform.position = _player.transform.position + _delta;
        }

        Debug.DrawRay(_player.transform.position, _delta.normalized * _delta.magnitude, Color.red, .1f);
        
        //_mouseX = Input.GetAxis("Mouse X");

        Vector3 forward = Managers.Game.GetPlayer().transform.forward;
        forward.y = -transform.up.y;
        transform.forward = forward;

        float angle = (Mathf.PI * 2) + 180f;
        Vector3 dir = Util.DirFromAngle(angle);
        Vector3 dest = _player.transform.position + (dir * _delta.magnitude);
        transform.position = dest;

        UpdateCam();
    }

    [SerializeField] float _mouseX = 0;
    float _radius = 10f;

    void UpdateCam()
    {
        if (Input.GetMouseButton(0))
        {
            _mouseX += Input.GetAxis("Mouse X");

            float angle = (Mathf.PI * 2) * _mouseX + 180f;
            Vector3 dir = Util.DirFromAngle(angle);
            Vector3 dest = _player.transform.position + (dir * (_delta.magnitude / 2)) + (Vector3.up * _delta.y);
            transform.position = dest;

            transform.LookAt(_player);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            _mouseX = 0;
            transform.rotation = _rotOrigin;
        }
    }
}
