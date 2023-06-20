using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tutorial_Subitem : UI_Base
{
    enum Buttons
    {
        NextButton,
        PrevButton
    }

    protected override void Init()
    {
        BindButton(typeof(Buttons));
    }

    void OnClickNextButton()
    {

    }
}
