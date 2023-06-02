using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SequnceManager
{
    Transform _root;

    int _count = 0;
    public GameObject CurrentSequnce
    {
        get
        {
            if(_currentSequnce == null)
            {
                _currentSequnce = GameObject.Find("Timeline");
            }

            return _currentSequnce;
        }
    }

    GameObject _currentSequnce;
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
    public void Init()
    {
        _root = GameObject.Find("@TimelineRoot").transform;
        
        for (int i = 0; i < _root.childCount; i++)
        {
            Transform child = _root.GetChild(i);
            child.GetComponent<PlayableDirector>().playOnAwake = false;
            Define.SequnceNumber sequnce = (Define.SequnceNumber) i;

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
                        Camera.main.GetComponent<CameraController>().enabled = true;
                        IsCinematic = false; 
                    });
                    break;
            }
        }
    }

    public void PlaySequnce(Define.SequnceNumber num)
    {
        _currentSequnce = _root.GetChild((int)num).gameObject;
        PlayableDirector playableDirector = _currentSequnce.GetComponent<PlayableDirector>();
        SequnceUI.SetActive(true);
        playableDirector.Play();
        IsCinematic = true;
        Debug.Log(num);
    }

    public void StopSequnce()
    {
        Managers.Resource.Destroy(CurrentSequnce);
    }

    public void CloseUI()
    {

    }
}
