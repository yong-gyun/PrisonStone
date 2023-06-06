using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Letter : UI_Popup
{
    enum Buttons
    {
        CloseButton
    }

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        GetButton((int)Buttons.CloseButton).onClick.AddListener(ClosePopupUI);
    }

    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
        Managers.UI.MakeProduction<UI_FadeIn>().OnFadeHandler += () => 
        {
            Managers.UI.MakeProduction<UI_FadeOut>().OnFadeHandler += () => 
            {
                Managers.Sequnce.Play(Define.SequnceNumber.Opening_2);
                Managers.Game.GetPlayer().GetComponent<PlayerController>().IsActionable = true;
            };
            Managers.Game.KeyInventory[Define.CardKey.White]++;
            Managers.Game.GetPlayer().FindChild("Gun", true).SetActive(true);

            Managers.Game.GetPlayer().transform.position = new Vector3(0f, 0f, -42f);
            Managers.Game.GetPlayer().transform.rotation = Quaternion.identity;
            Managers.Game.GetPlayer().FindChild("Model", true).transform.rotation = Quaternion.identity;
            Camera.main.transform.position = new Vector3(0f, 9.5f, -47.5f);
        };
    }
}
