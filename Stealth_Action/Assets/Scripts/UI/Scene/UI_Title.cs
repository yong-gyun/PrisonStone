using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Title : UI_Scene
{
    enum Buttons
    {
        PlayButton,
        OptionButton,
        ExitButton
    }

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        GetButton((int)Buttons.PlayButton).onClick.AddListener(OnClickPlayButton);
        GetButton((int)Buttons.OptionButton).onClick.AddListener(OnClickOptionButton);
        GetButton((int)Buttons.ExitButton).onClick.AddListener(OnClickExitButton);
    }
    
    void OnClickPlayButton()
    {
        Managers.Scene.LoadSceneAsync(Define.Scene.Game);
    }

    void OnClickExitButton()
    {
        Application.Quit();
    }

    void OnClickOptionButton()
    {

    }
}
