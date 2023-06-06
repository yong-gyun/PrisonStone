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
                        Managers.UI.ShowPopupUI<UI_Letter>(); IsCinematic = false;
                        Managers.Game.GetPlayer().GetComponent<PlayerController>().IsActionable = false;
                    });
                    break;
                case Define.SequnceNumber.Opening_2:
                    child.gameObject.BindSequnceEvent(() =>
                    {
                        SequnceUI.SetActive(false);
                        IsCinematic = false;
                    });
                    break;
            }

            child.gameObject.BindSequnceEvent(Stop);
        }
    }

    public void Play(Define.SequnceNumber num)
    {
        _currentSequnce = _root.GetChild((int)num).gameObject.GetComponent<PlayableDirector>();
        SequnceUI.SetActive(true);
        _currentSequnce.Play();

        CinematicCamera.gameObject.SetActive(true);
        Camera.main.gameObject.SetActive(false);
        IsCinematic = true;
        Debug.Log(num);
    }

    public void Stop()
    {
        PlayableDirector playableDirector = _currentSequnce.GetComponent<PlayableDirector>();
        playableDirector.Stop();
        _cinematicCam.gameObject.SetActive(false);
        Camera.main.gameObject.SetActive(true);
    }
}
