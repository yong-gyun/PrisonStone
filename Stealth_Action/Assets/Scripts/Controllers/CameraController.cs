using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _player;
    [SerializeField] Vector3 _delta = new Vector3(0, 10, -7.5f); 

    void Start()
    {   
        _player = Managers.Game.GetPlayer().transform;
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
        //transform.rotation = Quaternion.Euler(_target.rotation.eulerAngles + _quaOffset);
        
    }
}
