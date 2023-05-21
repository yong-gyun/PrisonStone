using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _target;
    [SerializeField] Vector3 _offset = new Vector3(0, 25, -20);
    float _moveSpeed = 20f;

    void Start()
    {
        _target = Managers.Game.Player.transform;
    }

    void LateUpdate()
    {
        if (_target == null)
            return;

        Vector3 destnation = _target.position;
        destnation.z = 0;
        transform.position = Vector3.Lerp(transform.position, destnation + _offset, _moveSpeed * Time.deltaTime);
    }
}
