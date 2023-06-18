using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameClear : UI_Popup
{
    enum Images
    {
        Image
    }

    enum GameObjects
    {
        Texts
    }

    protected override void Init()
    {
        base.Init();
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));
        Managers.Game.GetPlayer().GetComponent<PlayerController>().enabled = false;
        StartCoroutine(CoFade());
    }

    IEnumerator CoFade()
    {
        Image img = GetImage((int)Images.Image);
        float f_time = 2f;
        float t = 0;
        
        while (img.color.a < 1f)
        {
            Color imgColor = img.color;
            t += Time.deltaTime / f_time;
            imgColor.a = Mathf.Lerp(0f, 1f, t);
            img.color = imgColor;

            if (img.color.a > 0.75f)
                GetObject((int)GameObjects.Texts).SetActive(true);

            yield return null;
        }
    }
}
