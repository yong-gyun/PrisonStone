using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Production : UI_Base
{
    protected override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void CloseProduction()
    {
        Managers.UI.CloseProduction(gameObject);
    }
}
