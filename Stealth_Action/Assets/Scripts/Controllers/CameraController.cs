using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _mouseX = 0;
    [SerializeField] float _sensitive = 20f;
    [SerializeField]Transform _aimingPoint;
    [SerializeField]Transform _quarterviewPoint;
    [SerializeField] Define.CameraMode _mode;
    Transform _player;

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
        Managers.Game.GetPlayer().transform.forward = transform.forward;

        _mouseX += Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(0, _mouseX, 0);
    }

    void QuarterView()
    {
        Camera.main.transform.position = _quarterviewPoint.position;
        Camera.main.transform.localRotation = Quaternion.Euler(20, 0, 0);

        Debug.DrawRay(Managers.CamRoot.transform.position, new Vector3(Managers.CamRoot.forward.x, 0, Managers.CamRoot.forward.z).normalized, Color.red, 1f);

        if(Input.GetKeyDown(KeyCode.R))
        {
            _mouseX = 0;
        }

        if(Input.GetMouseButtonDown(1))
        {
            _mode = Define.CameraMode.Aiming;
            _crosshair = Managers.UI.MakeProduction<UI_Crosshair>();
            _crosshair.OnShow();
        }
    }

    UI_Crosshair _crosshair;
    
    void Aiming()
    {
        Camera.main.transform.position = _aimingPoint.position;
        Camera.main.transform.localRotation = Quaternion.identity;
        float t = Time.deltaTime / 0.1f;
        
        if (Input.GetMouseButtonUp(1))
        {
            _mode = Define.CameraMode.Quarterview;
            _crosshair.OnClose();
        }
    }
}
