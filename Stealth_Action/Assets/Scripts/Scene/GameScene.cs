using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        Managers.Sequnce.Init();
        Managers.UI.MakeProduction<UI_FadeOut>();
    }

    private void Start()
    {
        Managers.Sequnce.PlaySequnce(Define.SequnceNumber.Opening_1);
    }
}
