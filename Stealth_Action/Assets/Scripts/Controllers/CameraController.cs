using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _target;
    [SerializeField] Vector3 _offset = new Vector3(0, 25, -20);
    Vector3 _quaOffset;
    void Start()
    {
        _quaOffset = transform.eulerAngles;
        _target = Managers.Game.GetPlayer().transform;
    }

    void LateUpdate()
    {
        if (_target == null)
            return;

        
        //transform.rotation = Quaternion.Euler(_target.rotation.eulerAngles + _quaOffset);
        transform.position = _target.position + _offset;
    }
}
