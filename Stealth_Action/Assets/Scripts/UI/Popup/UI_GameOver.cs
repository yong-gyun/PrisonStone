using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : UI_Popup
{
    enum Images
    {
        Image
    }

    enum Texts
    {
        Text
    }

    protected override void Init()
    {
        base.Init();
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        Managers.Sound.Play("Bgm/Over", Define.Sound.Bgm);
        StartCoroutine(CoFade());
    }

    IEnumerator CoFade()
    {
        Image img = GetImage((int)Images.Image);
        TMP_Text text = GetText((int)Texts.Text);
        float f_time = 4f;
        float t = 0;

        while(img.color.a < 1f)
        {
            Color imgColor = img.color;
            t += Time.deltaTime / f_time;
            imgColor.a = Mathf.Lerp(0, 1, t);
            img.color = imgColor;

            Color textColor = text.color;
            t += Time.deltaTime / f_time;
            textColor.a = Mathf.Lerp(0, 1, t);
            text.color = textColor;
            yield return null;
        }
    }
}
