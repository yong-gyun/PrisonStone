using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Stamina : UI_Production
{
    PlayerController _player;

    enum Scrollbars
    {
        StaminaScroll
    }

    protected override void Init()
    {
        base.Init();
        BindScrollbar(typeof(Scrollbars));
        _player = Managers.Game.GetPlayer().GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = _player.CurrentStamina / _player.MaxStamina;
        GetScrollbar((int)Scrollbars.StaminaScroll).size = value;
    }
}
