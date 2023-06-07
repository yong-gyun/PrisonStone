using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _mouseX = 0;
    [SerializeField] float _mouseY = 0;
    [SerializeField] float _sensitive = 20f;
    Transform _aimingPoint;
    Transform _quarterviewPoint;
    Transform _player;
    Define.CameraMode _mode;
    
    void Start()
    {
        _player = Managers.Game.GetPlayer().transform;
        _mode = Define.CameraMode.Quarterview;

        _quarterviewPoint = transform.Find("QuarterviewPoint");
        _aimingPoint = transform.Find("AimingPoint");
    }

    void LateUpdate()
    {
        if (Camera.main == null)
            return;

        switch(_mode)
        {
            case Define.CameraMode.Quarterview:
                QuarterView();
                break;
            case Define.CameraMode.Aiming:
                Aiming();
                break;
        }

        transform.position = _player.position;
    }

    void QuarterView()
    {
        Camera.main.transform.parent = _quarterviewPoint;
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localRotation = Quaternion.identity;
        //transform.rotation = Quaternion.Euler(transform.rotation.x ,_player.eulerAngles.y, transform.rotation.z);
        
        _mouseX += Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(0, _mouseX, 0);
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            _mouseX = 0;
        }
    }

    void Aiming()
    {

    }
}
