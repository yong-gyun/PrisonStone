using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : BaseScene
{
    protected override void Init()
    {
        Define.Scene type = Managers.Scene.AsyncLoadSceneType;
        Managers.UI.ShowSceneUI<UI_Loading>().LoadScene(type);
    }
}
