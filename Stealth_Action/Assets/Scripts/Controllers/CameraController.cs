using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _target;
    [SerializeField] Vector3 _offset = new Vector3(0, 25, -20);
    
    void Start()
    {
        _target = Managers.Game.GetPlayer().transform;
    }

    void LateUpdate()
    {
        if (_target == null)
            return;

        transform.position = _target.position + _offset;
    }
}
