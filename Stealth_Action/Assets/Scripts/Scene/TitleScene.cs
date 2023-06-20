using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        Managers.UI.ShowSceneUI<UI_Title>();
    }
}
