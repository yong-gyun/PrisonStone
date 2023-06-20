using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _mouseX = 0;
    [SerializeField] float _sensitive = 20f;
    [SerializeField] Transform _aimingPoint;
    [SerializeField] Transform _quarterviewPoint;
    [SerializeField] Define.CameraMode _mode;
    Vector3 _offset;
    Transform _player;

    void Start()
    {
        _player = Managers.Game.GetPlayer().transform;
        _mode = Define.CameraMode.Quarterview;

        _quarterviewPoint = transform.Find("QuarterviewPoint");
        _aimingPoint = transform.Find("AimingPoint");
        Vector3 delta = Vector3.up;
        _offset = delta * _player.GetComponent<Collider>().bounds.size.y / 2;
    }

    void LateUpdate()
    {
        if (Camera.main == null || _player == null)
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
        Managers.Game.GetPlayer().transform.forward = transform.forward;

        _mouseX += Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(0, _mouseX, 0);
    }

    void QuarterView()
    {
        RaycastHit hit;
        Vector3 dir = (_quarterviewPoint.position - _player.transform.position + _offset).normalized;
        float distance = (_quarterviewPoint.position - _player.transform.position + _offset).magnitude;

        if (Physics.Raycast(_player.transform.position, dir, out hit, distance, LayerMask.GetMask("Wall")))
        {
            float magnitude = (hit.point - _player.transform.position).magnitude * 0.8f;
            Camera.main.transform.position = _player.transform.position + dir * magnitude;
        }
        else
        {
            Camera.main.transform.position = _quarterviewPoint.position;
            Camera.main.transform.localRotation = Quaternion.Euler(20, 0, 0);
        }

        Debug.DrawRay(_quarterviewPoint.position, dir * distance, Color.red, 1f);

        //Camera.main.transform.position = _quarterviewPoint.position;
        //Camera.main.transform.localRotation = Quaternion.Euler(20, 0, 0);
        if (Input.GetKeyDown(KeyCode.R))
        {
            _mouseX = 0;
        }

        if (Input.GetMouseButtonDown(1))
        {
            _mode = Define.CameraMode.Aiming;
            _crosshair = Managers.UI.MakeProduction<UI_Crosshair>();
            _crosshair.OnShow();
        }
    }

    UI_Crosshair _crosshair;
    
    void Aiming()
    {
        //RaycastHit hit;
        //Vector3 dir = (_aimingPoint.position - _player.transform.position + _offset).normalized;
        //float distance = (_aimingPoint.position - _player.transform.position + _offset).magnitude;

        //if (Physics.Raycast(_player.transform.position, dir, out hit, distance, LayerMask.GetMask("Wall")))
        //{
        //    float magnitude = (hit.point - _player.transform.position).magnitude;
        //    Camera.main.transform.position = _player.transform.position + dir * magnitude;
        //}
        //else
        //{
        //    Camera.main.transform.position = _aimingPoint.position;
        //    Camera.main.transform.localRotation = Quaternion.identity;
        //}

        Camera.main.transform.position = _aimingPoint.position;
        Camera.main.transform.localRotation = Quaternion.identity;

        if (Input.GetMouseButtonUp(1))
        {
            _mode = Define.CameraMode.Quarterview;
            _crosshair.OnClose();
        }
    }
}
