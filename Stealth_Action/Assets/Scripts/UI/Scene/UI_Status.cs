using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Status : UI_Scene
{
    enum Texts
    {
        Text
    }

    PlayerController _player;

    protected override void Init()
    {
        base.Init();
        BindText(typeof(Texts));
        _player = Managers.Game.GetPlayer().GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_player == null)
            return;

        GetText((int)Texts.Text).text = $"{_player.CurrentBulletCount} / {_player.MaxBulletCount}";
    }
}
