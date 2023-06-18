using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    GameManager _game = new GameManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    SequnceManager _sequnce = new SequnceManager();
    SoundManager _sound = new SoundManager();

    public static GameManager Game { get { return Instance._game; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static SequnceManager Sequnce { get {  return Instance._sequnce; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    
    static Transform _camRoot;
    
    public static Transform CamRoot
    {
        get
        {
            return _camRoot;
        }
    }

    void Start()
    {
        Init();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            s_instance = go.GetComponent<Managers>();
            s_instance._game.Init();
            s_instance._sound.Init();
            _camRoot = Camera.main.transform.root;
        }
    }

    public static void Clear()
    {

    }
}
