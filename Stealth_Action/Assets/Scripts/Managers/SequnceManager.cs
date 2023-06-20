using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class SequnceManager
{
    Transform _root;

    int _count = 0;
    public PlayableDirector CurrentSequnce
    {
        get
        {
            if(_currentSequnce == null)
            {
                GameObject go = GameObject.Find("Timeline");
                _currentSequnce = go.GetComponent<PlayableDirector>();
            }

            return _currentSequnce;
        }
    }

    PlayableDirector _currentSequnce;
    public bool IsCinematic { get; private set; }
    public UI_Sequnce SequnceUI
    {
        get
        {
            if (_sequnceUI == null)
                _sequnceUI = GameObject.Find("UI_Dialog").GetComponent<UI_Sequnce>();

            return _sequnceUI;
        }
    }
    
    UI_Sequnce _sequnceUI;
    Camera _cinematicCam;
    Camera _mainCamera;
    Transform _camRoot;

    public Camera CinematicCamera 
    { 
        get 
        {
            if(_cinematicCam == null)
            {
                _cinematicCam = GameObject.Find("@CinematicCam").GetComponent<Camera>();
            }

            return _cinematicCam; 
        } 
        set
        { 
            _cinematicCam = value; 
        }
    }

    public void Init()
    {
        _root = GameObject.Find("@TimelineRoot").transform;

        for (int i = 0; i < _root.childCount; i++)
        {
            Transform child = _root.GetChild(i);
            child.GetComponent<PlayableDirector>().playOnAwake = false;
            Define.SequnceNumber sequnce = (Define.SequnceNumber)i;

            switch (sequnce)
            {
                case Define.SequnceNumber.Opening_1:
                    child.gameObject.BindSequnceEvent(() =>
                    {
                        Managers.UI.ShowPopupUI<UI_Letter>(); 
                        IsCinematic = false;
                        Managers.Game.GetPlayer().GetComponent<PlayerController>().IsActionable = false;
                        Cursor.lockState = CursorLockMode.None;
                    });
                    break;
                case Define.SequnceNumber.Opening_2:
                    child.gameObject.BindSequnceEvent(() =>
                    {
                        SequnceUI.SetActive(false);
                        IsCinematic = false;
                        Managers.Game.GetPlayer().GetComponent<PlayerController>().Init();
                        Managers.UI.ShowSceneUI<UI_Status>();
                        Managers.Sound.Play("Bgm/Game", Define.Sound.Bgm);
                        Stop();
                    });
                    break;
            }
        }

        _mainCamera = Camera.main;
        _cinematicCam = GameObject.Find("@CinematicCam").GetComponent<Camera>();
        _camRoot = _mainCamera.transform.root;
    }

    public void Play(Define.SequnceNumber num)
    {
        _currentSequnce = _root.GetChild((int)num).gameObject.GetComponent<PlayableDirector>();
        SequnceUI.SetActive(true);
        _currentSequnce.Play();

        CinematicCamera.gameObject.SetActive(true);
        _mainCamera.gameObject.SetActive(false);
        IsCinematic = true;

        _camRoot.GetComponent<CameraController>().enabled = false;
        Managers.Game.GetPlayer().GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Stop()
    {
        PlayableDirector playableDirector = _currentSequnce.GetComponent<PlayableDirector>();
        playableDirector.Stop();
        CinematicCamera.gameObject.SetActive(false);
        _mainCamera.gameObject.SetActive(true);
        Managers.Game.GetPlayer().GetComponent<Rigidbody>().isKinematic = false;
        _camRoot.GetComponent<CameraController>().enabled = true;
    }
}
